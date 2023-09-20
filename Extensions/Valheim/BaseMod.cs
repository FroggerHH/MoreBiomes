﻿using System;
using System.IO;
using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using ServerSync;

namespace Extensions.Valheim;

public sealed class ModBase
{
    private static bool sendDebugMessagesToHud;
    private static ConfigEntry<bool> sendDebugMessagesToHudConfig;

    private ModBase(string modName, string modAuthor, string modVersion)
    {
        ModName = modName;
        ModAuthor = modAuthor;
        ModVersion = modVersion;
        ModGUID = $"com.{ModAuthor}.{ModName}";
        harmony = new Harmony(ModGUID);
        ConfigFileName = $"{ModGUID}.cfg";
    }

    public static ModBase CreateMod(BaseUnityPlugin plugin, string modName, string modAuthor, string modVersion)
    {
        if (mod)
            throw new Exception($"Trying to create new mod {modName}, but {ModName} already exists");

        mod = new ModBase(modName, modAuthor, modVersion);
        ModBase.plugin = plugin;
        mod.Init();
        return mod;
    }

    private void Init()
    {
        configSync = new ConfigSync(ModName)
            { DisplayName = ModName, CurrentVersion = ModVersion, MinimumRequiredVersion = ModVersion };
        plugin.Config.SaveOnConfigSet = false;
        SetupWatcher();
        plugin.Config.ConfigReloaded += (_, _) => UpdateConfiguration();

        serverConfigLocked = config("General", "ServerConfigLock", true, "");
        configSync.AddLockingConfigEntry(serverConfigLocked);
        sendDebugMessagesToHudConfig = config("Debug", "Send debug messages to hud", true, "");

        plugin.Config.SaveOnConfigSet = true;
        plugin.Config.Save();
        harmony.PatchAll();
    }

    public static implicit operator bool(ModBase modBase) { return modBase != null; }

    #region values

    public static string ModName, ModAuthor, ModVersion, ModGUID;
    private readonly Harmony harmony;
    public static ModBase mod;
    public static BaseUnityPlugin plugin;
    public Action OnConfigurationChanged;

    #endregion

    #region Debug

    public static void Debug(object msg)
    {
        plugin.Logger.LogInfo(msg);
        msg = $"[{ModName}] {msg}";
        if (Console.IsVisible()) Console.instance.AddString(msg.ToString());
        if (Player.m_localPlayer && Player.m_debugMode && sendDebugMessagesToHud)
            Player.m_localPlayer.Message(MessageHud.MessageType.TopLeft, msg.ToString());
    }

    public static void DebugError(object msg, bool showWriteToDev = true)
    {
        if (Console.IsVisible()) Console.instance.AddString($"<color=red>[{ModName}] {msg}</color>");
        if (showWriteToDev) msg += "Write to the developer and moderator if this happens often.";
        plugin.Logger.LogError(msg);
    }

    public static void DebugWarning(object msg, bool showWriteToDev = true)
    {
        if (Console.IsVisible()) Console.instance.AddString($"<color=yellow>[{ModName}] {msg}</color>");
        if (showWriteToDev) msg += "Write to the developer and moderator if this happens often.";
        plugin.Logger.LogWarning(msg);
    }

    #endregion

    #region ConfigSettings

    #region Core

    private readonly string ConfigFileName = "-1";
    private DateTime LastConfigChange;
    internal ConfigSync configSync;
    private static ConfigEntry<bool> serverConfigLocked = null!;

    public ConfigEntry<T> config<T>(string group, string name, T value, ConfigDescription description,
        bool synchronizedSetting = true)
    {
        var configEntry = plugin.Config.Bind(group, name, value, description);

        var syncedConfigEntry = configSync.AddConfigEntry(configEntry);
        syncedConfigEntry.SynchronizedConfig = synchronizedSetting;

        return configEntry;
    }

    public ConfigEntry<T> config<T>(string group, string name, T value, string description,
        bool synchronizedSetting = true)
    {
        return config(group, name, value, new ConfigDescription(description), synchronizedSetting);
    }

    public enum Toggle
    {
        On = 1,
        Off = 0
    }

    #endregion

    #region Updating

    private void SetupWatcher()
    {
        FileSystemWatcher fileSystemWatcher = new(Paths.ConfigPath, ConfigFileName);
        fileSystemWatcher.Changed += ConfigChanged;
        fileSystemWatcher.IncludeSubdirectories = true;
        fileSystemWatcher.SynchronizingObject = ThreadingHelper.SynchronizingObject;
        fileSystemWatcher.EnableRaisingEvents = true;
    }

    private void ConfigChanged(object sender, FileSystemEventArgs e)
    {
        if ((DateTime.Now - LastConfigChange).TotalSeconds <= 2) return;
        LastConfigChange = DateTime.Now;

        try
        {
            plugin.Config.Reload();
        }
        catch
        {
            DebugError("Unable reload config");
        }
    }

    private void UpdateConfiguration()
    {
        try
        {
            sendDebugMessagesToHud = sendDebugMessagesToHudConfig.Value;
            OnConfigurationChanged?.Invoke();
            Debug("Configuration Received");
        }
        catch (Exception e)
        {
            DebugError($"Configuration error: {e.Message}", false);
        }
    }

    #endregion

    #endregion
}
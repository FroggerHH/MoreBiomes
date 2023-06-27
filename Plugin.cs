using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ItemManager;
using LocalizationManager;
using ServerSync;
using UnityEngine;
using static Heightmap;

namespace MoreBiomes;

[BepInPlugin(ModGUID, ModName, ModVersion)]
internal class Plugin : BaseUnityPlugin
{
    #region values

    internal const string ModName = "MoreBiomes", ModVersion = "1.0.0", ModGUID = "com.Frogger." + ModName;
    internal static Harmony harmony = new(ModGUID);

    internal static Plugin _self;
    internal static AssetBundle bundleDesert;
    public const Biome desert = (Biome)(1024);

    #endregion

    #region tools

    public static void Debug(string msg)
    {
        _self.Logger.LogInfo(msg);
    }

    public static void DebugError(object msg, bool showWriteToDev)
    {
        if (showWriteToDev)
        {
            msg += "Write to the developer and moderator if this happens often.";
        }

        _self.Logger.LogError(msg);
    }

    public static void DebugWarning(string msg, bool showWriteToDev)
    {
        if (showWriteToDev)
        {
            msg += "Write to the developer and moderator if this happens often.";
        }

        _self.Logger.LogWarning(msg);
    }

    #endregion

    #region ConfigSettings

    #region tools

    static string ConfigFileName = $"com.Frogger.{ModName}.cfg";
    DateTime LastConfigChange;

    public static readonly ConfigSync configSync = new(ModName)
        { DisplayName = ModName, CurrentVersion = ModVersion, MinimumRequiredVersion = ModVersion };

    public static ConfigEntry<T> config<T>(string group, string name, T value, ConfigDescription description,
        bool synchronizedSetting = true)
    {
        ConfigEntry<T> configEntry = _self.Config.Bind(group, name, value, description);

        SyncedConfigEntry<T> syncedConfigEntry = configSync.AddConfigEntry(configEntry);
        syncedConfigEntry.SynchronizedConfig = synchronizedSetting;

        return configEntry;
    }

    private ConfigEntry<T> config<T>(string group, string name, T value, string description,
        bool synchronizedSetting = true)
    {
        return config(group, name, value, new ConfigDescription(description), synchronizedSetting);
    }

    void SetCfgValue<T>(Action<T> setter, ConfigEntry<T> config)
    {
        setter(config.Value);
        config.SettingChanged += (_, _) => setter(config.Value);
    }

    public enum Toggle
    {
        On = 1,
        Off = 0
    }

    #endregion

    #region configs

    #endregion

    #endregion

    #region Config

    private void SetupWatcher()
    {
        FileSystemWatcher fileSystemWatcher = new(Paths.ConfigPath, ConfigFileName);
        fileSystemWatcher.Changed += ConfigChanged;
        fileSystemWatcher.IncludeSubdirectories = true;
        fileSystemWatcher.SynchronizingObject = ThreadingHelper.SynchronizingObject;
        fileSystemWatcher.EnableRaisingEvents = true;
    }

    void ConfigChanged(object sender, FileSystemEventArgs e)
    {
        if ((DateTime.Now - LastConfigChange).TotalSeconds <= 5.0)
        {
            return;
        }

        LastConfigChange = DateTime.Now;
        try
        {
            Config.Reload();
        }
        catch
        {
            DebugError("Can't reload Config", true);
        }
    }

    private void UpdateConfiguration()
    {
        Debug("Configuration Received");
    }

    #endregion

    // static List<Biome> indexToBiome = new();
    private void Awake()
    {
        _self = this;

        #region Config

        configSync.AddLockingConfigEntry(config("Main", "Lock Configuration", Toggle.On,
            "If on, the configuration is locked and can be changed by server admins only."));

        #endregion

        bundleDesert = PrefabManager.RegisterAssetBundle("desert");

        Localizer.Load();
        harmony.PatchAll();

        foreach (Material? material in bundleDesert.LoadAllAssets<Material>())
        {
            if (!material) continue;
            material.shader = Shader.Find(material.shader.name);
        }

        SetupBiomeArrays();
    }

    static readonly float[] biomeWeights = new float[30];

    public static void SetupBiomeArrays()
    {
        var indexToBiome = s_indexToBiome.ToList();
        indexToBiome.Add((Biome)(1024));
        var indexToBiomeArray = indexToBiome.ToArray();
        unsafe
        {
            fixed (void* ptr = &Heightmap.s_indexToBiome)
                *(object*)ptr = indexToBiomeArray;
            fixed (void* ptr = &Heightmap.s_tempBiomeWeights)
                *(object*)ptr = biomeWeights;
        }

        s_biomeToIndex.Add(desert, 10);
    }
}
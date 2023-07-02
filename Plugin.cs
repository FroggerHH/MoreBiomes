using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ItemManager;
using LocalizationManager;
using LocationManager;
using ServerSync;
using UnityEngine;
using static Heightmap;
using Location = LocationManager.Location;

namespace MoreBiomes;

[BepInPlugin(ModGUID, ModName, ModVersion)]
internal class Plugin : BaseUnityPlugin
{
    #region values

    internal const string ModName = "MoreBiomes", ModVersion = "1.0.0", ModGUID = "com.Frogger." + ModName;
    internal static Harmony harmony = new(ModGUID);
    internal static Plugin _self;
    internal static AssetBundle bundleDesert;
    internal static AssetBundle bundleJungle;

    #endregion

    #region tools

    public static void Debug(string msg)
    {
        _self.Logger.LogInfo(msg);
    }

    public static void DebugError(object msg, bool showWriteToDev = true)
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

    private void Awake()
    {
        _self = this;
        SetupBiomeArrays();
        harmony.PatchAll();

        #region Config

        configSync.AddLockingConfigEntry(config("Main", "Lock Configuration", Toggle.On,
            "If on, the configuration is locked and can be changed by server admins only."));

        #endregion

        bundleDesert = PrefabManager.RegisterAssetBundle("desert");
        bundleJungle = PrefabManager.RegisterAssetBundle("jungle");

        LocationManager.Location Bones = new(bundleDesert, "Bones")
        {
            Biome = Const.Desert,
            Rotation = Rotation.Random,
            PreferCenter = true,
            SpawnArea = BiomeArea.Everything,
            SnapToWater = false,
            MinimumDistanceFromGroup = 25,
            Count = 150,
            MapIcon = "bones",
            ShowMapIcon = ShowIcon.Always,
            GroupName = "DesertBones",
            SpawnAltitude = new(10, 100000)
        };
        LocationManager.Location Oasis = new(bundleDesert, "Oasis")
        {
            Biome = Const.Desert,
            Rotation = Rotation.Fixed,
            PreferCenter = true,
            SnapToWater = false,
            MinimumDistanceFromGroup = 200,
            Count = 25,
            MapIcon = "Oasis",
            ShowMapIcon = ShowIcon.Always,
            GroupName = "DesertOasis",
            SpawnAltitude = new(10, 100000)
        };


        LocationManager.Location ancienttemple = new(bundleJungle, "ancient-temple")
        {
            Biome = Const.Jungle,
            Rotation = Rotation.Random,
            PreferCenter = true,
            SnapToWater = false,
            MinimumDistanceFromGroup = 80,
            Count = 25,
            MapIcon = "ancient-temple",
            ShowMapIcon = ShowIcon.Always,
            GroupName = "ancient-temple",
            SpawnAltitude = new(20, 100000)
        };
        LocationManager.Location Columns_ruins = new(bundleJungle, "Columns_ruins")
        {
            Biome = Const.Jungle,
            Rotation = Rotation.Fixed,
            PreferCenter = true,
            SpawnArea = BiomeArea.Everything,
            SnapToWater = false,
            MinimumDistanceFromGroup = 60,
            Count = 40,
            MapIcon = "Columns_ruins",
            ShowMapIcon = ShowIcon.Always,
            GroupName = "Columns_ruins",
            SpawnAltitude = new(10, 100000)
        };
        LocationManager.Location DesertRuins = new(bundleJungle, "DesertRuins")
        {
            Biome = Const.Desert,
            Rotation = Rotation.Fixed,
            PreferCenter = true,
            SpawnArea = BiomeArea.Everything,
            SnapToWater = false,
            MinimumDistanceFromGroup = 60,
            Count = 40,
            MapIcon = "DesertRuins",
            ShowMapIcon = ShowIcon.Always,
            GroupName = "DesertRuins",
            SpawnAltitude = new(10, 100000)
        };

        PrefabManager.RegisterPrefab(bundleJungle, "TreasureChest_DesertRuins");
        PrefabManager.RegisterPrefab(bundleJungle, "TreasureChest_ancient-temple");
        PrefabManager.RegisterPrefab(bundleJungle, "column_ruins");

        Localizer.Load();
    }

    static readonly float[] biomeWeights = new float[30];

    public static void SetupBiomeArrays()
    {
        var indexToBiome = s_indexToBiome.ToList();
        indexToBiome.Add(Const.Desert);
        indexToBiome.Add(Const.Jungle);
        var indexToBiomeArray = indexToBiome.ToArray();
        unsafe
        {
            fixed (void* ptr = &Heightmap.s_indexToBiome)
                *(object*)ptr = indexToBiomeArray;
            fixed (void* ptr = &Heightmap.s_tempBiomeWeights)
                *(object*)ptr = biomeWeights;
        }

        s_biomeToIndex.Add(Const.Desert, 10);
        s_biomeToIndex.Add(Const.Jungle, 10);
    }
}
using BepInEx;
using BepInEx.Configuration;
using ItemManager;
using LocalizationManager;
using LocationManager;

namespace MoreBiomes;

[BepInPlugin(ModGUID, ModName, ModVersion)]
internal class Plugin : BaseUnityPlugin
{
    internal const string ModName = "MoreBiomes",
        ModVersion = "1.1.0",
        ModGUID = $"com.{ModAuthor}.{ModName}",
        ModAuthor = "Frogger";

    internal static AssetBundle bundleDesert;
    internal static AssetBundle bundleJungle;
    private static readonly float[] biomeWeights = new float[30];

  //  public static ConfigEntry<bool> advancedMapTooltip;

    private void Awake()
    {
        CreateMod(this, ModName, ModAuthor, ModVersion);
        SetupBiomeArrays();
       // advancedMapTooltip = mod.config("General", "Advanced map tooltip", false, "");

        bundleDesert = PrefabManager.RegisterAssetBundle("desert");
        bundleJungle = PrefabManager.RegisterAssetBundle("jungle");

        #region Staff

        LocationManager.Location Bones = new(bundleDesert, "Bones")
        {
            Biome = Desert,
            Rotation = Rotation.Random,
            PreferCenter = true,
            SpawnArea = BiomeArea.Everything,
            SnapToWater = false,
            MinimumDistanceFromGroup = 25,
            Count = 150,
            // MapIcon = "bones",
            // ShowMapIcon = ShowIcon.Always,
            GroupName = "DesertBones",
            SpawnAltitude = new Range(10, 100000)
        };
        LocationManager.Location Oasis = new(bundleDesert, "Oasis")
        {
            Biome = Desert,
            Rotation = Rotation.Fixed,
            PreferCenter = true,
            SnapToWater = false,
            MinimumDistanceFromGroup = 200,
            Count = 25,
            //MapIcon = "Oasis",
            //ShowMapIcon = ShowIcon.Always,
            GroupName = "DesertOasis",
            SpawnAltitude = new Range(10, 100000)
        };
        LocationManager.Location ancienttemple = new(bundleJungle, "ancient-temple")
        {
            Biome = Jungle,
            Rotation = Rotation.Random,
            PreferCenter = true,
            SnapToWater = false,
            MinimumDistanceFromGroup = 80,
            Count = 25,
            //MapIcon = "ancient-temple",
            //ShowMapIcon = ShowIcon.Always,
            GroupName = "ancient-temple",
            SpawnAltitude = new Range(20, 100000)
        };
        LocationManager.Location Columns_ruins = new(bundleJungle, "Columns_ruins")
        {
            Biome = Jungle,
            Rotation = Rotation.Fixed,
            PreferCenter = true,
            SpawnArea = BiomeArea.Everything,
            SnapToWater = false,
            MinimumDistanceFromGroup = 60,
            Count = 40,
            //MapIcon = "Columns_ruins",
            //ShowMapIcon = ShowIcon.Always,
            GroupName = "Columns_ruins",
            SpawnAltitude = new Range(10, 100000)
        };
        LocationManager.Location DesertRuins = new(bundleJungle, "DesertRuins")
        {
            Biome = Desert,
            Rotation = Rotation.Fixed,
            PreferCenter = true,
            SpawnArea = BiomeArea.Everything,
            SnapToWater = false,
            MinimumDistanceFromGroup = 60,
            Count = 40,
            //MapIcon = "DesertRuins",
            // ShowMapIcon = ShowIcon.Always,
            GroupName = "DesertRuins",
            SpawnAltitude = new Range(10, 100000)
        };
        PrefabManager.RegisterPrefab(bundleJungle, "TreasureChest_DesertRuins");
        PrefabManager.RegisterPrefab(bundleJungle, "TreasureChest_ancient-temple");
        PrefabManager.RegisterPrefab(bundleJungle, "column_ruins");
        Localizer.Load();

        #endregion
    }

    public static void SetupBiomeArrays()
    {
        var indexToBiome = s_indexToBiome.ToList();
        indexToBiome.Add(Desert);
        indexToBiome.Add(Jungle);
        indexToBiome.Add(Canyon);
        var indexToBiomeArray = indexToBiome.ToArray();
        unsafe
        {
            fixed (void* ptr = &s_indexToBiome)
            {
                *(object*)ptr = indexToBiomeArray;
            }

            fixed (void* ptr = &s_tempBiomeWeights)
            {
                *(object*)ptr = biomeWeights;
            }
        }

        s_biomeToIndex.Add(Desert, 10);
        s_biomeToIndex.Add(Jungle, 11);
        s_biomeToIndex.Add(Canyon, 12);
    }
}
using ItemManager;

namespace MoreBiomes;

[HarmonyPatch]
public class Vegetation
{
    [HarmonyPatch(typeof(ZoneSystem), nameof(ZoneSystem.Awake))] [HarmonyPostfix]
    public static void Patch(ZoneSystem __instance)
    {
        var Birch1 = __instance.m_vegetation.Find(x => x.m_prefab.name == "Birch1");
        var Birch2 = __instance.m_vegetation.Find(x => x.m_prefab.name == "Birch2");
        var FirTree_small_dead = __instance.m_vegetation.Find(x => x.m_prefab.name == "FirTree_small_dead");
        var Bush02_en = __instance.m_vegetation.Find(x => x.m_prefab.name == "Bush02_en");
        var RaspberryBush = __instance.m_vegetation.Find(x => x.m_prefab.name == "RaspberryBush");
        var FirTree_oldLog = __instance.m_vegetation.Find(x => x.m_prefab.name == "FirTree_oldLog");
        var stubbe = __instance.m_vegetation.Find(x => x.m_prefab.name == "stubbe");
        var randomTreeVegetation = Random.Range(0, 100) > 45 ? Birch1 : Birch2;

        #region Desert

        __instance.m_vegetation.Add(new ZoneVegetation
        {
            m_name = "PalmTree",
            m_biome = Desert,
            m_enable = true,
            m_prefab = PrefabManager.RegisterPrefab(bundleDesert, "PalmTree"),
            m_inForest = true,
            m_forestTresholdMin = randomTreeVegetation.m_forestTresholdMin,
            m_forestTresholdMax = randomTreeVegetation.m_forestTresholdMax,
            m_foldout = randomTreeVegetation.m_foldout,
            m_snapToWater = false,
            m_maxVegetation = randomTreeVegetation.m_maxVegetation,
            m_max = randomTreeVegetation.m_max,
            m_min = randomTreeVegetation.m_min,
            m_blockCheck = randomTreeVegetation.m_blockCheck,
            m_snapToStaticSolid = randomTreeVegetation.m_snapToStaticSolid,
            m_chanceToUseGroundTilt = randomTreeVegetation.m_chanceToUseGroundTilt,
            m_minAltitude = 2
        });
        var randomVegetation = Random.Range(0, 100) > 85
            ? Random.Range(0, 100) > 45 ? Birch1 : Bush02_en
            : RaspberryBush;
        __instance.m_vegetation.Add(new ZoneVegetation
        {
            //m_name = "DesertPlant" + (i == 0 ? "" : i),
            m_name = "DesertPlant",
            m_biome = Desert,
            m_enable = true,
            //m_prefab = PrefabManager.RegisterPrefab(bundleDesert, "DesertPlant" + (i == 0 ? "" : $" {i}")),
            m_prefab = PrefabManager.RegisterPrefab(bundleDesert, "DesertPlant"),
            m_inForest = true,
            m_forestTresholdMin = randomVegetation.m_forestTresholdMin,
            m_forestTresholdMax = randomVegetation.m_forestTresholdMax,
            m_foldout = randomVegetation.m_foldout,
            m_snapToWater = false,
            m_maxVegetation = randomVegetation.m_maxVegetation,
            m_max = randomVegetation.m_max,
            m_min = randomVegetation.m_min,
            m_blockCheck = randomVegetation.m_blockCheck,
            m_snapToStaticSolid = randomVegetation.m_snapToStaticSolid,
            m_chanceToUseGroundTilt = randomVegetation.m_chanceToUseGroundTilt,
            m_minAltitude = 2
        });

        for (var i = 0; i < 3; i++)
        {
            var randomWitheredTree = Random.Range(0, 100) > 45 ? Bush02_en : RaspberryBush;
            __instance.m_vegetation.Add(new ZoneVegetation
            {
                m_name = "WitheredTree" + (i == 0 ? "" : i),
                m_biome = Desert,
                m_enable = true,
                m_prefab = PrefabManager.RegisterPrefab(bundleDesert, "WitheredTree" + (i == 0 ? "" : $" {i}")),
                m_inForest = true,
                m_forestTresholdMin = randomWitheredTree.m_forestTresholdMin,
                m_forestTresholdMax = randomWitheredTree.m_forestTresholdMax,
                m_foldout = randomWitheredTree.m_foldout,
                m_snapToWater = randomWitheredTree.m_snapToWater,
                m_maxVegetation = randomWitheredTree.m_maxVegetation,
                m_max = randomWitheredTree.m_max,
                m_min = randomWitheredTree.m_min,
                m_blockCheck = randomWitheredTree.m_blockCheck,
                m_snapToStaticSolid = randomWitheredTree.m_snapToStaticSolid,
                m_chanceToUseGroundTilt = randomWitheredTree.m_chanceToUseGroundTilt,
                m_minAltitude = 2
            });
        }

        __instance.m_vegetation.Add(new ZoneVegetation
        {
            m_name = "Сactuse",
            m_biome = Desert,
            m_enable = true,
            m_prefab = PrefabManager.RegisterPrefab(bundleDesert, "Сactuse"),
            m_inForest = true,
            m_forestTresholdMin = RaspberryBush.m_forestTresholdMin,
            m_forestTresholdMax = RaspberryBush.m_forestTresholdMax,
            m_foldout = RaspberryBush.m_foldout,
            m_snapToWater = RaspberryBush.m_snapToWater,
            m_maxVegetation = RaspberryBush.m_maxVegetation * 2f,
            m_max = RaspberryBush.m_max * 2f,
            m_min = RaspberryBush.m_min * 1.1f,
            m_blockCheck = RaspberryBush.m_blockCheck,
            m_snapToStaticSolid = RaspberryBush.m_snapToStaticSolid,
            m_chanceToUseGroundTilt = RaspberryBush.m_chanceToUseGroundTilt,
            m_minAltitude = 2
        });

        __instance.m_vegetation.Add(new ZoneVegetation
        {
            m_name = "LogDesert",
            m_biome = Desert,
            m_enable = true,
            m_prefab = PrefabManager.RegisterPrefab(bundleDesert, "LogDesert"),
            m_inForest = true,
            m_forestTresholdMin = FirTree_oldLog.m_forestTresholdMin,
            m_forestTresholdMax = FirTree_oldLog.m_forestTresholdMax,
            m_foldout = FirTree_oldLog.m_foldout,
            m_snapToWater = FirTree_oldLog.m_snapToWater,
            m_maxVegetation = FirTree_oldLog.m_maxVegetation * 2f,
            m_max = FirTree_oldLog.m_max * 2f,
            m_min = FirTree_oldLog.m_min * 1.1f,
            m_blockCheck = FirTree_oldLog.m_blockCheck,
            m_snapToStaticSolid = FirTree_oldLog.m_snapToStaticSolid,
            m_chanceToUseGroundTilt = FirTree_oldLog.m_chanceToUseGroundTilt,
            m_minAltitude = 2
        });

        #endregion

        #region Jungle

        __instance.m_vegetation.Add(new ZoneVegetation
        {
            m_name = "JungleStubbe",
            m_biome = Jungle,
            m_enable = true,
            m_prefab = PrefabManager.RegisterPrefab(bundleJungle, "JungleStubbe"),
            m_inForest = true,
            m_forestTresholdMin = stubbe.m_forestTresholdMin,
            m_forestTresholdMax = stubbe.m_forestTresholdMax,
            m_foldout = stubbe.m_foldout,
            m_snapToWater = false,
            m_maxVegetation = stubbe.m_maxVegetation * 1.1f,
            m_max = stubbe.m_max * 1.1f,
            m_min = stubbe.m_min * 1.1f,
            m_blockCheck = stubbe.m_blockCheck,
            m_snapToStaticSolid = stubbe.m_snapToStaticSolid,
            m_chanceToUseGroundTilt = stubbe.m_chanceToUseGroundTilt,
            m_minAltitude = 2
        });
        __instance.m_vegetation.Add(new ZoneVegetation
        {
            m_name = "Jungle_Rafflesia",
            m_biome = Jungle,
            m_enable = true,
            m_prefab = PrefabManager.RegisterPrefab(bundleJungle, "Jungle_Rafflesia"),
            m_inForest = true,
            m_forestTresholdMin = stubbe.m_forestTresholdMin,
            m_forestTresholdMax = stubbe.m_forestTresholdMax,
            m_foldout = stubbe.m_foldout,
            m_snapToWater = false,
            m_maxVegetation = stubbe.m_maxVegetation * 1.2f,
            m_max = stubbe.m_max * 1.2f,
            m_min = stubbe.m_min * 1.2f,
            m_blockCheck = true,
            m_snapToStaticSolid = stubbe.m_snapToStaticSolid,
            m_chanceToUseGroundTilt = stubbe.m_chanceToUseGroundTilt,
            m_minAltitude = 2,
            m_scaleMax = 1.5f,
            m_scaleMin = 0.7f
        });

        __instance.m_vegetation.Add(new ZoneVegetation
        {
            m_name = "JungleBamboo",
            m_biome = Jungle,
            m_enable = true,
            m_prefab = PrefabManager.RegisterPrefab(bundleJungle, "JungleBamboo"),
            m_inForest = true,
            m_forestTresholdMin = randomTreeVegetation.m_forestTresholdMin,
            m_forestTresholdMax = randomTreeVegetation.m_forestTresholdMax,
            m_foldout = randomTreeVegetation.m_foldout,
            m_snapToWater = false,
            m_maxVegetation = randomTreeVegetation.m_maxVegetation,
            m_max = randomTreeVegetation.m_max,
            m_min = randomTreeVegetation.m_min,
            m_blockCheck = true,
            m_snapToStaticSolid = false,
            m_chanceToUseGroundTilt = randomTreeVegetation.m_chanceToUseGroundTilt,
            m_minAltitude = 2,
            m_groundOffset = -5
        });
        __instance.m_vegetation.Add(new ZoneVegetation
        {
            m_name = "JungleBananaTree",
            m_biome = Jungle,
            m_enable = true,
            m_prefab = PrefabManager.RegisterPrefab(bundleJungle, "JungleBananaTree"),
            m_inForest = true,
            m_forestTresholdMin = randomTreeVegetation.m_forestTresholdMin,
            m_forestTresholdMax = randomTreeVegetation.m_forestTresholdMax,
            m_foldout = randomTreeVegetation.m_foldout,
            m_snapToWater = false,
            m_maxVegetation = randomTreeVegetation.m_maxVegetation / 1.5f,
            m_max = randomTreeVegetation.m_max / 1.5f,
            m_min = randomTreeVegetation.m_min / 1.5f,
            m_blockCheck = true,
            m_snapToStaticSolid = false,
            m_chanceToUseGroundTilt = randomTreeVegetation.m_chanceToUseGroundTilt,
            m_minAltitude = 1
        });

        // __instance.m_vegetation.Add(new()
        // {
        //     m_name = "JungleTreeSlant",
        //     m_biome = Const.Jungle,
        //     m_enable = true,
        //     m_prefab = PrefabManager.RegisterPrefab(bundleJungle, "JungleTreeSlant"),
        //     m_inForest = true,
        //     m_forestTresholdMin = randomTreeVegetation.m_forestTresholdMin,
        //     m_forestTresholdMax = randomTreeVegetation.m_forestTresholdMax,
        //     m_foldout = randomTreeVegetation.m_foldout,
        //     m_snapToWater = false,
        //     m_maxVegetation = randomTreeVegetation.m_maxVegetation + 50,
        //     m_max = randomTreeVegetation.m_max + 40,
        //     m_min = randomTreeVegetation.m_min + 10,
        //     m_blockCheck = randomTreeVegetation.m_blockCheck,
        //     m_snapToStaticSolid = randomTreeVegetation.m_snapToStaticSolid,
        //     m_chanceToUseGroundTilt = randomTreeVegetation.m_chanceToUseGroundTilt,
        //     m_minAltitude = 2
        // });
        //
        // __instance.m_vegetation.Add(new()
        // {
        //     m_name = "JungleTree1",
        //     m_biome = Const.Jungle,
        //     m_enable = true,
        //     m_prefab = PrefabManager.RegisterPrefab(bundleJungle, "JungleTree1"),
        //     m_inForest = true,
        //     m_forestTresholdMin = randomTreeVegetation.m_forestTresholdMin,
        //     m_forestTresholdMax = randomTreeVegetation.m_forestTresholdMax,
        //     m_foldout = randomTreeVegetation.m_foldout,
        //     m_snapToWater = false,
        //     m_maxVegetation = randomTreeVegetation.m_maxVegetation + 50,
        //     m_max = randomTreeVegetation.m_max + 40,
        //     m_min = randomTreeVegetation.m_min + 10,
        //     m_blockCheck = randomTreeVegetation.m_blockCheck,
        //     m_snapToStaticSolid = randomTreeVegetation.m_snapToStaticSolid,
        //     m_chanceToUseGroundTilt = randomTreeVegetation.m_chanceToUseGroundTilt,
        //     m_minAltitude = 2
        // });
        //
        // __instance.m_vegetation.Add(new()
        // {
        //     m_name = "JungleBananaTree",
        //     m_biome = Const.Jungle,
        //     m_enable = true,
        //     m_prefab = PrefabManager.RegisterPrefab(bundleJungle, "JungleBananaTree"),
        //     m_inForest = true,
        //     m_forestTresholdMin = FirTree_small_dead.m_forestTresholdMin,
        //     m_forestTresholdMax = FirTree_small_dead.m_forestTresholdMax,
        //     m_foldout = FirTree_small_dead.m_foldout,
        //     m_snapToWater = false,
        //     m_maxVegetation = FirTree_small_dead.m_maxVegetation + 20,
        //     m_max = FirTree_small_dead.m_max + 10,
        //     m_min = FirTree_small_dead.m_min,
        //     m_blockCheck = FirTree_small_dead.m_blockCheck,
        //     m_snapToStaticSolid = FirTree_small_dead.m_snapToStaticSolid,
        //     m_chanceToUseGroundTilt = FirTree_small_dead.m_chanceToUseGroundTilt,
        //     m_minAltitude = 2
        // });
        // __instance.m_vegetation.Add(new()
        // {
        //     m_name = "Jungletrees",
        //     m_biome = Const.Jungle,
        //     m_enable = true,
        //     m_prefab = PrefabManager.RegisterPrefab(bundleJungle, "Jungletrees"),
        //     m_inForest = true,
        //     m_forestTresholdMin = FirTree_small_dead.m_forestTresholdMin,
        //     m_forestTresholdMax = FirTree_small_dead.m_forestTresholdMax,
        //     m_foldout = FirTree_small_dead.m_foldout,
        //     m_snapToWater = false,
        //     m_maxVegetation = FirTree_small_dead.m_maxVegetation + 50,
        //     m_max = FirTree_small_dead.m_max + 40,
        //     m_min = FirTree_small_dead.m_min + 10,
        //     m_blockCheck = FirTree_small_dead.m_blockCheck,
        //     m_snapToStaticSolid = FirTree_small_dead.m_snapToStaticSolid,
        //     m_chanceToUseGroundTilt = FirTree_small_dead.m_chanceToUseGroundTilt,
        //     m_minAltitude = 2
        // });

        #endregion
    }
}
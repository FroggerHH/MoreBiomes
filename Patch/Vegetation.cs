using System.Collections.Generic;
using HarmonyLib;
using ItemManager;
using UnityEngine;
using static MoreBiomes.Plugin;
using static Heightmap;
using static Heightmap.Biome;
using static ZoneSystem;
using static ZoneSystem.ZoneVegetation;

namespace MoreBiomes;

[HarmonyPatch]
public class Vegetation
{
    private static ZoneVegetation Bush02_en;
    private static ZoneVegetation RaspberryBush;
    
    [HarmonyPatch(typeof(ZoneSystem), nameof(ZoneSystem.Awake)), HarmonyPostfix]
    public static void GetBiomeHeight(ZoneSystem __instance)
    {
        Bush02_en = __instance.m_vegetation.Find(x => x.m_prefab.name == "Bush02_en");
        RaspberryBush = __instance.m_vegetation.Find(x => x.m_prefab.name == "RaspberryBush");
        
        __instance.m_vegetation.Add(new ()
        {
            m_name = "PalmTree",
            m_biome = desert,
            m_enable = true,
            m_prefab = PrefabManager.RegisterPrefab(bundleDesert, "PalmTree"),
            m_inForest = true,
            m_forestTresholdMin = Bush02_en.m_forestTresholdMin,
            m_forestTresholdMax = Bush02_en.m_forestTresholdMax,
            m_foldout = Bush02_en.m_foldout,
            m_snapToWater = Bush02_en.m_snapToWater,
            m_maxVegetation = Bush02_en.m_maxVegetation,
            m_max = Bush02_en.m_max,
            m_min = Bush02_en.m_min,
            m_blockCheck = Bush02_en.m_blockCheck,
            m_snapToStaticSolid = Bush02_en.m_snapToStaticSolid,
            m_chanceToUseGroundTilt = Bush02_en.m_chanceToUseGroundTilt
        });
        __instance.m_vegetation.Add(new ()
        {
            m_name = "DesertPlant",
            m_biome = desert,
            m_enable = true,
            m_prefab = PrefabManager.RegisterPrefab(bundleDesert, "DesertPlant"),
            m_inForest = true,
            m_forestTresholdMin = RaspberryBush.m_forestTresholdMin,
            m_forestTresholdMax = RaspberryBush.m_forestTresholdMax,
            m_foldout = RaspberryBush.m_foldout,
            m_snapToWater = RaspberryBush.m_snapToWater,
            m_maxVegetation = RaspberryBush.m_maxVegetation,
            m_max = RaspberryBush.m_max,
            m_min = RaspberryBush.m_min,
            m_blockCheck = RaspberryBush.m_blockCheck,
            m_snapToStaticSolid = RaspberryBush.m_snapToStaticSolid,
            m_chanceToUseGroundTilt = RaspberryBush.m_chanceToUseGroundTilt
        });
    }
}
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
public class Grass
{
    [HarmonyPatch(typeof(ClutterSystem), nameof(ClutterSystem.Awake)), HarmonyPostfix]
    public static void ClutterSystem_(ClutterSystem __instance)
    {
        var find = __instance.m_clutter.Find(x => x.m_prefab.name == "instanced_heathgrass");
        __instance.m_clutter.Add(new()
        {
            m_biome = desert,
            m_prefab = bundleDesert.LoadAsset<GameObject>("instanced_heathgrass_desert"),
            m_amount = find.m_amount,
            m_enabled = true,
            m_instanced = find.m_instanced,
            m_name = "instanced_heathgrass_desert",
            m_fractalOffset = find.m_fractalOffset,
            m_fractalScale = find.m_fractalScale,
            m_inForest = find.m_inForest,
            m_maxAlt = find.m_maxAlt,
            m_maxTilt = find.m_maxTilt,
            m_maxVegetation = find.m_maxVegetation,
            m_minAlt = find.m_minAlt,
            m_minTilt = find.m_minTilt,
            m_minVegetation = find.m_minVegetation,
            m_onCleared = find.m_onCleared,
            m_onUncleared = find.m_onUncleared,
            m_randomOffset = find.m_randomOffset,
            m_scaleMax = find.m_scaleMax,
            m_scaleMin = find.m_scaleMin,
            m_terrainTilt = find.m_terrainTilt,
            m_forestTresholdMax = find.m_forestTresholdMax,
            m_forestTresholdMin = find.m_forestTresholdMin,
            m_fractalTresholdMax = find.m_fractalTresholdMax,
            m_fractalTresholdMin = find.m_fractalTresholdMin,
            m_maxOceanDepth = find.m_maxOceanDepth,
            m_minOceanDepth = find.m_minOceanDepth,
            m_snapToWater = find.m_snapToWater
        });
    }
}
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
    private static ClutterSystem.Clutter instanced_heathgrass;
    private static ClutterSystem.Clutter instanced_heathflowers;
    private static ClutterSystem.Clutter instanced_vass;

    [HarmonyPatch(typeof(ClutterSystem), nameof(ClutterSystem.Awake)), HarmonyPostfix]
    public static void ClutterSystem_(ClutterSystem __instance)
    {
        instanced_heathgrass = __instance.m_clutter.Find(x => x.m_prefab.name == "instanced_heathgrass");
        instanced_heathflowers = __instance.m_clutter.Find(x => x.m_prefab.name == "instanced_heathflowers");
        instanced_vass = __instance.m_clutter.Find(x => x.m_prefab.name == "instanced_vass");

        #region Desert

        __instance.m_clutter.Add(new()
        {
            m_biome = Const.Desert,
            m_prefab = bundleDesert.LoadAsset<GameObject>("instanced_grass_desert"),
            m_amount = instanced_heathgrass.m_amount,
            m_enabled = true,
            m_instanced = instanced_heathgrass.m_instanced,
            m_name = "instanced_grass_desert",
            m_fractalOffset = instanced_heathgrass.m_fractalOffset,
            m_fractalScale = instanced_heathgrass.m_fractalScale,
            m_inForest = instanced_heathgrass.m_inForest,
            m_maxAlt = instanced_heathgrass.m_maxAlt,
            m_maxTilt = instanced_heathgrass.m_maxTilt,
            m_maxVegetation = instanced_heathgrass.m_maxVegetation,
            m_minAlt = instanced_heathgrass.m_minAlt,
            m_minTilt = instanced_heathgrass.m_minTilt,
            m_minVegetation = instanced_heathgrass.m_minVegetation,
            m_onCleared = instanced_heathgrass.m_onCleared,
            m_onUncleared = instanced_heathgrass.m_onUncleared,
            m_randomOffset = instanced_heathgrass.m_randomOffset,
            m_scaleMax = instanced_heathgrass.m_scaleMax,
            m_scaleMin = instanced_heathgrass.m_scaleMin,
            m_terrainTilt = instanced_heathgrass.m_terrainTilt,
            m_forestTresholdMax = instanced_heathgrass.m_forestTresholdMax,
            m_forestTresholdMin = instanced_heathgrass.m_forestTresholdMin,
            m_fractalTresholdMax = instanced_heathgrass.m_fractalTresholdMax,
            m_fractalTresholdMin = instanced_heathgrass.m_fractalTresholdMin,
            m_maxOceanDepth = instanced_heathgrass.m_maxOceanDepth,
            m_minOceanDepth = instanced_heathgrass.m_minOceanDepth,
            m_snapToWater = instanced_heathgrass.m_snapToWater
        });

        __instance.m_clutter.Add(new()
        {
            m_biome = Const.Desert,
            m_prefab = bundleDesert.LoadAsset<GameObject>("instanced_seaweed"),
            m_amount = instanced_vass.m_amount,
            m_enabled = true,
            m_instanced = instanced_vass.m_instanced,
            m_name = "instanced_seaweed",
            m_fractalOffset = instanced_vass.m_fractalOffset,
            m_fractalScale = instanced_vass.m_fractalScale,
            m_inForest = instanced_vass.m_inForest,
            m_maxAlt = instanced_vass.m_maxAlt,
            m_maxTilt = instanced_vass.m_maxTilt,
            m_maxVegetation = instanced_vass.m_maxVegetation,
            m_minAlt = -5,
            m_minTilt = instanced_vass.m_minTilt,
            m_minVegetation = instanced_vass.m_minVegetation,
            m_onCleared = instanced_vass.m_onCleared,
            m_onUncleared = instanced_vass.m_onUncleared,
            m_randomOffset = instanced_vass.m_randomOffset,
            m_scaleMax = instanced_vass.m_scaleMax,
            m_scaleMin = instanced_vass.m_scaleMin,
            m_terrainTilt = instanced_vass.m_terrainTilt,
            m_forestTresholdMax = instanced_vass.m_forestTresholdMax,
            m_forestTresholdMin = instanced_vass.m_forestTresholdMin,
            m_fractalTresholdMax = instanced_vass.m_fractalTresholdMax,
            m_fractalTresholdMin = instanced_vass.m_fractalTresholdMin,
            m_maxOceanDepth = instanced_vass.m_maxOceanDepth,
            m_minOceanDepth = instanced_vass.m_minOceanDepth,
            m_snapToWater = instanced_vass.m_snapToWater
        });

        // for (int i = 0; i < 3; i++)
        // {
        __instance.m_clutter.Add(new()
        {
            m_biome = Const.Desert,
            //m_prefab = bundleDesert.LoadAsset<GameObject>("instanced_plant_desert" + (i == 0 ? "" : $" {i}")),
            m_prefab = bundleDesert.LoadAsset<GameObject>("instanced_plant_desert"),
            m_amount = instanced_heathflowers.m_amount,
            m_enabled = true,
            m_instanced = instanced_heathflowers.m_instanced,
            //m_name = "instanced_plant_desert" + (i == 0 ? "" : $" {i}"),
            m_name = "instanced_plant_desert",
            m_fractalOffset = instanced_heathflowers.m_fractalOffset,
            m_fractalScale = instanced_heathflowers.m_fractalScale,
            m_inForest = instanced_heathflowers.m_inForest,
            m_maxAlt = instanced_heathflowers.m_maxAlt,
            m_maxTilt = instanced_heathflowers.m_maxTilt,
            m_maxVegetation = instanced_heathflowers.m_maxVegetation,
            m_minAlt = instanced_heathflowers.m_minAlt,
            m_minTilt = instanced_heathflowers.m_minTilt,
            m_minVegetation = instanced_heathflowers.m_minVegetation,
            m_onCleared = instanced_heathflowers.m_onCleared,
            m_onUncleared = instanced_heathflowers.m_onUncleared,
            m_randomOffset = instanced_heathflowers.m_randomOffset,
            m_scaleMax = instanced_heathflowers.m_scaleMax,
            m_scaleMin = instanced_heathflowers.m_scaleMin,
            m_terrainTilt = instanced_heathflowers.m_terrainTilt,
            m_forestTresholdMax = instanced_heathflowers.m_forestTresholdMax,
            m_forestTresholdMin = instanced_heathflowers.m_forestTresholdMin,
            m_fractalTresholdMax = instanced_heathflowers.m_fractalTresholdMax,
            m_fractalTresholdMin = instanced_heathflowers.m_fractalTresholdMin,
            m_maxOceanDepth = instanced_heathflowers.m_maxOceanDepth,
            m_minOceanDepth = instanced_heathflowers.m_minOceanDepth,
            m_snapToWater = instanced_heathflowers.m_snapToWater
        });
        //}

        #endregion

        #region Jungle

        __instance.m_clutter.Add(new()
        {
            m_biome = Const.Jungle,
            m_prefab = bundleJungle.LoadAsset<GameObject>("instanced_Jungle_grass big"),
            m_amount = instanced_heathgrass.m_amount,
            m_enabled = true,
            m_instanced = true,
            m_name = "instanced_Jungle_grass big",
            m_fractalOffset = instanced_heathgrass.m_fractalOffset,
            m_fractalScale = instanced_heathgrass.m_fractalScale,
            m_inForest = instanced_heathgrass.m_inForest,
            m_maxAlt = instanced_heathgrass.m_maxAlt,
            m_maxTilt = instanced_heathgrass.m_maxTilt,
            m_maxVegetation = instanced_heathgrass.m_maxVegetation,
            m_minAlt = instanced_heathgrass.m_minAlt,
            m_minTilt = instanced_heathgrass.m_minTilt,
            m_minVegetation = instanced_heathgrass.m_minVegetation,
            m_onCleared = instanced_heathgrass.m_onCleared,
            m_onUncleared = instanced_heathgrass.m_onUncleared,
            m_randomOffset = instanced_heathgrass.m_randomOffset,
            m_scaleMax = instanced_heathgrass.m_scaleMax,
            m_scaleMin = instanced_heathgrass.m_scaleMin,
            m_terrainTilt = instanced_heathgrass.m_terrainTilt,
            m_forestTresholdMax = instanced_heathgrass.m_forestTresholdMax,
            m_forestTresholdMin = instanced_heathgrass.m_forestTresholdMin,
            m_fractalTresholdMax = instanced_heathgrass.m_fractalTresholdMax,
            m_fractalTresholdMin = instanced_heathgrass.m_fractalTresholdMin
        });
        __instance.m_clutter.Add(new()
        {
            m_biome = Const.Jungle,
            m_prefab = bundleJungle.LoadAsset<GameObject>("instanced_Jungle_grass"),
            m_amount = instanced_heathgrass.m_amount,
            m_enabled = true,
            m_instanced = true,
            m_name = "instanced_Jungle_grass",
            m_fractalOffset = instanced_heathgrass.m_fractalOffset,
            m_fractalScale = instanced_heathgrass.m_fractalScale,
            m_inForest = instanced_heathgrass.m_inForest,
            m_maxAlt = instanced_heathgrass.m_maxAlt,
            m_maxTilt = instanced_heathgrass.m_maxTilt,
            m_maxVegetation = instanced_heathgrass.m_maxVegetation,
            m_minAlt = instanced_heathgrass.m_minAlt,
            m_minTilt = instanced_heathgrass.m_minTilt,
            m_minVegetation = instanced_heathgrass.m_minVegetation,
            m_onCleared = instanced_heathgrass.m_onCleared,
            m_onUncleared = instanced_heathgrass.m_onUncleared,
            m_randomOffset = instanced_heathgrass.m_randomOffset,
            m_scaleMax = instanced_heathgrass.m_scaleMax,
            m_scaleMin = instanced_heathgrass.m_scaleMin,
            m_terrainTilt = instanced_heathgrass.m_terrainTilt,
            m_forestTresholdMax = instanced_heathgrass.m_forestTresholdMax,
            m_forestTresholdMin = instanced_heathgrass.m_forestTresholdMin,
            m_fractalTresholdMax = instanced_heathgrass.m_fractalTresholdMax,
            m_fractalTresholdMin = instanced_heathgrass.m_fractalTresholdMin
        });
        __instance.m_clutter.Add(new()
        {
            m_biome = Const.Jungle,
            m_prefab = bundleJungle.LoadAsset<GameObject>("instanced_Jungle_grass_short"),
            m_amount = instanced_heathgrass.m_amount,
            m_enabled = true,
            m_instanced = instanced_heathgrass.m_instanced,
            m_name = "instanced_Jungle_grass",
            m_fractalOffset = instanced_heathgrass.m_fractalOffset,
            m_fractalScale = instanced_heathgrass.m_fractalScale,
            m_inForest = instanced_heathgrass.m_inForest,
            m_maxAlt = instanced_heathgrass.m_maxAlt,
            m_maxTilt = instanced_heathgrass.m_maxTilt,
            m_maxVegetation = instanced_heathgrass.m_maxVegetation,
            m_minAlt = instanced_heathgrass.m_minAlt,
            m_minTilt = instanced_heathgrass.m_minTilt,
            m_minVegetation = instanced_heathgrass.m_minVegetation,
            m_onCleared = instanced_heathgrass.m_onCleared,
            m_onUncleared = instanced_heathgrass.m_onUncleared,
            m_randomOffset = instanced_heathgrass.m_randomOffset,
            m_scaleMax = instanced_heathgrass.m_scaleMax,
            m_scaleMin = instanced_heathgrass.m_scaleMin,
            m_terrainTilt = instanced_heathgrass.m_terrainTilt,
            m_forestTresholdMax = instanced_heathgrass.m_forestTresholdMax,
            m_forestTresholdMin = instanced_heathgrass.m_forestTresholdMin,
            m_fractalTresholdMax = instanced_heathgrass.m_fractalTresholdMax,
            m_fractalTresholdMin = instanced_heathgrass.m_fractalTresholdMin,
            m_maxOceanDepth = instanced_heathgrass.m_maxOceanDepth,
            m_minOceanDepth = instanced_heathgrass.m_minOceanDepth,
            m_snapToWater = instanced_heathgrass.m_snapToWater
        });

        __instance.m_clutter.Add(new()
        {
            m_biome = Const.Jungle,
            m_prefab = bundleJungle.LoadAsset<GameObject>("instanced_seaweed"),
            m_amount = instanced_vass.m_amount,
            m_enabled = true,
            m_instanced = instanced_vass.m_instanced,
            m_name = "instanced_seaweed",
            m_fractalOffset = instanced_vass.m_fractalOffset,
            m_fractalScale = instanced_vass.m_fractalScale,
            m_inForest = instanced_vass.m_inForest,
            m_maxAlt = instanced_vass.m_maxAlt,
            m_maxTilt = instanced_vass.m_maxTilt,
            m_maxVegetation = instanced_vass.m_maxVegetation,
            m_minAlt = -5,
            m_minTilt = instanced_vass.m_minTilt,
            m_minVegetation = instanced_vass.m_minVegetation,
            m_onCleared = instanced_vass.m_onCleared,
            m_onUncleared = instanced_vass.m_onUncleared,
            m_randomOffset = instanced_vass.m_randomOffset,
            m_scaleMax = instanced_vass.m_scaleMax,
            m_scaleMin = instanced_vass.m_scaleMin,
            m_terrainTilt = instanced_vass.m_terrainTilt,
            m_forestTresholdMax = instanced_vass.m_forestTresholdMax,
            m_forestTresholdMin = instanced_vass.m_forestTresholdMin,
            m_fractalTresholdMax = instanced_vass.m_fractalTresholdMax,
            m_fractalTresholdMin = instanced_vass.m_fractalTresholdMin,
            m_maxOceanDepth = instanced_vass.m_maxOceanDepth,
            m_minOceanDepth = instanced_vass.m_minOceanDepth,
            m_snapToWater = instanced_vass.m_snapToWater
        });
        __instance.m_clutter.Add(new()
        {
            m_biome = Const.Jungle,
            m_prefab = bundleJungle.LoadAsset<GameObject>("instanced_WaterLilyLeaf"),
            m_amount = instanced_vass.m_amount,
            m_enabled = true,
            m_instanced = instanced_vass.m_instanced,
            m_name = "instanced_WaterLilyLeaf",
            m_fractalOffset = instanced_vass.m_fractalOffset,
            m_fractalScale = instanced_vass.m_fractalScale,
            m_inForest = instanced_vass.m_inForest,
            m_maxAlt = instanced_vass.m_maxAlt,
            m_maxTilt = instanced_vass.m_maxTilt,
            m_maxVegetation = instanced_vass.m_maxVegetation,
            m_minAlt = -10,
            m_minTilt = instanced_vass.m_minTilt,
            m_minVegetation = instanced_vass.m_minVegetation,
            m_onCleared = instanced_vass.m_onCleared,
            m_onUncleared = instanced_vass.m_onUncleared,
            m_randomOffset = instanced_vass.m_randomOffset,
            m_scaleMax = 3,
            m_scaleMin = 1,
            m_terrainTilt = instanced_vass.m_terrainTilt,
            m_forestTresholdMax = instanced_vass.m_forestTresholdMax,
            m_forestTresholdMin = instanced_vass.m_forestTresholdMin,
            m_fractalTresholdMax = instanced_vass.m_fractalTresholdMax,
            m_fractalTresholdMin = instanced_vass.m_fractalTresholdMin,
            m_maxOceanDepth = instanced_vass.m_maxOceanDepth,
            m_minOceanDepth = instanced_vass.m_minOceanDepth,
            m_snapToWater = instanced_vass.m_snapToWater
        });
        __instance.m_clutter.Add(new()
        {
            m_biome = Const.Jungle,
            m_prefab = bundleJungle.LoadAsset<GameObject>("instanced_WaterLilyFlower"),
            m_amount = instanced_vass.m_amount,
            m_enabled = true,
            m_instanced = instanced_vass.m_instanced,
            m_name = "instanced_WaterLilyFlower",
            m_fractalOffset = instanced_vass.m_fractalOffset,
            m_fractalScale = instanced_vass.m_fractalScale,
            m_inForest = instanced_vass.m_inForest,
            m_maxAlt = instanced_vass.m_maxAlt,
            m_maxTilt = instanced_vass.m_maxTilt,
            m_maxVegetation = instanced_vass.m_maxVegetation,
            m_minAlt = -10,
            m_minTilt = instanced_vass.m_minTilt,
            m_minVegetation = instanced_vass.m_minVegetation,
            m_onCleared = instanced_vass.m_onCleared,
            m_onUncleared = instanced_vass.m_onUncleared,
            m_randomOffset = instanced_vass.m_randomOffset,
            m_scaleMax = 2,
            m_scaleMin = 1,
            m_terrainTilt = instanced_vass.m_terrainTilt,
            m_forestTresholdMax = instanced_vass.m_forestTresholdMax,
            m_forestTresholdMin = instanced_vass.m_forestTresholdMin,
            m_fractalTresholdMax = instanced_vass.m_fractalTresholdMax,
            m_fractalTresholdMin = instanced_vass.m_fractalTresholdMin,
            m_maxOceanDepth = instanced_vass.m_maxOceanDepth,
            m_minOceanDepth = instanced_vass.m_minOceanDepth,
            m_snapToWater = instanced_vass.m_snapToWater
        });

        // __instance.m_clutter.Add(new()
        // {
        //     m_biome = Const.Jungle,
        //     m_prefab = bundleJungle.LoadAsset<GameObject>("instanced_jungle_ormbunke"),
        //     m_amount = instanced_heathflowers.m_amount + 100,
        //     m_enabled = true,
        //     m_instanced = instanced_heathflowers.m_instanced,
        //     m_name = "instanced_jungle_ormbunke",
        //     m_fractalOffset = instanced_heathflowers.m_fractalOffset,
        //     m_fractalScale = instanced_heathflowers.m_fractalScale,
        //     m_inForest = instanced_heathflowers.m_inForest,
        //     m_maxAlt = instanced_heathflowers.m_maxAlt,
        //     m_maxTilt = instanced_heathflowers.m_maxTilt,
        //     m_maxVegetation = instanced_heathflowers.m_maxVegetation,
        //     m_minAlt = instanced_heathflowers.m_minAlt,
        //     m_minTilt = instanced_heathflowers.m_minTilt,
        //     m_minVegetation = instanced_heathflowers.m_minVegetation,
        //     m_onCleared = instanced_heathflowers.m_onCleared,
        //     m_onUncleared = instanced_heathflowers.m_onUncleared,
        //     m_randomOffset = instanced_heathflowers.m_randomOffset,
        //     m_scaleMax = instanced_heathflowers.m_scaleMax,
        //     m_scaleMin = instanced_heathflowers.m_scaleMin,
        //     m_terrainTilt = instanced_heathflowers.m_terrainTilt,
        //     m_forestTresholdMax = instanced_heathflowers.m_forestTresholdMax,
        //     m_forestTresholdMin = instanced_heathflowers.m_forestTresholdMin,
        //     m_fractalTresholdMax = instanced_heathflowers.m_fractalTresholdMax,
        //     m_fractalTresholdMin = instanced_heathflowers.m_fractalTresholdMin,
        //     m_maxOceanDepth = instanced_heathflowers.m_maxOceanDepth,
        //     m_minOceanDepth = instanced_heathflowers.m_minOceanDepth,
        //     m_snapToWater = instanced_heathflowers.m_snapToWater
        // });
        __instance.m_clutter.Add(new()
        {
            m_biome = Const.Jungle,
            m_prefab = bundleJungle.LoadAsset<GameObject>("instanced_JungleFern"),
            m_amount = instanced_heathflowers.m_amount + 100,
            m_enabled = true,
            m_instanced = instanced_heathflowers.m_instanced,
            m_name = "instanced_JungleFern",
            m_fractalOffset = instanced_heathflowers.m_fractalOffset,
            m_fractalScale = instanced_heathflowers.m_fractalScale,
            m_inForest = instanced_heathflowers.m_inForest,
            m_maxAlt = instanced_heathflowers.m_maxAlt,
            m_maxTilt = instanced_heathflowers.m_maxTilt,
            m_maxVegetation = instanced_heathflowers.m_maxVegetation,
            m_minAlt = instanced_heathflowers.m_minAlt,
            m_minTilt = instanced_heathflowers.m_minTilt,
            m_minVegetation = instanced_heathflowers.m_minVegetation,
            m_onCleared = instanced_heathflowers.m_onCleared,
            m_onUncleared = instanced_heathflowers.m_onUncleared,
            m_randomOffset = instanced_heathflowers.m_randomOffset,
            m_scaleMax = instanced_heathflowers.m_scaleMax,
            m_scaleMin = instanced_heathflowers.m_scaleMin,
            m_terrainTilt = instanced_heathflowers.m_terrainTilt,
            m_forestTresholdMax = instanced_heathflowers.m_forestTresholdMax,
            m_forestTresholdMin = instanced_heathflowers.m_forestTresholdMin,
            m_fractalTresholdMax = instanced_heathflowers.m_fractalTresholdMax,
            m_fractalTresholdMin = instanced_heathflowers.m_fractalTresholdMin,
            m_maxOceanDepth = instanced_heathflowers.m_maxOceanDepth,
            m_minOceanDepth = instanced_heathflowers.m_minOceanDepth,
            m_snapToWater = instanced_heathflowers.m_snapToWater
        });
        __instance.m_clutter.Add(new()
        {
            m_biome = Const.Jungle,
            m_prefab = bundleJungle.LoadAsset<GameObject>("instanced_JungleFern 1"),
            m_amount = instanced_heathflowers.m_amount + 100,
            m_enabled = true,
            m_instanced = instanced_heathflowers.m_instanced,
            m_name = "instanced_JungleFern 1",
            m_fractalOffset = instanced_heathflowers.m_fractalOffset,
            m_fractalScale = instanced_heathflowers.m_fractalScale,
            m_inForest = instanced_heathflowers.m_inForest,
            m_maxAlt = instanced_heathflowers.m_maxAlt,
            m_maxTilt = instanced_heathflowers.m_maxTilt,
            m_maxVegetation = instanced_heathflowers.m_maxVegetation,
            m_minAlt = instanced_heathflowers.m_minAlt,
            m_minTilt = instanced_heathflowers.m_minTilt,
            m_minVegetation = instanced_heathflowers.m_minVegetation,
            m_onCleared = instanced_heathflowers.m_onCleared,
            m_onUncleared = instanced_heathflowers.m_onUncleared,
            m_randomOffset = instanced_heathflowers.m_randomOffset,
            m_scaleMax = instanced_heathflowers.m_scaleMax,
            m_scaleMin = instanced_heathflowers.m_scaleMin,
            m_terrainTilt = instanced_heathflowers.m_terrainTilt,
            m_forestTresholdMax = instanced_heathflowers.m_forestTresholdMax,
            m_forestTresholdMin = instanced_heathflowers.m_forestTresholdMin,
            m_fractalTresholdMax = instanced_heathflowers.m_fractalTresholdMax,
            m_fractalTresholdMin = instanced_heathflowers.m_fractalTresholdMin,
            m_maxOceanDepth = instanced_heathflowers.m_maxOceanDepth,
            m_minOceanDepth = instanced_heathflowers.m_minOceanDepth,
            m_snapToWater = instanced_heathflowers.m_snapToWater
        });
        __instance.m_clutter.Add(new()
        {
            m_biome = Const.Jungle,
            m_prefab = bundleJungle.LoadAsset<GameObject>("instanced_JungleFern 2"),
            m_amount = instanced_heathflowers.m_amount + 100,
            m_enabled = true,
            m_instanced = instanced_heathflowers.m_instanced,
            m_name = "instanced_JungleFern 2",
            m_fractalOffset = instanced_heathflowers.m_fractalOffset,
            m_fractalScale = instanced_heathflowers.m_fractalScale,
            m_inForest = instanced_heathflowers.m_inForest,
            m_maxAlt = instanced_heathflowers.m_maxAlt,
            m_maxTilt = instanced_heathflowers.m_maxTilt,
            m_maxVegetation = instanced_heathflowers.m_maxVegetation,
            m_minAlt = instanced_heathflowers.m_minAlt,
            m_minTilt = instanced_heathflowers.m_minTilt,
            m_minVegetation = instanced_heathflowers.m_minVegetation,
            m_onCleared = instanced_heathflowers.m_onCleared,
            m_onUncleared = instanced_heathflowers.m_onUncleared,
            m_randomOffset = instanced_heathflowers.m_randomOffset,
            m_scaleMax = instanced_heathflowers.m_scaleMax,
            m_scaleMin = instanced_heathflowers.m_scaleMin,
            m_terrainTilt = instanced_heathflowers.m_terrainTilt,
            m_forestTresholdMax = instanced_heathflowers.m_forestTresholdMax,
            m_forestTresholdMin = instanced_heathflowers.m_forestTresholdMin,
            m_fractalTresholdMax = instanced_heathflowers.m_fractalTresholdMax,
            m_fractalTresholdMin = instanced_heathflowers.m_fractalTresholdMin,
            m_maxOceanDepth = instanced_heathflowers.m_maxOceanDepth,
            m_minOceanDepth = instanced_heathflowers.m_minOceanDepth,
            m_snapToWater = instanced_heathflowers.m_snapToWater
        });
        __instance.m_clutter.Add(new()
        {
            m_biome = Const.Jungle,
            m_prefab = bundleJungle.LoadAsset<GameObject>("instanced_JungleFern 3"),
            m_amount = instanced_heathflowers.m_amount + 100,
            m_enabled = true,
            m_instanced = instanced_heathflowers.m_instanced,
            m_name = "instanced_JungleFern 3",
            m_fractalOffset = instanced_heathflowers.m_fractalOffset,
            m_fractalScale = instanced_heathflowers.m_fractalScale,
            m_inForest = instanced_heathflowers.m_inForest,
            m_maxAlt = instanced_heathflowers.m_maxAlt,
            m_maxTilt = instanced_heathflowers.m_maxTilt,
            m_maxVegetation = instanced_heathflowers.m_maxVegetation,
            m_minAlt = instanced_heathflowers.m_minAlt,
            m_minTilt = instanced_heathflowers.m_minTilt,
            m_minVegetation = instanced_heathflowers.m_minVegetation,
            m_onCleared = instanced_heathflowers.m_onCleared,
            m_onUncleared = instanced_heathflowers.m_onUncleared,
            m_randomOffset = instanced_heathflowers.m_randomOffset,
            m_scaleMax = instanced_heathflowers.m_scaleMax,
            m_scaleMin = instanced_heathflowers.m_scaleMin,
            m_terrainTilt = instanced_heathflowers.m_terrainTilt,
            m_forestTresholdMax = instanced_heathflowers.m_forestTresholdMax,
            m_forestTresholdMin = instanced_heathflowers.m_forestTresholdMin,
            m_fractalTresholdMax = instanced_heathflowers.m_fractalTresholdMax,
            m_fractalTresholdMin = instanced_heathflowers.m_fractalTresholdMin,
            m_maxOceanDepth = instanced_heathflowers.m_maxOceanDepth,
            m_minOceanDepth = instanced_heathflowers.m_minOceanDepth,
            m_snapToWater = instanced_heathflowers.m_snapToWater
        });
        __instance.m_clutter.Add(new()
        {
            m_biome = Const.Jungle,
            m_prefab = bundleJungle.LoadAsset<GameObject>("instanced_JungleGeran"),
            m_amount = instanced_heathflowers.m_amount + 100,
            m_enabled = true,
            m_instanced = instanced_heathflowers.m_instanced,
            m_name = "instanced_JungleGeran",
            m_fractalOffset = instanced_heathflowers.m_fractalOffset,
            m_fractalScale = instanced_heathflowers.m_fractalScale,
            m_inForest = instanced_heathflowers.m_inForest,
            m_maxAlt = instanced_heathflowers.m_maxAlt,
            m_maxTilt = instanced_heathflowers.m_maxTilt,
            m_maxVegetation = instanced_heathflowers.m_maxVegetation,
            m_minAlt = instanced_heathflowers.m_minAlt,
            m_minTilt = instanced_heathflowers.m_minTilt,
            m_minVegetation = instanced_heathflowers.m_minVegetation,
            m_onCleared = instanced_heathflowers.m_onCleared,
            m_onUncleared = instanced_heathflowers.m_onUncleared,
            m_randomOffset = instanced_heathflowers.m_randomOffset,
            m_scaleMax = instanced_heathflowers.m_scaleMax,
            m_scaleMin = instanced_heathflowers.m_scaleMin,
            m_terrainTilt = instanced_heathflowers.m_terrainTilt,
            m_forestTresholdMax = instanced_heathflowers.m_forestTresholdMax,
            m_forestTresholdMin = instanced_heathflowers.m_forestTresholdMin,
            m_fractalTresholdMax = instanced_heathflowers.m_fractalTresholdMax,
            m_fractalTresholdMin = instanced_heathflowers.m_fractalTresholdMin,
            m_maxOceanDepth = instanced_heathflowers.m_maxOceanDepth,
            m_minOceanDepth = instanced_heathflowers.m_minOceanDepth,
            m_snapToWater = instanced_heathflowers.m_snapToWater
        });

        #endregion
    }
}
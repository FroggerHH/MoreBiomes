using System.Collections.Generic;
using HarmonyLib;
using ItemManager;
using UnityEngine;
using static MoreBiomes.Plugin;
using static Heightmap;
using static Heightmap.Biome;
using static ZoneSystem;
using static ZoneSystem.ZoneVegetation;
using static ClutterSystem;
using static ClutterSystem.Clutter;
using static ClutterSystem.Quality;
using static ClutterSystem.PatchData;
using static MoreBiomes.ClutterExtention;
using static MoreBiomes.Const;

namespace MoreBiomes;

[HarmonyPatch]
public class Grass
{
    private static ClutterSystem.Clutter instanced_heathgrass;
    private static ClutterSystem.Clutter instanced_heathflowers;
    private static ClutterSystem.Clutter instanced_vass;

    [HarmonyPatch(typeof(ClutterSystem), nameof(ClutterSystem.Awake)), HarmonyPostfix]
    public static void AddGrassPatch(ClutterSystem __instance)
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
            m_minVegetation = instanced_heathgrass.m_minVegetation / 1.5f,
            m_maxVegetation = instanced_heathgrass.m_maxVegetation / 1.5f,
            m_minAlt = instanced_heathgrass.m_minAlt,
            m_minTilt = instanced_heathgrass.m_minTilt,
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
            m_minVegetation = instanced_vass.m_minVegetation / 1.5f,
            m_maxVegetation = instanced_vass.m_maxVegetation / 1.5f,
            m_minAlt = -5,
            m_minTilt = instanced_vass.m_minTilt,
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
            m_minVegetation = instanced_heathflowers.m_minVegetation / 1.5f,
            m_maxVegetation = instanced_heathflowers.m_maxVegetation / 1.5f,
            m_minAlt = instanced_heathflowers.m_minAlt,
            m_minTilt = instanced_heathflowers.m_minTilt,
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

        AddGrass("instanced_Jungle_grass big", instanced_heathgrass)
            .SetBiome(Jungle)
            .SetMaxScale(2.5f)
            .SetMinAlt(2f);
        AddGrass("instanced_Jungle_grass", instanced_heathgrass)
            .SetBiome(Jungle)
            .SetMaxScale(1.5f)
            .SetMinAlt(1f);
        AddGrass("instanced_Jungle_grass_short", instanced_heathgrass)
            .SetBiome(Jungle)
            .SetMaxScale(1)
            .SetMinAlt(0.5f);
        AddGrass("instanced_seaweed", instanced_vass)
            .SetMinAlt(-1)
            .SetMaxAlt(0)
            .SetMinScale(1)
            .SetMaxScale(3)
            .SetBiome(Jungle);
        AddGrass("instanced_WaterLilyLeaf", instanced_vass)
            .SetMinAlt(-7)
            .SetMaxAlt(-1)
            .SetMinScale(1)
            .SetMaxScale(3)
            .SetBiome(Jungle)
            .SetSnapToWater(true);
        AddGrass("instanced_WaterLilyFlower", instanced_vass)
            .SetMinAlt(-7)
            .SetMaxAlt(-1)
            .SetMinScale(1f)
            .SetMaxScale(1.7f)
            .SetBiome(Jungle)
            .SetSnapToWater(true);
        AddGeran();
        Add_JungleFern();
        Add_JungleFern_1();
        Add_JungleFern_2();
        Add_JungleFern_3();

        #endregion
    }

    private static void AddGeran()
    {
        AddGrass("instanced_JungleGeran", instanced_vass)
            .SetMinAlt(1.3f)
            .SetMaxAlt(1000)
            .SetMinScale(0.2f)
            .SetMaxScale(1f)
            .SetBiome(Jungle);
    }

    private static void Add_JungleFern_3()
    {
        AddGrass("instanced_JungleFern 3", instanced_vass)
            .SetMinAlt(3)
            .SetMaxAlt(10000)
            .SetMinScale(0.2f)
            .SetMaxScale(1.5f)
            .SetBiome(Jungle);
    }

    private static void Add_JungleFern_2()
    {
        AddGrass("instanced_JungleFern 2", instanced_vass)
            .SetMinAlt(2)
            .SetMaxAlt(10000)
            .SetMinScale(0.2f)
            .SetMaxScale(1.5f)
            .SetBiome(Jungle);
    }

    private static void Add_JungleFern_1()
    {
        AddGrass("instanced_JungleFern 1", instanced_vass)
            .SetMinAlt(5)
            .SetMaxAlt(10000)
            .SetMinScale(0.2f)
            .SetMaxScale(0.9f)
            .SetBiome(Jungle);
    }

    private static void Add_JungleFern()
    {
        AddGrass("instanced_JungleFern", instanced_vass)
            .SetMinAlt(1)
            .SetMaxAlt(10000)
            .SetMinScale(0.2f)
            .SetMaxScale(1.5f)
            .SetBiome(Jungle);
    }

    private static ClutterExtention.MBClutter AddGrass(string name, string reference)
    {
        return AddGrass(name, ClutterSystem.instance.m_clutter.Find(x => x.m_prefab.name == reference));
    }

    private static MBClutter AddGrass(string name, Clutter reference)
    {
        var clutter = new Clutter()
        {
            m_biome = reference.m_biome,
            m_prefab = bundleJungle.LoadAsset<GameObject>(name),
            m_amount = reference.m_amount,
            m_enabled = true,
            m_instanced = true,
            m_name = name,
            m_fractalOffset = reference.m_fractalOffset,
            m_fractalScale = reference.m_fractalScale,
            m_inForest = reference.m_inForest,
            m_maxAlt = reference.m_maxAlt,
            m_maxTilt = reference.m_maxTilt,
            m_maxVegetation = reference.m_maxVegetation,
            m_minAlt = reference.m_minAlt,
            m_minTilt = reference.m_minTilt,
            m_minVegetation = reference.m_minVegetation,
            m_onCleared = reference.m_onCleared,
            m_onUncleared = reference.m_onUncleared,
            m_randomOffset = reference.m_randomOffset,
            m_scaleMin = reference.m_scaleMin,
            m_scaleMax = reference.m_scaleMax,
            m_terrainTilt = reference.m_terrainTilt,
            m_forestTresholdMax = reference.m_forestTresholdMax,
            m_forestTresholdMin = reference.m_forestTresholdMin,
            m_fractalTresholdMax = reference.m_fractalTresholdMax,
            m_fractalTresholdMin = reference.m_fractalTresholdMin
        };
        ClutterSystem.instance.m_clutter.Add(clutter);

        return clutter.ToMBClutter();
    }
}
using System;
using System.Diagnostics;
using System.Linq;
using HarmonyLib;
using UnityEngine;
using UnityEngine.SceneManagement;
using static MoreBiomes.Plugin;
using static Heightmap;
using static ZoneSystem;
using static ZoneSystem.ZoneLocation;

namespace MoreBiomes;

[HarmonyPatch]
internal static class MoreBiomes
{
    private static bool isInitingStartTemple = false;
    private static Color32 desertColor = new Color32((byte)0, (byte)0, (byte)0, byte.MaxValue);


    [HarmonyPatch(typeof(WorldGenerator), nameof(WorldGenerator.GetBiome), typeof(float), typeof(float)),
     HarmonyPostfix]
    public static void GetBiome(WorldGenerator __instance, ref Biome __result, float wx, float wy)
    {
        if (__instance.m_world.m_menu) return;
        if (isInitingStartTemple) return;

        float magnitude = new Vector2(wx, wy).magnitude;
        float num = __instance.WorldAngle(wx, wy) * 90f;

        var x = (float)((__instance.m_offset1 + wx) * (1f / 1000f));
        var y = (float)((__instance.m_offset1 + wy) * (1f / 1000f));
        var noise = Mathf.PerlinNoise(x, y);
        if (noise > 0.4000010059604645 && magnitude > 4000f + num && magnitude < 6000f) //3000 //8000
        {
            __result = desert;
        }
    }

    [HarmonyPatch(typeof(ZoneSystem), nameof(ZoneSystem.GenerateLocations), typeof(ZoneLocation)), HarmonyPrefix]
    public static void MoreBiomesZoneSystemGenerateLocationsPatch(ZoneSystem __instance, ZoneLocation location)
    {
        isInitingStartTemple = location.m_prefabName == "StartTemple";
    }

    [HarmonyPatch(typeof(Heightmap), nameof(Heightmap.GetBiomeColor), typeof(Biome)), HarmonyPostfix]
    public static void GetBiomeColor(Heightmap __instance, Heightmap.Biome biome, ref Color32 __result)
    {
        if (biome is desert)
        {
            __result = desertColor;
        }
    }

    [HarmonyPatch(typeof(Minimap), nameof(Minimap.GetPixelColor), typeof(Biome)), HarmonyPostfix]
    public static void GetPixelColor(Minimap __instance, Heightmap.Biome biome, ref Color __result)
    {
        if (biome is desert)
        {
            __result = new Color(1, 0.61f, 0);
        }
    }

    [HarmonyPatch(typeof(EnvMan), nameof(EnvMan.Awake)), HarmonyPrefix]
    public static void EnvManAwake(EnvMan __instance)
    {
        var biomeEnvSetup = new BiomeEnvSetup();
        biomeEnvSetup.m_name = "Desert";
        var meadowsEnvSetup = __instance.m_biomes.Find(x => x.m_biome == Biome.Meadows);
        biomeEnvSetup.m_environments = meadowsEnvSetup.m_environments;
        biomeEnvSetup.m_musicMorning = meadowsEnvSetup.m_musicMorning;
        biomeEnvSetup.m_musicEvening = meadowsEnvSetup.m_musicEvening;
        biomeEnvSetup.m_musicDay = meadowsEnvSetup.m_musicDay;
        biomeEnvSetup.m_musicNight = meadowsEnvSetup.m_musicNight;
        biomeEnvSetup.m_biome = desert;
        __instance.m_biomes.Add(biomeEnvSetup);
    }

    [HarmonyPatch(typeof(WorldGenerator), nameof(WorldGenerator.GetBiomeHeight)), HarmonyPostfix]
    public static void GetBiomeHeight(WorldGenerator __instance, Heightmap.Biome biome, float wx, float wy,
        ref float __result)
    {
        if (biome is desert)
        {
            __result = __instance.GetMeadowsHeight(wx, wy) * 200f;
        }
    }

    [HarmonyPatch(typeof(Heightmap), nameof(Heightmap.Initialize)), HarmonyPostfix]
    public static void GetBiomeHeight(Heightmap __instance)
    {
    }

    public static float GetMeadowsHeight(WorldGenerator worldGenerator, float wx, float wy)
    {
        float wx1 = wx;
        float wy1 = wy;
        double baseHeight = (double)worldGenerator.GetBaseHeight(wx, wy, false);
        wx += 100000f + worldGenerator.m_offset3;
        wy += 100000f + worldGenerator.m_offset3;
        return //worldGenerator.AddRivers(wx1, wy1, h) +
            Mathf.PerlinNoise(wx * 0.1f, wy * 0.1f) * 0.02f + Mathf.PerlinNoise(wx * 0.3f, wy * 0.21f) * (2.5f / 1000f);
    }
}


//TestMoreBiomes
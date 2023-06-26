using System;
using System.Diagnostics;
using System.Linq;
using HarmonyLib;
using UnityEngine;
using UnityEngine.SceneManagement;
using static MoreBiomes.Plugin;
using static Heightmap;

namespace MoreBiomes;

[HarmonyPatch]
internal static class MoreBiomes
{
    private static bool isInitingStartTemple = false;

    [HarmonyPatch(typeof(WorldGenerator), nameof(WorldGenerator.GetBiome), typeof(float), typeof(float)),
     HarmonyPostfix]
    public static void MoreBiomesWorldGeneratorGetBiomePatch2(WorldGenerator __instance, ref Biome __result, float wx,
        float wy)
    {
        if (__instance.m_world.m_menu) return;
        if (isInitingStartTemple) return;

        float magnitude = new Vector2(wx, wy).magnitude;
        float num = __instance.WorldAngle(wx, wy) * 100f;

        var x = (float)((__instance.m_offset1 + wx) * (1.0 / 1000.0));
        var y = (float)((__instance.m_offset1 + wy) * (1.0 / 1000.0));
        var noise = Mathf.PerlinNoise(x, y);
        if (noise > 0.4000010059604645 && magnitude > 3100.0 + num && magnitude < 7000.0)
        {
            __result = (Biome)(1024);
        }
    }

    [HarmonyPatch(typeof(ZoneSystem), nameof(ZoneSystem.GenerateLocations), typeof(ZoneSystem.ZoneLocation)),
     HarmonyPrefix]
    public static void MoreBiomesZoneSystemGenerateLocationsPatch(ZoneSystem __instance,
        ZoneSystem.ZoneLocation location)
    {
        isInitingStartTemple = location.m_prefabName == "StartTemple";
    }

    [HarmonyPatch(typeof(Heightmap), nameof(Heightmap.GetBiomeColor), typeof(Biome)),
     HarmonyPostfix]
    public static void GetBiomeColor(Heightmap __instance, Heightmap.Biome biome, ref Color32 __result)
    {
        if (biome is (Biome)(1024))
        {
            __result = new Color32(255, 175, 0, 0);
        }
    }

    [HarmonyPatch(typeof(Minimap), nameof(Minimap.GetPixelColor), typeof(Biome)),
     HarmonyPostfix]
    public static void GetPixelColor(Minimap __instance, Heightmap.Biome biome, ref Color __result)
    {
        if (biome is (Biome)(1024))
        {
            __result = new Color(1, 0.61f, 0);
        }
    }

    [HarmonyPatch(typeof(WorldGenerator), nameof(WorldGenerator.GetBiomeHeight)), HarmonyPostfix]
    public static void GetBiomeHeight(WorldGenerator __instance, Heightmap.Biome biome,
        float wx, float wy, ref float __result)
    {
        if (biome is (Biome)(1024))
        {
            __result = __instance.GetPlainsHeight(wx, wy) * 280f;
        }
    }

    // [HarmonyPatch(typeof(EnvMan), nameof(EnvMan.GetBiomeEnvSetup)), HarmonyPostfix]
    // public static void GetBiomeEnvSetup(EnvMan __instance, Biome biome, ref float __result)
    // {
    //     if (biome is (Biome)(1024))
    //     {
    //     }
    // }

    [HarmonyPatch(typeof(EnvMan), nameof(EnvMan.Awake)), HarmonyPrefix]
    public static void EnvManAwake(EnvMan __instance)
    {
        var biomeEnvSetup = new BiomeEnvSetup();
        biomeEnvSetup.m_name = "";
        var meadowsEnvSetup = __instance.m_biomes.Find(x => x.m_biome == Biome.Meadows);
        biomeEnvSetup.m_environments = meadowsEnvSetup.m_environments;
        biomeEnvSetup.m_musicMorning = meadowsEnvSetup.m_musicMorning;
        biomeEnvSetup.m_musicEvening = meadowsEnvSetup.m_musicEvening;
        biomeEnvSetup.m_musicDay = meadowsEnvSetup.m_musicDay;
        biomeEnvSetup.m_musicNight = meadowsEnvSetup.m_musicNight;
        biomeEnvSetup.m_biome = (Biome)(1024);
        __instance.m_biomes.Add(biomeEnvSetup);
    }
}


//TestMoreBiomes
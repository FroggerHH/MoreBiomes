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
    private static Color32 jungleColor = new Color32(0, 62, 255, 197);

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


        if (noise > 0.4 && magnitude > 4000f + num && magnitude < 6000f) //3000 8000 = Plains
        {
            __result = Const.Desert;
            return;
        }

        if (noise > 0.4f && magnitude > 6000f + num && magnitude < 8000f)
        {
            __result = Const.Jungle;
            return;
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
        if (biome is Const.Desert)
        {
            __result = desertColor;
        }

        if (biome is Const.Jungle)
        {
            __result = jungleColor;
        }
    }

    [HarmonyPatch(typeof(Minimap), nameof(Minimap.GetPixelColor), typeof(Biome)), HarmonyPostfix]
    public static void GetPixelColor(Minimap __instance, Heightmap.Biome biome, ref Color __result)
    {
        if (biome is Const.Desert)
        {
            __result = new Color(1, 0.61f, 0);
        }

        if (biome is Const.Jungle)
        {
            __result = new Color(0f, 0.58f, 0);
        }
    }

    [HarmonyPatch(typeof(Minimap), nameof(Minimap.GetMaskColor)), HarmonyPostfix]
    public static void GetMaskColor(Minimap __instance, float wx, float wy, float height, Biome biome,
        ref Color __result)
    {
        if (biome is Const.Desert || biome is Const.Jungle)
        {
            __result = WorldGenerator.GetForestFactor(new Vector3(wx, 0.0f, wy)) >= 0.800000011920929 ? __instance.noForest : __instance.forest;
        }
    }

    [HarmonyPatch(typeof(EnvMan), nameof(EnvMan.Awake)), HarmonyPrefix]
    public static void EnvManAwake(EnvMan __instance)
    {
        var meadowsEnvSetup = __instance.m_biomes.Find(x => x.m_biome == Biome.Meadows);
        var desertSetup = new BiomeEnvSetup();
        desertSetup.m_name = nameof(Const.Desert);
        desertSetup.m_environments = meadowsEnvSetup.m_environments;
        desertSetup.m_musicMorning = meadowsEnvSetup.m_musicMorning;
        desertSetup.m_musicEvening = meadowsEnvSetup.m_musicEvening;
        desertSetup.m_musicDay = meadowsEnvSetup.m_musicDay;
        desertSetup.m_musicNight = meadowsEnvSetup.m_musicNight;
        desertSetup.m_biome = Const.Desert;
        __instance.m_biomes.Add(desertSetup);

        var jungleSetup = new BiomeEnvSetup();
        jungleSetup.m_name = nameof(Const.Jungle);
        jungleSetup.m_environments = meadowsEnvSetup.m_environments;
        jungleSetup.m_musicMorning = meadowsEnvSetup.m_musicMorning;
        jungleSetup.m_musicEvening = meadowsEnvSetup.m_musicEvening;
        jungleSetup.m_musicDay = meadowsEnvSetup.m_musicDay;
        jungleSetup.m_musicNight = meadowsEnvSetup.m_musicNight;
        jungleSetup.m_biome = Const.Jungle;
        __instance.m_biomes.Add(jungleSetup);
    }

    [HarmonyPatch(typeof(WorldGenerator), nameof(WorldGenerator.GetBiomeHeight)), HarmonyPostfix]
    public static void GetBiomeHeight(WorldGenerator __instance, Heightmap.Biome biome, float wx, float wy,
        ref float __result)
    {
        if (biome is Const.Desert)
        {
            __result = GetDesertHeight(__instance, wx, wy) * 260f;
            return;
        }

        if (biome is Const.Jungle)
        {
            __result = GetJungleHeight(__instance, wx, wy) * 230f;
            return;
        }
    }

    public static float GetDesertHeight(WorldGenerator worldGenerator, float wx, float wy)
    {
        float wx1 = wx;
        float wy1 = wy;
        float baseHeight = worldGenerator.GetBaseHeight(wx, wy, false);
        wx += 100000f + worldGenerator.m_offset3;
        wy += 100000f + worldGenerator.m_offset3;
        float num1 = Mathf.PerlinNoise(wx * 0.01f, wy * 0.01f) * Mathf.PerlinNoise(wx * 0.02f, wy * 0.02f);
        float h = baseHeight + num1 +
                  Mathf.PerlinNoise(wx * 0.05f, wy * 0.05f) *
                  Mathf.PerlinNoise(wx * 0.1f, wy * 0.1f) *
                  num1 * 0.5f * 0.1f;
        float num2 = 0.15f;
        float num3 = h - num2;
        float num4 = Mathf.Clamp01(baseHeight / 0.4000000059604645f);
        if ((double)num3 > 0.0)
            h -= num3 * (1f - num4 * 0.75f);
        return worldGenerator.AddRivers(wx1, wy1, h) +
               Mathf.PerlinNoise(wx * 0.1f, wy * 0.1f) * 0.01f +
               Mathf.PerlinNoise(wx * 0.4f, wy * 0.4f) * (3f / 1000f);
    }

    public static float GetJungleHeight(WorldGenerator worldGenerator, float wx, float wy)
    {
        float wx1 = wx;
        float wy1 = wy;
        double baseHeight = (double)worldGenerator.GetBaseHeight(wx, wy, false);
        wx += 100000f + worldGenerator.m_offset3;
        wy += 100000f + worldGenerator.m_offset3;

        float num1 = Mathf.PerlinNoise(wx * 0.01f, wy * 0.01f) * Mathf.PerlinNoise(wx * 0.02f, wy * 0.02f);
        float h = (float)baseHeight + (num1 + (float)((double)Mathf.PerlinNoise(wx * 0.05f, wy * 0.05f) *
                                                      (double)Mathf.PerlinNoise(wx * 0.1f, wy * 0.1f) * (double)num1 *
                                                      0.5)) * 0.1f;
        float num2 = 0.15f;
        float num3 = h - num2;
        float num4 = Mathf.Clamp01((float)(baseHeight / 0.4000000059604645));
        if ((double)num3 > 0.0)
            h -= (float)((double)num3 * (1.0 - (double)num4) * 0.75);


        return worldGenerator.AddRivers(wx1, wy1, h) +
               Mathf.PerlinNoise(wx * 0.14f, wy * 0.14f) * 0.02f +
               Mathf.PerlinNoise(wx * 0.8f, wy * 0.8f) * (2.5f / 1000f);
    }
}


//TestMoreBiomes
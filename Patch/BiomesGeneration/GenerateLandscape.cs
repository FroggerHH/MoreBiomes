using HarmonyLib;
using UnityEngine;
using static MoreBiomes.Const;
using static Heightmap;

namespace MoreBiomes;

[HarmonyPatch] internal static class GenerateLandscape
{
    [HarmonyPatch(typeof(WorldGenerator), nameof(WorldGenerator.GetBiomeHeight))] [HarmonyPostfix]
    public static void GetBiomeHeight(WorldGenerator __instance, Biome biome, float wx, float wy,
        ref float __result)
    {
        if (biome is Desert)
        {
            __result = GetDesertHeight(__instance, wx, wy) * 260f;
            return;
        }

        if (biome is Jungle)
        {
            __result = GetJungleHeight(__instance, wx, wy) * 230f;
            return;
        }

        if (biome is Canyon) __result = GetCanyonHeight(__instance, wx, wy) * 230f;
    }

    public static float GetDesertHeight(WorldGenerator worldGenerator, float wx, float wy)
    {
        var wx1 = wx;
        var wy1 = wy;
        var baseHeight = worldGenerator.GetBaseHeight(wx, wy, false);
        wx += 100000f + worldGenerator.m_offset3;
        wy += 100000f + worldGenerator.m_offset3;
        var num1 = Mathf.PerlinNoise(wx * 0.01f, wy * 0.01f)
                   * Mathf.PerlinNoise(wx * 0.02f, wy * 0.02f);
        var h = baseHeight +
                (num1 + Mathf.PerlinNoise(wx * 0.05f, wy * 0.05f) *
                    Mathf.PerlinNoise(wx * 0.1f, wy * 0.1f) * num1 *
                    0.5f) * 0.1f;
        var num2 = 0.15f;
        var num3 = h - num2;
        var num4 = Mathf.Clamp01((float)(baseHeight / 0.4000000059604645));
        if (num3 > 0)
            h -= num3 * (1 - num4) * 0.75f;

        var desertHeight = worldGenerator.AddRivers(wx1, wy1, h) +
                           Mathf.PerlinNoise(wx * 0.3f, wy * 0.1f) * 0.0001f +
                           Mathf.PerlinNoise(wx * 0.6f, wy * 0.6f) * 0.005f;

        desertHeight = Mathf.Lerp(desertHeight + Mathf.PerlinNoise(wx * 0.4f, wy * 0.4f) * 0.0005f,
            Mathf.Ceil(desertHeight * 120) / 120, num4);
        return desertHeight;
    }

    public static float GetJungleHeight(WorldGenerator worldGenerator, float wx, float wy)
    {
        var wx1 = wx;
        var wy1 = wy;
        var baseHeight = worldGenerator.GetBaseHeight(wx, wy, false);
        wx += 100000f + worldGenerator.m_offset3;
        wy += 100000f + worldGenerator.m_offset3;

        var num1 = Mathf.PerlinNoise(wx * 0.01f, wy * 0.01f) * Mathf.PerlinNoise(wx * 0.02f, wy * 0.02f);
        var h = baseHeight + (num1 + Mathf.PerlinNoise(wx * 0.05f, wy * 0.05f) *
            Mathf.PerlinNoise(wx * 0.1f, wy * 0.1f) * num1 *
            0.5f) * 0.1f;
        var num2 = 0.15f;
        var num3 = h - num2;
        var num4 = Mathf.Clamp01((float)(baseHeight / 0.4000000059604645));
        if (num3 > 0)
            h -= num3 * (1 - num4) * 0.75f;


        return worldGenerator.AddRivers(wx1, wy1, h) +
               Mathf.PerlinNoise(wx * 0.14f, wy * 0.14f) * 0.02f +
               Mathf.PerlinNoise(wx * 0.8f, wy * 0.8f) * (2.5f / 1000f);
    }

    public static float GetCanyonHeight(WorldGenerator worldGenerator, float wx, float wy) =>
        GetJungleHeight(worldGenerator, wx, wy);
}
using MoreBiomes.Noise;

namespace MoreBiomes;

[HarmonyPatch] internal static class AddBiomes
{
    [HarmonyPatch(typeof(WorldGenerator), nameof(WorldGenerator.GetBiome), typeof(float), typeof(float))]
    [HarmonyPostfix]
    public static void GetBiomePatch(WorldGenerator __instance, ref Biome __result, float wx, float wy)
    {
        var baseHeight = __instance.GetBaseHeight(wx, wy, false);
        if (baseHeight <= 0.02) return;

        var magnitude = new Vector2(wx, wy).magnitude;
        var worldAngle = __instance.WorldAngle(wx, wy) * 100f;

        var basePerlinNoise = Mathf.PerlinNoise(
            (__instance.m_offset1 + wx) * (1f / 1000f),
            (__instance.m_offset1 + wy) * (1f / 1000f));


        var noise = NoiseGenerator.GetPixel(wx, wy);
        if (basePerlinNoise > 0.4 &&
            noise == 1 &&
            magnitude > 2100f + worldAngle &&
            magnitude < 3500f)
        {
            __result = Desert;
            return;
        }

        if (basePerlinNoise > 0.4f &&
            noise == 1 &&
            magnitude > 3500f + worldAngle &&
            magnitude < 4500f)
        {
            __result = Jungle;
            return;
        }

        if (basePerlinNoise > 0.4f &&
            noise == 1 &&
            magnitude > 4500f + worldAngle &&
            magnitude < 6500f)
        {
            __result = Canyon;
            return;
        }


        var magnitudePlus4000 = new Vector2(wx, wy + 4000).magnitude;
        var magnitudeMinus4000 = new Vector2(wx, wy - 4000).magnitude;
        if (basePerlinNoise > 0.4f &&
            noise == 1 &&
            magnitudePlus4000 > 12000f + worldAngle &&
            magnitudePlus4000 < 14000f)
        {
            __result = Siberia_snowy;
            return;
        }

        if (basePerlinNoise > 0.4f &&
            noise == 1 &&
            magnitudeMinus4000 > 12000f + worldAngle &&
            magnitudeMinus4000 < 14000f)
            __result = Siberia_steppe;
    }
}
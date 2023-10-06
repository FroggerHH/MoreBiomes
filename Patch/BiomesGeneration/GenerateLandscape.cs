namespace MoreBiomes;

[HarmonyPatch] internal static class GenerateLandscape
{
    [HarmonyPatch(typeof(WorldGenerator), nameof(WorldGenerator.GetBiomeHeight))] [HarmonyPostfix]
    public static void GetBiomeHeight(WorldGenerator __instance, Biome biome, float wx, float wy,
        ref float __result)
    {
        __result = biome switch
        {
            Desert => GetDesertHeight(__instance, wx, wy) * 260f,
            Jungle => GetJungleHeight(__instance, wx, wy) * 230,
            Canyon => GetCanyonHeight(__instance, wx, wy) * 230f,
            Siberia_steppe => GetSiberiaHeight(__instance, wx, wy) * 200,
            Siberia_snowy => GetSiberiaHeight(__instance, wx, wy) * 200,
            _ => __result
        };
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

    public static float GetCanyonHeight(WorldGenerator worldGenerator, float wx, float wy)
    {
        return GetJungleHeight(worldGenerator, wx, wy);
    }

    public static float GetSiberiaHeight(WorldGenerator worldGenerator, float wx, float wy)
    {
        float wx1 = wx;
        float wy1 = wy;
        wx += 100000f;
        wy += 100000f;


        var height = siberiaBaseLandHeight;


        var riversNoise1 = DUtils.PerlinNoise(wx * siberiaMod1_x, wy * siberiaMod1_y) * siberiaMod1;
        var riversNoise2 = DUtils.PerlinNoise(wx * siberiaMod2_x, wy * siberiaMod2_y) * siberiaMod2;
        var baseRivers = worldGenerator.AddRivers(wx1, wy1, height);
        float rivers = baseRivers + riversNoise1 + riversNoise2;

        return height + rivers;
    }

    static float siberiaBaseLandHeight = 0.09f;
    static float siberiaMod1 = 0.01f;
    static double siberiaMod1_x = 0.1;
    static double siberiaMod1_y = 0.1;
    static float siberiaMod2 = 3f / 1000f;
    static double siberiaMod2_x = 0.4;
    static double siberiaMod2_y = 0.4;
}
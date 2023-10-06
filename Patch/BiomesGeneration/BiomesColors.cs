namespace MoreBiomes;

[HarmonyPatch]
internal static class BiomesColors
{
    [HarmonyPatch(typeof(Heightmap), nameof(Heightmap.GetBiomeColor), typeof(Biome))]
    public static void Postfix(Biome biome, ref Color32 __result)
    {
        switch (biome)
        {
            case Desert:
                __result = desertColor;
                break;
            case Jungle:
                __result = jungleColor;
                break;
            case Canyon:
                __result = сanyonColor;
                break;
        }
    }

    [HarmonyPatch(typeof(Minimap), nameof(Minimap.GetPixelColor), typeof(Biome))]
    public static void Postfix(Biome biome, ref Color __result)
    {
        switch (biome)
        {
            case Desert:
                __result = new Color(1, 0.61f, 0);
                return;
            case Jungle:
                __result = new Color(0.65f, 0.91f, 0.46f);
                return;
            case Canyon:
                __result = new Color(0.86f, 0.86f, 0.34f);
                return;
            case Siberia_steppe:
                __result = new Color(0.89f, 0.82f, 0.45f);
                return;
            case Siberia_snowy:
                __result = new Color(0.87f, 1f, 1f);
                return;
        }
    }
}
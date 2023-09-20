using HarmonyLib;
using UnityEngine;
using static MoreBiomes.Const;
using static Heightmap;

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
                __result = new Color(0f, 0.58f, 0);
                return;
            case Canyon:
                __result = new Color(0.75f, 1f, 0.88f);
                return;
        }
    }
}
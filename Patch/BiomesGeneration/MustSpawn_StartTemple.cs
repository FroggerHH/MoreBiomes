namespace MoreBiomes;

[HarmonyPatch]
internal static class MustSpawn_StartTemple
{
    private static bool isInitingStartTemple;

    [HarmonyPatch(typeof(WorldGenerator), nameof(WorldGenerator.GetBiome),
        typeof(float), typeof(float))]
    private static void Postfix(WorldGenerator __instance, ref Biome __result, float wx, float wy)
    {
        if (__instance.m_world.m_menu) return;
        if (isInitingStartTemple) return;

        AddBiomes.GetBiomePatch(__instance, ref __result, wx, wy);
    }

    [HarmonyPatch(typeof(ZoneSystem), nameof(ZoneSystem.GenerateLocations),
        typeof(ZoneLocation))]
    private static void Prefix(ZoneSystem __instance, ZoneLocation location)
    {
        isInitingStartTemple =
            location.m_prefabName == "StartTemple";
    }
}
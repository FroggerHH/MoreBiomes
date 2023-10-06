namespace MoreBiomes;

[HarmonyPatch] internal static class AddEnvironments
{
    private static BiomeEnvSetup meadowsEnvSetup;

    [HarmonyPatch(typeof(EnvMan), nameof(EnvMan.Awake))] [HarmonyPrefix]
    public static void EnvManAwake(EnvMan __instance)
    {
        meadowsEnvSetup = __instance.m_biomes.Find(x => x.m_biome == Meadows);

        CreateMeadowsEnvCloneForBiome(__instance, Desert);
        CreateMeadowsEnvCloneForBiome(__instance, Jungle);
        CreateMeadowsEnvCloneForBiome(__instance, Canyon);
        CreateMeadowsEnvCloneForBiome(__instance, Siberia_steppe);
        CreateMeadowsEnvCloneForBiome(__instance, Siberia_snowy);
    }

    private static void CreateMeadowsEnvCloneForBiome(EnvMan envMan, Biome biome)
    {
        envMan.m_biomes.Add(new BiomeEnvSetup
        {
            m_name = GetBiomeName(biome),
            m_environments = meadowsEnvSetup.m_environments,
            m_musicMorning = meadowsEnvSetup.m_musicMorning,
            m_musicEvening = meadowsEnvSetup.m_musicEvening,
            m_musicDay = meadowsEnvSetup.m_musicDay,
            m_musicNight = meadowsEnvSetup.m_musicNight,
            m_biome = biome
        });
    }
}
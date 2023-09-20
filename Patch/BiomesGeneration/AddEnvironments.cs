using HarmonyLib;
using static Heightmap;

namespace MoreBiomes;

[HarmonyPatch] internal static class AddEnvironments
{
    private static BiomeEnvSetup meadowsEnvSetup;

    [HarmonyPatch(typeof(EnvMan), nameof(EnvMan.Awake))] [HarmonyPrefix]
    public static void EnvManAwake(EnvMan __instance)
    {
        meadowsEnvSetup = __instance.m_biomes.Find(x => x.m_biome == Biome.Meadows);

        Desert(__instance);
        Jungle(__instance);
        Canyon(__instance);
    }

    private static void Desert(EnvMan envMan)
    {
        envMan.m_biomes.Add(new BiomeEnvSetup
        {
            m_name = nameof(Const.Desert),
            m_environments = meadowsEnvSetup.m_environments,
            m_musicMorning = meadowsEnvSetup.m_musicMorning,
            m_musicEvening = meadowsEnvSetup.m_musicEvening,
            m_musicDay = meadowsEnvSetup.m_musicDay,
            m_musicNight = meadowsEnvSetup.m_musicNight,
            m_biome = Const.Desert
        });
    }

    private static void Jungle(EnvMan envMan)
    {
        envMan.m_biomes.Add(new BiomeEnvSetup
        {
            m_name = nameof(Const.Jungle),
            m_environments = meadowsEnvSetup.m_environments,
            m_musicMorning = meadowsEnvSetup.m_musicMorning,
            m_musicEvening = meadowsEnvSetup.m_musicEvening,
            m_musicDay = meadowsEnvSetup.m_musicDay,
            m_musicNight = meadowsEnvSetup.m_musicNight,
            m_biome = Const.Jungle
        });
    }

    private static void Canyon(EnvMan envMan)
    {
        envMan.m_biomes.Add(new BiomeEnvSetup
        {
            m_name = nameof(Const.Canyon),
            m_environments = meadowsEnvSetup.m_environments,
            m_musicMorning = meadowsEnvSetup.m_musicMorning,
            m_musicEvening = meadowsEnvSetup.m_musicEvening,
            m_musicDay = meadowsEnvSetup.m_musicDay,
            m_musicNight = meadowsEnvSetup.m_musicNight,
            m_biome = Const.Canyon
        });
    }
}
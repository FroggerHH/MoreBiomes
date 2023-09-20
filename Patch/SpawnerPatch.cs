using HarmonyLib;

namespace MoreBiomes;

[HarmonyPatch]
public class SpawnerPatch
{
    [HarmonyPatch(typeof(CreatureSpawner), nameof(CreatureSpawner.Awake))]
    [HarmonyPostfix]
    private static void Postfix(CreatureSpawner __instance)
    {
        if (__instance.m_creaturePrefab) return;
        if (__instance.name.StartsWith("Spawner_Skeleton_Poison"))
            __instance.m_creaturePrefab = ZNetScene.instance.GetPrefab("Skeleton_Poison");
        else if (__instance.name.StartsWith("Spawner_Skeleton"))
            __instance.m_creaturePrefab = ZNetScene.instance.GetPrefab("Skeleton");
        else if (__instance.name.StartsWith("Spawner_Greydwarf_Elite"))
            __instance.m_creaturePrefab = ZNetScene.instance.GetPrefab("Greydwarf_Elite");
        else if (__instance.name.StartsWith("Spawner_Greydwarf_Shaman"))
            __instance.m_creaturePrefab = ZNetScene.instance.GetPrefab("Greydwarf_Shaman");
        else if (__instance.name.StartsWith("Spawner_Greydwarf"))
            __instance.m_creaturePrefab = ZNetScene.instance.GetPrefab("Greydwarf");
        else if (__instance.name.StartsWith("Spawner_Greyling"))
            __instance.m_creaturePrefab = ZNetScene.instance.GetPrefab("Greyling");
        else if (__instance.name.StartsWith("Spawner_Troll"))
            __instance.m_creaturePrefab = ZNetScene.instance.GetPrefab("Troll");
    }

    [HarmonyPatch(typeof(SpawnArea), nameof(SpawnArea.Awake))]
    [HarmonyPostfix]
    private static void Postfix(SpawnArea __instance)
    {
        if (__instance.m_prefabs.Count > 0) return;
        if (__instance.name.StartsWith("BonePileSpawner"))
            __instance.m_prefabs.Add(new SpawnArea.SpawnData
            {
                m_prefab = ZNetScene.instance.GetPrefab("Skeleton"),
                m_minLevel = 1,
                m_maxLevel = 3
            });
    }
}
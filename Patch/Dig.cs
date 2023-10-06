namespace MoreBiomes;

[HarmonyPatch]
public class Digs
{
    private static GameObject sand;
    private static readonly Dictionary<string, GameObject> mods = new();

    [HarmonyPatch(typeof(TerrainOp), nameof(TerrainOp.OnPlaced))] [HarmonyPrefix]
    public static void SwitchSpawnOnPlaced(TerrainOp __instance)
    {
        var biome = WorldGenerator.instance.GetBiome(__instance.transform.position);
        if (!mods.TryGetValue(__instance.gameObject.GetPrefabName(), out var def))
            mods.Add(__instance.gameObject.GetPrefabName(), __instance.m_spawnOnPlaced);

        if (biome == Desert)
            __instance.m_spawnOnPlaced = sand;
        else __instance.m_spawnOnPlaced = def;
    }


    [HarmonyPatch(typeof(ZNetScene), nameof(ZNetScene.Awake))] [HarmonyPostfix] [HarmonyWrapSafe]
    public static void RegisterSpawnObjects(ZNetScene __instance) { sand = __instance.GetPrefab("Sand"); }
}
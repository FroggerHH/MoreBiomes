using System.Collections.Generic;
using HarmonyLib;
using ItemManager;
using UnityEngine;
using static MoreBiomes.Plugin;
using static Heightmap;
using static Heightmap.Biome;
using static ZoneSystem;
using static ZoneSystem.ZoneVegetation;

namespace MoreBiomes;

[HarmonyPatch]
public class Digs
{
    private static GameObject sand;
    private static Dictionary<string, GameObject> mods = new();

    [HarmonyPatch(typeof(TerrainOp), nameof(TerrainOp.OnPlaced)), HarmonyPrefix]
    public static void SwitchSpawnOnPlaced(TerrainOp __instance)
    { 
        var biome = WorldGenerator.instance.GetBiome(__instance.transform.position);
        if (!mods.TryGetValue(__instance.gameObject.GetPrefabName(), out GameObject def))
        {
            mods.Add(__instance.gameObject.GetPrefabName(), __instance.m_spawnOnPlaced);
        }

        if (biome == Const.Desert)
            __instance.m_spawnOnPlaced = sand;
        else __instance.m_spawnOnPlaced = def;
    }


    [HarmonyPatch(typeof(ZNetScene), nameof(ZNetScene.Awake)), HarmonyPostfix, HarmonyWrapSafe]
    public static void RegisterSpawnObjects(ZNetScene __instance)
    {
        sand = __instance.GetPrefab("Sand");
    }
}
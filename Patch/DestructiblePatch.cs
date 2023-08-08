using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using HarmonyLib;
using UnityEngine;
using Random = UnityEngine.Random;
using static MoreBiomes.Plugin;

namespace MoreBiomes;

[HarmonyPatch, HarmonyWrapSafe]
public class DestructiblePatch
{
    [HarmonyPatch(typeof(ZNetScene), nameof(ZNetScene.Awake)), HarmonyWrapSafe]
    [HarmonyPostfix]
    static void Patch(ZNetScene __instance)
    {
        // var column_ruins = __instance.GetPrefab("column_ruins")?.GetComponent<Destructible>();
        // Debug("column_ruins");
        // var column_ruins_main = __instance.GetPrefab("column_ruins_main")?.GetComponent<Destructible>();
        // Debug("column_ruins_main");
        // var column_ruins_footing = __instance.GetPrefab("column_ruins_footing")?.GetComponent<Destructible>();
        // Debug("column_ruins_footing");

        // List<Piece.Requirement> requirements_10Stone = new();
        // requirements_10Stone.Add(new()
        // {
        //     m_resItem = ZNetScene.instance.GetPrefab("Stone").GetComponent<ItemDrop>(),
        //     m_amount = 10,
        //     m_recover = true
        // });
        //
        // column_ruins. = requirements_10Stone.ToArray();
        // column_ruins_main.m_resources = requirements_10Stone.ToArray();
        // column_ruins_footing.m_resources = requirements_10Stone.ToArray();
        // TreasureChest_ancienttemple.m_resources = requirements_10Stone.ToArray();
        // TreasureChest_DesertRuin.m_resources = requirements_10Stone.ToArray();
    }
}
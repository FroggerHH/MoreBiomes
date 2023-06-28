using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using HarmonyLib;
using UnityEngine;
using Random = UnityEngine.Random;

namespace MoreBiomes;

[HarmonyPatch(typeof(ZNetScene), nameof(ZNetScene.Awake))]
public class ChestPatch
{
    [HarmonyPostfix]
    static void ChestPatchPostfix(ZNetScene __instance)
    {
        var TreasureChest_DesertRuins = __instance.GetPrefab("TreasureChest_DesertRuins").GetComponent<Container>();
        var TreasureChest_ancienttemple =
            __instance.GetPrefab("TreasureChest_ancient-temple").GetComponent<Container>();
        TreasureChest_DesertRuins.m_defaultItems.m_drops.Add(
            new DropTable.DropData()
            {
                m_item = ZNetScene.instance.GetPrefab("Feathers"),
                m_stackMin = 2,
                m_stackMax = 25,
                m_weight = 1,
                m_dontScale = false
            });
        TreasureChest_DesertRuins.m_defaultItems.m_drops.Add(
            new DropTable.DropData()
            {
                m_item = ZNetScene.instance.GetPrefab("ArrowBronze"),
                m_stackMin = 1,
                m_stackMax = 5,
                m_weight = 1,
                m_dontScale = false
            });
        TreasureChest_DesertRuins.m_defaultItems.m_drops.Add(
            new DropTable.DropData()
            {
                m_item = ZNetScene.instance.GetPrefab("ArrowFire"),
                m_stackMin = 4,
                m_stackMax = 15,
                m_weight = 1,
                m_dontScale = false
            });
        TreasureChest_DesertRuins.m_defaultItems.m_drops.Add(
            new DropTable.DropData()
            {
                m_item = ZNetScene.instance.GetPrefab("Ruby"),
                m_stackMin = 1,
                m_stackMax = 2,
                m_weight = 1,
                m_dontScale = false
            });
        TreasureChest_DesertRuins.m_defaultItems.m_drops.Add(
            new DropTable.DropData()
            {
                m_item = ZNetScene.instance.GetPrefab("Coins"),
                m_stackMin = 0,
                m_stackMax = 100,
                m_weight = 1,
                m_dontScale = false
            });
        TreasureChest_DesertRuins.m_defaultItems.m_drops.Add(
            new DropTable.DropData()
            {
                m_item = ZNetScene.instance.GetPrefab("BoneFragments"),
                m_stackMin = 1,
                m_stackMax = 15,
                m_weight = 1,
                m_dontScale = false
            });

        if (Random.Range(0, 100) < 20)
        {
            TreasureChest_DesertRuins.m_defaultItems.m_drops.Add(
                new DropTable.DropData()
                {
                    m_item = ZNetScene.instance.GetPrefab("TrophySkeleton"),
                    m_stackMin = 1,
                    m_stackMax = 1,
                    m_weight = 1,
                    m_dontScale = false
                });
        }

        if (TreasureChest_DesertRuins.TryGetComponent(out EggHatch eggHatch))
        {
            eggHatch.m_spawnPrefab = ZNetScene.instance.GetPrefab("Skeleton");
        }


        //TreasureChest_ancienttemple
        TreasureChest_DesertRuins.m_defaultItems.m_drops.Add(
            new DropTable.DropData()
            {
                m_item = ZNetScene.instance.GetPrefab("Feathers"),
                m_stackMin = 2,
                m_stackMax = 25,
                m_weight = 1,
                m_dontScale = false
            });
        TreasureChest_DesertRuins.m_defaultItems.m_drops.Add(
            new DropTable.DropData()
            {
                m_item = ZNetScene.instance.GetPrefab("ArrowSilver"),
                m_stackMin = 1,
                m_stackMax = 5,
                m_weight = 1,
                m_dontScale = false
            });
        TreasureChest_DesertRuins.m_defaultItems.m_drops.Add(
            new DropTable.DropData()
            {
                m_item = ZNetScene.instance.GetPrefab("Stone"),
                m_stackMin = 4,
                m_stackMax = 15,
                m_weight = 1,
                m_dontScale = false
            });
        TreasureChest_DesertRuins.m_defaultItems.m_drops.Add(
            new DropTable.DropData()
            {
                m_item = ZNetScene.instance.GetPrefab("Ruby"),
                m_stackMin = 1,
                m_stackMax = 2,
                m_weight = 1,
                m_dontScale = false
            });
        TreasureChest_DesertRuins.m_defaultItems.m_drops.Add(
            new DropTable.DropData()
            {
                m_item = ZNetScene.instance.GetPrefab("Coins"),
                m_stackMin = 0,
                m_stackMax = 100,
                m_weight = 1,
                m_dontScale = false
            });
        TreasureChest_DesertRuins.m_defaultItems.m_drops.Add(
            new DropTable.DropData()
            {
                m_item = ZNetScene.instance.GetPrefab("BoneFragments"),
                m_stackMin = 1,
                m_stackMax = 15,
                m_weight = 1,
                m_dontScale = false
            });
        TreasureChest_DesertRuins.m_defaultItems.m_drops.Add(
            new DropTable.DropData()
            {
                m_item = ZNetScene.instance.GetPrefab("Acorn"),
                m_stackMin = 0,
                m_stackMax = 4,
                m_weight = 1,
                m_dontScale = false
            });

        if (Random.Range(0, 100) < 20)
        {
            TreasureChest_DesertRuins.m_defaultItems.m_drops.Add(
                new DropTable.DropData()
                {
                    m_item = ZNetScene.instance.GetPrefab("HelmetDverger"),
                    m_stackMin = 1,
                    m_stackMax = 1,
                    m_weight = 1,
                    m_dontScale = false
                });
        }

        if (TreasureChest_DesertRuins.TryGetComponent(out EggHatch eggHatch1))
        {
            eggHatch.m_spawnPrefab = ZNetScene.instance.GetPrefab("Skeleton");
        }
    }
}
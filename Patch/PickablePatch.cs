using HarmonyLib;

namespace MoreBiomes;

[HarmonyPatch(typeof(Pickable), nameof(Pickable.Awake))]
public class PickablePatch
{
    [HarmonyPostfix]
    private static void Postfix(Pickable __instance)
    {
        if (__instance.m_itemPrefab) return;
        if (__instance.name.StartsWith("Pickable_Mushroom_blue"))
            __instance.m_itemPrefab = ZNetScene.instance.GetPrefab("MushroomBlue");
        if (__instance.name.StartsWith("Pickable_Mushroom_yellow"))
            __instance.m_itemPrefab = ZNetScene.instance.GetPrefab("MushroomYellow");
        else if (__instance.name.StartsWith("Pickable_Mushroom"))
            __instance.m_itemPrefab = ZNetScene.instance.GetPrefab("Mushroom");
        if (__instance.name.StartsWith("Pickable_Dandelion"))
            __instance.m_itemPrefab = ZNetScene.instance.GetPrefab("Dandelion");
        if (__instance.name.StartsWith("Pickable_Flint"))
            __instance.m_itemPrefab = ZNetScene.instance.GetPrefab("Flint");
        if (__instance.name.StartsWith("Pickable_ShieldBanded"))
            __instance.m_itemPrefab = ZNetScene.instance.GetPrefab("ShieldBanded");
        if (__instance.name.StartsWith("Pickable_Branch"))
            __instance.m_itemPrefab = ZNetScene.instance.GetPrefab("Wood");
        if (__instance.name.StartsWith("Pickable_Coins"))
            __instance.m_itemPrefab = ZNetScene.instance.GetPrefab("Coins");
        if (__instance.name.StartsWith("Pickable_Stone"))
            __instance.m_itemPrefab = ZNetScene.instance.GetPrefab("Stone");
        if (__instance.name.StartsWith("Pickable_Bones"))
            __instance.m_itemPrefab = ZNetScene.instance.GetPrefab("BoneFragments");
        if (__instance.name.StartsWith("Pickable_ForestCryptRemains04"))
        {
            __instance.m_itemPrefab = ZNetScene.instance.GetPrefab("BoneFragments");
            if (__instance.TryGetComponent(out EggHatch eggHatch))
                eggHatch.m_spawnPrefab = ZNetScene.instance.GetPrefab("Skeleton");
        }

        if (__instance.name.StartsWith("Pickable_ForestCryptRemains03"))
        {
            __instance.m_itemPrefab = ZNetScene.instance.GetPrefab("BoneFragments");
            if (__instance.TryGetComponent(out EggHatch eggHatch))
                eggHatch.m_spawnPrefab = ZNetScene.instance.GetPrefab("Skeleton");
        }

        if (__instance.name.StartsWith("Pickable_Amber"))
        {
            __instance.m_itemPrefab = ZNetScene.instance.GetPrefab("Amber");
            if (__instance.TryGetComponent(out EggHatch eggHatch))
                eggHatch.m_spawnPrefab = ZNetScene.instance.GetPrefab("Skeleton_Poison");
        }

        if (__instance.name.StartsWith("Pickable_ForestCryptRemains01"))
        {
            __instance.m_itemPrefab = ZNetScene.instance.GetPrefab("BoneFragments");
            if (__instance.TryGetComponent(out EggHatch eggHatch))
                eggHatch.m_spawnPrefab = ZNetScene.instance.GetPrefab("Skeleton");
        }
    }
}
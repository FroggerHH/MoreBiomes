using System.Collections.Generic;
using System.Text;
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
public class Map
{
    [HarmonyPatch(typeof(Minimap), nameof(Minimap.UpdateBiome)), HarmonyPostfix]
    public static void Patch(Minimap __instance)
    {
        if (__instance.m_mode != Minimap.MapMode.Large) return;

        Vector3 worldPoint = __instance.ScreenToWorldPoint(ZInput.IsMouseActive()
            ? ZInput.mousePosition
            : new(Screen.width / 2, Screen.height / 2));
        var biome = WorldGenerator.instance.GetBiome(worldPoint);
        var wx = worldPoint.x;
        var wy = worldPoint.z;
        float magnitude = new Vector2(wx, wy).magnitude;
        float num = WorldGenerator.instance.WorldAngle(wx, wy) * 90f;

        var x = (WorldGenerator.instance.m_offset1 + wx) * (1f / 1000f);
        var y = (WorldGenerator.instance.m_offset1 + wy) * (1f / 1000f);
        var noise = Mathf.PerlinNoise(x, y);

        var sb = new StringBuilder();
        sb.AppendLine(Localization.instance.Localize("$biome_" + biome.ToString().ToLower()));
        sb.AppendLine($"Noise value is: {noise}");
        __instance.m_biomeNameLarge.text = sb.ToString();
    }
}
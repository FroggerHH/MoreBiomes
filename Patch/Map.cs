using MoreBiomes.Noise;

namespace MoreBiomes;

[HarmonyPatch]
public class Map
{
    private static Color[] mapTexture;
    private static Color[] combined;

    [HarmonyPatch(typeof(Minimap), nameof(Minimap.GenerateWorldMap))] [HarmonyPostfix]
    public static void GenerateWorldMap(Minimap __instance)
    {
        mapTexture = __instance.m_mapTexture.GetPixels();
        GenerateCombinedTexture(__instance);
    }

    private static void GenerateCombinedTexture(Minimap __instance)
    {
        combined = new Color[mapTexture.Length];
        var noiseMap = NoiseGenerator.GetColorMap();
        for (var i = 0; i < noiseMap.Length; i++)
        {
            var mapC = mapTexture[i];
            var noiceC = noiseMap[i];
            combined[i] = noiceC == Color.black ? Color.black : mapC;
        }
    }

    // [HarmonyPatch(typeof(Minimap), nameof(Minimap.UpdateBiome))] [HarmonyPostfix]
    // public static void Patch(Minimap __instance)
    // {
    //     if (advancedMapTooltip.Value == false) return;
    //     if (__instance.m_mode != Minimap.MapMode.Large) return;
    //
    //     Tooltip(__instance);
    //     ShowNoise(__instance);
    // }

    private static void RegenerateNoise_Debug() { GenerateCombinedTexture(Minimap.instance); }

    private static void ShowNoise(Minimap minimap)
    {
        if (Input.GetKeyDown(KeyCode.Keypad7)) RegenerateNoise_Debug();
        if (Input.GetKey(KeyCode.N))
            minimap.m_mapTexture.SetPixels(combined);
        else
            minimap.m_mapTexture.SetPixels(mapTexture);

        minimap.m_mapTexture.Apply();
    }

    // private static void Tooltip(Minimap __instance)
    // {
    //     var worldPoint = __instance.ScreenToWorldPoint(ZInput.IsMouseActive()
    //         ? ZInput.mousePosition
    //         : new Vector3(Screen.width / 2, Screen.height / 2));
    //     var biome = WorldGenerator.instance.GetBiome(worldPoint);
    //     var wx = worldPoint.x;
    //     var wy = worldPoint.z;
    //     var magnitude = new Vector2(wx, wy).magnitude;
    //     var num = WorldGenerator.instance.WorldAngle(wx, wy) * 90f;
    //
    //     var x = (WorldGenerator.instance.m_offset1 + wx) * (1f / 1000f);
    //     var y = (WorldGenerator.instance.m_offset1 + wy) * (1f / 1000f);
    //     var noise = Mathf.PerlinNoise(x, y);
    //     var baseHeight = WorldGenerator.instance.GetBaseHeight(wx, wy, false);
    //     var height = WorldGenerator.instance.GetHeight(wx, wy);
    //     var sb = new StringBuilder();
    //     sb.AppendLine(Localization.instance.Localize("$biome_" + biome.ToString().ToLower()));
    //     sb.AppendLine($"PerlinNoise is: {noise}");
    //     __instance.WorldToMapPoint(worldPoint, out var mapPosX_vanila, out var mapPosY_vanila);
    //     sb.AppendLine($"Noise is: {NoiseGenerator.GetPixel(worldPoint)}");
    //     sb.AppendLine($"BaseHeight is: {baseHeight}");
    //     sb.AppendLine($"Height is: {height}");
    //     sb.AppendLine($"Altitude is: {worldPoint.y - instance.m_waterLevel}");
    //     sb.AppendLine($"Altitude is: {worldPoint.y - instance.m_waterLevel}");
    //     __instance.m_biomeNameLarge.text = sb.ToString();
    // }
}
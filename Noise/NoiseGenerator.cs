using System.Collections.Generic;
using TestGener;
using UnityEngine;

public static class NoiseGenerator
{
    private static NoisePreset preset = NoisePreset.CreateDefaultInstance();
    private static (float[,], Color[]) cathedNoiseMap;
    private const int textureSize = 2048;
    private const int pixelSize = 12;

    public static float[,] GetNoiseMap()
    {
        if (cathedNoiseMap.Item1 == null || cathedNoiseMap.Item2 == null)
            CreateNoiseMap();
        return cathedNoiseMap.Item1;
    }

    /*public static Texture2D GetTexture()
    {
        if (cathedNoiseMap.Item1 == null || cathedNoiseMap.Item2 == null || cathedNoiseMap.Item3 == null)
            CreateNoiseMap();
        return cathedNoiseMap.Item2;
    }*/

    public static Color[] GetColorMap()
    {
        if (cathedNoiseMap.Item1 == null || cathedNoiseMap.Item2 == null || cathedNoiseMap.Item2 == null)
            CreateNoiseMap();
        return cathedNoiseMap.Item2;
    }

    private static float[,] CreateNoiseMap()
    {
        cathedNoiseMap.Item1 = CreateNoiseMap(preset);
        //cathedNoiseMap.Item2 = ToTexture(cathedNoiseMap.Item1);
        //Debug("CreateNoiseMap no arguments - ToTexture done");
        cathedNoiseMap.Item2 = GetColorMap(cathedNoiseMap.Item1);
        return cathedNoiseMap.Item1;
    }

    private static float[,] CreateNoiseMap(NoisePreset preset)
    {
        var debug = 0;
        var noiseMap = new float[preset.mapWidth, preset.mapHeight];
        var useLimit = preset.Limit != -1;

        var prng = new System.Random(preset.seed);
        var octaveOffsets = new Vector2[preset.octaves];
        for (var i = 0; i < preset.octaves; i++)
        {
            var offsetX = prng.Next(-100000, 100000) + preset.offset.x;
            var offsetY = prng.Next(-100000, 100000) + preset.offset.y;
            octaveOffsets[i] = new Vector2(offsetX, offsetY);
        }

        if (preset.noiseScale <= 0) preset.noiseScale = 0.0001f;

        var maxNoiseHeight = float.MinValue;
        var minNoiseHeight = float.MaxValue;

        var halfWidth = preset.mapWidth / 2f;
        var halfHeight = preset.mapHeight / 2f;


        for (var y = 0; y < preset.mapHeight; y++)
        for (var x = 0; x < preset.mapWidth; x++)
        {
            float amplitude = 1;
            float frequency = 1;
            float noiseHeight = 0;

            for (var i = 0; i < preset.octaves; i++)
            {
                var sampleX = (x - halfWidth) / preset.noiseScale * frequency + octaveOffsets[i].x;
                var sampleY = (y - halfHeight) / preset.noiseScale * frequency + octaveOffsets[i].y;

                var perlinValue = Mathf.PerlinNoise(sampleX, sampleY) * 2 - 1;
                noiseHeight += perlinValue * amplitude;

                amplitude *= preset.persistance;
                frequency *= preset.lacunarity;
            }

            if (noiseHeight > maxNoiseHeight) maxNoiseHeight = noiseHeight;
            else if (noiseHeight < minNoiseHeight) minNoiseHeight = noiseHeight;

            noiseHeight = Mathf.InverseLerp(minNoiseHeight, maxNoiseHeight, noiseHeight);

            if (useLimit) noiseHeight = noiseHeight < preset.Limit ? 0 : 1;

            noiseMap[x, y] = noiseHeight;
        }

        return noiseMap;
    }

    public static void SetPreset(NoisePreset preset) { NoiseGenerator.preset = preset; }

    /*private static Texture2D ToTexture(float[,] noiseMap)
    {
        Debug("ToTexture");
        var width = noiseMap.GetLength(0);
        var height = noiseMap.GetLength(1);

        Debug("ToTexture creating texture");
        var texture = new Texture2D(width, height);
        texture.filterMode = FilterMode.Point;
        Debug("ToTexture texture created");

        var colourMap = GetColorMap(noiseMap);
        Debug("ToTexture GetColorMap done");

        texture.SetPixels(colourMap);
        texture.Apply();
        return texture;
    }*/

    private static Color[] GetColorMap(float[,] noiseMap)
    {
        var width = noiseMap.GetLength(0);
        var height = noiseMap.GetLength(1);
        var colourMap = new Color[width * height];
        for (var y = 0; y < height; y++)
        for (var x = 0; x < width; x++)
            colourMap[y * width + x] = Color.Lerp(Color.black, Color.white, noiseMap[x, y]);
        return colourMap;
    }

    public static int GetPixel(Vector3 worldPoint)
    {
        var mapPos = WorldToMapPoint(worldPoint);
        return GetNoiseMap()[mapPos.x, mapPos.y] == 1 ? 1 : 0;
    }

    public static int GetPixel(float x, float y) => GetPixel(new Vector3(x, 0, y));

    internal static Vector2i WorldToMapPoint(Vector3 p)
    {
        var halfTextureSize = textureSize / 2;
        float mx = p.x / pixelSize + halfTextureSize;
        float my = p.z / pixelSize + halfTextureSize;

        //mx /= textureSize;
        // my /= textureSize;

        return new((int)mx, (int)my);
    }
}
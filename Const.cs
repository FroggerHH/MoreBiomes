using UnityEngine;

namespace MoreBiomes;

public static class Const
{
    public const Heightmap.Biome Desert = (Heightmap.Biome)1024;
    public const Heightmap.Biome Jungle = (Heightmap.Biome)2048;
    public const Heightmap.Biome Canyon = (Heightmap.Biome)4096;


    public static readonly Color32 desertColor = new(0, 0, 0, byte.MaxValue);
    public static readonly Color32 jungleColor = new(0, 62, 255, 197);
    public static readonly Color32 сanyonColor = new(0, 0, 0, 197);
}
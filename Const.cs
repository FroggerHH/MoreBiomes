namespace MoreBiomes;

public static class Const
{
    public const Biome Desert = (Biome)1024;
    public const Biome Jungle = (Biome)2048;
    public const Biome Canyon = (Biome)4096;
    public const Biome Siberia_snowy = (Biome)8192;
    public const Biome Siberia_steppe = (Biome)16384;


    public static readonly Color32 desertColor = new(0, 0, 0, byte.MaxValue);
    public static readonly Color32 jungleColor = new(0, 62, 255, 197);
    public static readonly Color32 сanyonColor = new(0, 0, 0, 197);
    public static readonly Color32 siberia_snowyColor = new(0, 0, 0, 197);
    public static readonly Color32 siberia_steppeColor = new(0, 0, 0, byte.MaxValue);

    public static string GetBiomeName(Biome biome)
    {
        return biome switch
        {
            Desert => "Desert",
            Jungle => "Jungle",
            Canyon => "Canyon",
            Siberia_snowy => "Siberia_snowy",
            Siberia_steppe => "Siberia_steppe"
        };
    }
}
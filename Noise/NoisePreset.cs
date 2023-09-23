namespace TestGener;

[Serializable]
public struct NoisePreset
{
    public int mapWidth;
    public int mapHeight;

    public float noiseScale;

    [Range(1, 15)]
    public int octaves;

    [Range(-2, 2)]
    public float persistance;

    [Range(-2, 2)]
    public float lacunarity;

    public int seed;
    public Vector2 offset;

    [SerializeField] private bool useLimit;

    [Range(0, 1)]
    [SerializeField]
    private float limit;

    public NoisePreset(int mapWidth, int mapHeight, float noiseScale, int octaves, float persistance,
        float lacunarity, int seed, Vector2 offset, bool useLimit, float limit)
    {
        this.mapWidth = mapWidth;
        this.mapHeight = mapHeight;
        this.noiseScale = noiseScale;
        this.octaves = octaves;
        this.persistance = persistance;
        this.lacunarity = lacunarity;
        this.seed = seed;
        this.offset = offset;
        this.useLimit = useLimit;
        this.limit = limit;
    }

    public float Limit => useLimit ? limit : -1;

    public static NoisePreset CreateDefaultInstance()
    {
        var instance = new NoisePreset();
        instance.mapWidth = 2048;
        instance.mapHeight = 2048;
        instance.noiseScale = 50;
        instance.octaves = 2;
        instance.persistance = 0.43f;
        instance.lacunarity = -0.8f;
        instance.seed = -1;
        instance.offset = Vector2.zero;
        instance.useLimit = true;
        instance.limit = 0.53f;
        return instance;
    }
}
public static class RandomUtils
{
    private static System.Random rng = new System.Random();
    private static int seed = -1;

    public static void ChangeSeed()
    {
        seed = UnityEngine.Random.Range(int.MinValue, int.MaxValue);
        ResetSeed();
    }

    public static void ResetSeed()
    {
        rng = new System.Random(seed);
    }
    
    public static float Range(float min, float max)
    {
        return (float)(rng.NextDouble() * (max - min) + min);
    }

    public static int Range(int minInclusive, int maxExclusive)
    {
        return rng.Next(minInclusive, maxExclusive);
    }
}

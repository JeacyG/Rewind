public class DeterministicRandom : IRandom
{
    private uint seed;

    public DeterministicRandom(uint seed)
    {
        this.seed = seed;
    }

    private uint NextUInt()
    {
        seed ^= seed << 13;
        seed ^= seed >> 17;
        seed ^= seed << 5;
        return seed;
    }
    
    public float Range(float min, float max)
    {
        return min + (NextUInt() / (float)uint.MaxValue) * (max - min);
    }

    public int Range(int min, int max)
    {
        return (int)(NextUInt() % (uint)(max - min)) + min;
    }
}

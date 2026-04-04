using System.Collections.Generic;

public class RandomContainer
{
    private Dictionary<string, DeterministicRandom> streams = new();

    public DeterministicRandom GetStream(string key, uint baseSeed)
    {
        if (!streams.TryGetValue(key, out DeterministicRandom rng))
        {
            uint seed = Hash(baseSeed, key);
            rng = new DeterministicRandom(seed);
            streams[key] = rng;
        }
        
        return rng;
    }

    private uint Hash(uint seed, string key)
    {
        unchecked
        {
            uint hash = seed;
            foreach (char c in key)
            {
                hash = hash * 31 + c;
            }
            return hash;
        }
    }
}

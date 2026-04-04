using UnityEngine;
using Random = UnityEngine.Random;

public class WaveManager : MonoBehaviour
{
    public static WaveManager Instance;
    
    private uint globalSeed;
    private int currentEnemyId = 0;

    private void Awake()
    {
        Instance = this;
        ResetForNewWave((uint)Random.Range(1, 10000));
    }

    public uint GetNextId()
    {
        return GameUtils.Hash(globalSeed, currentEnemyId++);
    }

    public void ResetForNewWave(uint newSeed)
    {
        globalSeed = newSeed;
        currentEnemyId = 0;
    }
}

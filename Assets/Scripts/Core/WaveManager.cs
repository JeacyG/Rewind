using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private EnemySpawner spawner;
    
    private uint waveSeed;
    
    public void Init(WaveConfig waveConfig, uint waveSeed, int currentRewind)
    {
        this.waveSeed = waveSeed;
    }

    public void StartWave()
    {
        ResetWorldState();
        
        spawner.Init(waveSeed);
        spawner.StartSpawning();
    }

    public void ResetWave()
    {
        ResetWorldState();
        
        spawner.ResetSpawner();
    }

    private void ResetWorldState()
    {
        
    }
}

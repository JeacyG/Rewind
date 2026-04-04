using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class RunManager : MonoBehaviour
{
    [SerializeField] private WaveManager waveManager;
    [SerializeField] private RewindManager rewindManager;
    
    [SerializeField] private WaveConfig waveConfig;
    
    private int currentWave;
    private int maxWave;
    private int currentRewind;
    private int rewindsLeft;
    private uint globalSeed;
    
    private int currentEnemyId;
    
    private InputAction rewindAction;
    
    private void Awake()
    {
        rewindAction = InputSystem.actions.FindAction("Rewind");
        rewindAction.performed += OnRewind;
    }

    private void OnDestroy()
    {
        rewindAction.performed -= OnRewind;
    }
    
    private void OnRewind(InputAction.CallbackContext context)
    {
        RequestRewind();
    }

    private void Start()
    {
        StartRun();
    }

    private void StartRun()
    {
        currentWave = 1;
        maxWave = 6;
        currentRewind = 0;
        rewindsLeft = 8;
        globalSeed = (uint)Random.Range(1, int.MaxValue);

        currentEnemyId = 0;
        
        StartWave();
    }

    private void StartWave()
    {
        uint waveSeed = GameUtils.Hash(globalSeed, currentWave);

        waveManager.Init(waveConfig, waveSeed, currentRewind);
    }
    
    public void RequestRewind()
    {
        if (rewindsLeft <= 0)
        {
            EndRun(false);
            return;
        }
        
        rewindsLeft--;
        currentRewind++;
        
        rewindManager.PrepareRewind(currentRewind);
        RestartWave();
    }

    private void RestartWave()
    {
        currentEnemyId = 0;
        waveManager.ResetWave();
    }

    public void OnWaveCompleted()
    {
        currentWave++;
        currentRewind = 0;
        
        if (currentWave > maxWave)
            EndRun(true);
        else
            StartWave();
    }

    private void EndRun(bool isWin)
    {
        
    }
    
    public uint GetNextEnemySeed()
    {
        return GameUtils.Hash(globalSeed, currentEnemyId++);
    }
}

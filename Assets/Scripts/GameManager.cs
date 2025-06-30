using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private CloneManager cloneManager;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private EnemySpawner enemySpawner;

    private void Awake()
    {
        RandomUtils.ChangeSeed();
        
        enemySpawner.Initialize(spawnPoint);
    }

    public void Rewind(List<ActionData> actions)
    {
        RandomUtils.ResetSeed();
        cloneManager.CreateNewClone(actions, spawnPoint);
        cloneManager.ResetAllClones();
        enemySpawner.ResetSpawner();
    }

    public Transform GetSpawnPoint()
    {
        return spawnPoint;
    }
}

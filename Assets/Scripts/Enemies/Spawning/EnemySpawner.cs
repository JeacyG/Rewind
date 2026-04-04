using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Vector2 edgeSize;
    [SerializeField] private float edgeRadius;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float spawnInterval;
    [SerializeField] private float spawnDelta;
    [SerializeField] private bool bAutoSpawn;
    
    private List<Enemy> enemies = new();

    private bool isInitialized = false;
    private bool isSpawning = false;
    private float timer;

    private uint seed;
    private RandomContainer random;

    private IRandom randomTiming;
    private IRandom randomPosition;

    public void Init(uint seed)
    {
        this.seed = seed;
        random = new RandomContainer();

        randomTiming = random.GetStream("timing", seed);
        randomPosition = random.GetStream("position", seed);

        isInitialized = true;
    }

    private void Update()
    {
        if (!isSpawning)
            return;
        
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            SpawnEnemy();
            
            float waitTime = spawnInterval + randomTiming.Range(-spawnDelta, spawnDelta);
            timer += waitTime;
        }
    }

    public void StartSpawning()
    {
        if (!isInitialized)
        {
            Debug.LogError("Spawner not initialized");
            return;
        }
        
        isSpawning = true;
        
        timer = spawnInterval + randomTiming.Range(-spawnDelta, spawnDelta);
    }

    public void StopSpawning()
    {
        isSpawning = false;
    }

    public void ResetSpawner()
    {
        StopSpawning();
        KillAll();

        random = new RandomContainer();
        
        randomTiming = random.GetStream("timing", seed);
        randomPosition = random.GetStream("position", seed);
        
        StartSpawning();
    }

    public void KillAll()
    {
        foreach (Enemy enemy in enemies)
        {
            if (!enemy.IsUnityNull())
            {
                Destroy(enemy.gameObject);
            }
        }
        
        enemies.Clear();
    }

    public void SpawnOne()
    {
        SpawnEnemy();
    }

    private void SpawnEnemy()
    {
        Enemy enemy = EnemyFactory.CreateChasePlayerEnemy(
            enemyPrefab,
            GetRandomPositionInSpawnArea(),
            Quaternion.identity,
            transform,
            WaveManager.Instance.GetNextId()
        );
        
        enemies.Add(enemy);
    }

    private Vector2 GetRandomPositionInSpawnArea()
    {
        Vector2 center = transform.position;
        float halfWidth = edgeSize.x / 2f;
        float halfHeight = edgeSize.y / 2f;

        int side = randomPosition.Range(0, 4);

        float x = 0f, y = 0f;
        switch (side)
        {
            case 0: // Top
                x = randomPosition.Range(center.x - halfWidth - edgeRadius, center.x + halfWidth + edgeRadius);
                y = randomPosition.Range(center.y + halfHeight - edgeRadius, center.y + halfHeight + edgeRadius);
                break;
            case 1: // Bottom
                x = randomPosition.Range(center.x - halfWidth - edgeRadius, center.x + halfWidth + edgeRadius);
                y = randomPosition.Range(center.y - halfHeight - edgeRadius, center.y - halfHeight + edgeRadius);
                break;
            case 2: // Left
                x = randomPosition.Range(center.x - halfWidth - edgeRadius, center.x - halfWidth + edgeRadius);
                y = randomPosition.Range(center.y - halfHeight + edgeRadius, center.y + halfHeight - edgeRadius);
                break;
            case 3: // Right
                x = randomPosition.Range(center.x + halfWidth - edgeRadius, center.x + halfWidth + edgeRadius);
                y = randomPosition.Range(center.y - halfHeight + edgeRadius, center.y + halfHeight - edgeRadius);
                break;
        }
        
        return new Vector2(x, y);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, edgeSize - edgeRadius * Vector2.one);
        Gizmos.DrawWireCube(transform.position, edgeSize + edgeRadius * Vector2.one);
    }
}

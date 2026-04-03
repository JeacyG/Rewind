using System;
using System.Collections;
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
    
    private Coroutine spawnCoroutine;
    
    private List<Enemy> enemies = new List<Enemy>();

    private void OnDestroy()
    {
        StopSpawning();
    }

    private void Start()
    {
        if (bAutoSpawn)
            StartSpawning();
    }

    public void ResetSpawner()
    {
        StopSpawning();
        StartSpawning();
    }

    private void StartSpawning()
    {
        if (spawnCoroutine == null)
        {
            spawnCoroutine = StartCoroutine(SpawnCoroutine());
        }
    }

    public void StopSpawning()
    {
        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
            spawnCoroutine = null;
        }
        
        KillAll();
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

    private IEnumerator SpawnCoroutine()
    {
        while (true)
        {
            SpawnEnemy();
        
            float waitTime = spawnInterval + RandomUtils.Range(-spawnDelta, spawnDelta);
            yield return new WaitForSeconds(waitTime);
        }
    }

    private void SpawnEnemy()
    {
        Enemy enemy = EnemyFactory.CreateChasePlayerEnemy(
            enemyPrefab,
            GetRandomPositionInSpawnArea(),
            Quaternion.identity,
            transform
        );
        
        enemies.Add(enemy);
    }

    public Enemy GetEnemy(int index)
    {
        return enemies[index];
    }

    private Vector2 GetRandomPositionInSpawnArea()
    {
        Vector2 center = transform.position;
        float halfWidth = edgeSize.x / 2f;
        float halfHeight = edgeSize.y / 2f;

        int side = RandomUtils.Range(0, 4);

        float x = 0f, y = 0f;
        switch (side)
        {
            case 0: // Top
                x = RandomUtils.Range(center.x - halfWidth - edgeRadius, center.x + halfWidth + edgeRadius);
                y = RandomUtils.Range(center.y + halfHeight - edgeRadius, center.y + halfHeight + edgeRadius);
                break;
            case 1: // Bottom
                x = RandomUtils.Range(center.x - halfWidth - edgeRadius, center.x + halfWidth + edgeRadius);
                y = RandomUtils.Range(center.y - halfHeight - edgeRadius, center.y - halfHeight + edgeRadius);
                break;
            case 2: // Left
                x = RandomUtils.Range(center.x - halfWidth - edgeRadius, center.x - halfWidth + edgeRadius);
                y = RandomUtils.Range(center.y - halfHeight + edgeRadius, center.y + halfHeight - edgeRadius);
                break;
            case 3: // Right
                x = RandomUtils.Range(center.x + halfWidth - edgeRadius, center.x + halfWidth + edgeRadius);
                y = RandomUtils.Range(center.y - halfHeight + edgeRadius, center.y + halfHeight - edgeRadius);
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

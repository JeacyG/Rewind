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

    private Transform target;
    private Coroutine spawnCoroutine;
    
    private List<EnemyController> enemies = new List<EnemyController>();
    private int enemyIndex = 0;

    private void OnDestroy()
    {
        StopSpawning();
    }

    public void Initialize(Transform target)
    {
        this.target = target;
        StartSpawning();
    }
    
    public void ResetSpawner()
    {
        enemyIndex = 0;
        StopSpawning();
        StartSpawning();
    }

    private void StartSpawning()
    {
        if (spawnCoroutine.IsUnityNull())
        {
            spawnCoroutine = StartCoroutine(SpawnCoroutine());
        }
    }

    private void StopSpawning()
    {
        if (!spawnCoroutine.IsUnityNull())
        {
            StopCoroutine(spawnCoroutine);
            spawnCoroutine = null;
        }
        
        KillAll();
    }

    private void KillAll()
    {
        foreach (EnemyController enemy in enemies)
        {
            if (!enemy.IsUnityNull())
            {
                Destroy(enemy.gameObject);
            }
        }
        
        enemies.Clear();
    }

    private IEnumerator SpawnCoroutine()
    {
        while (true)
        {
            SpawnEnemy();
        
            float waitTime = spawnInterval + RandomUtils.Range(-spawnDelta, spawnDelta);
            yield return new WaitForSeconds(waitTime);
        }
        // ReSharper disable once IteratorNeverReturns
    }

    private void SpawnEnemy()
    {
        EnemyController controller = GameObject.Instantiate(enemyPrefab, GetRandomPositionInSpawnArea(), Quaternion.identity, transform).GetComponent<EnemyController>();
        controller.Initialize(target, enemyIndex);
        enemies.Add(controller);
        enemyIndex++;
    }

    public EnemyController GetEnemy(int index)
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

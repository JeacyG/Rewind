using UnityEngine;

public static class EnemyFactory
{
    public static Enemy CreateChasePlayerEnemy(GameObject prefab, Vector2 position, Quaternion rotation, Transform parent, uint seed)
    {
        GameObject go = GameObject.Instantiate(prefab, position, rotation, parent);
        Enemy enemy = go.GetComponent<Enemy>();
        
        enemy.Init(
            new ChaseMovement(() => PlayerManager.Instance.GetClosestPlayer(enemy.transform.position),
                3f,
                1f),
            new ShootAttack(),
            new SimpleBrain(),
            seed
        );

        return enemy;
    }
}

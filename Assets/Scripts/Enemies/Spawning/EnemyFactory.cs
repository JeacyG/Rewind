using UnityEngine;

public static class EnemyFactory
{
    public static Enemy CreateBasicEnemy(GameObject prefab, int seed)
    {
        GameObject go = GameObject.Instantiate(prefab);
        Enemy enemy = go.GetComponent<Enemy>();
        
        enemy.Init(
            new RandomMovement(),
            new ShootAttack(),
            new SimpleBrain()
        );

        return enemy;
    }

    public static Enemy CreateChasePlayerEnemy(GameObject prefab, Vector2 position, Quaternion rotation, Transform parent)
    {
        GameObject go = GameObject.Instantiate(prefab, position, rotation, parent);
        Enemy enemy = go.GetComponent<Enemy>();
        
        enemy.Init(
            new ChaseMovement(() => PlayerManager.Instance.GetClosestPlayer(enemy.transform.position),
                3f,
                1f),
            new ShootAttack(),
            new SimpleBrain()
        );

        return enemy;
    }
}

using UnityEngine;

public class RandomMovement : IMovement
{
    private IRandom rng;
    
    public void Init(IRandom rng)
    {
        this.rng = rng;
    }

    public void Tick(Enemy enemy, float deltaTime)
    {
        Vector2 dir = new Vector2(
            RandomUtils.Range(0f, 1f),
            RandomUtils.Range(0f, 1f)
        );

        enemy.transform.position += (Vector3)(dir * deltaTime * 2f);
    }
}

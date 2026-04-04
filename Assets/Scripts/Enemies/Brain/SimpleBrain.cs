using UnityEngine;

public class SimpleBrain : IEnemyBrain
{
    private IRandom rng;
    
    public void Init(IRandom rng)
    {
        this.rng = rng;
    }

    public void Tick(Enemy enemy, float deltaTime)
    {
        enemy.Movement?.Tick(enemy, deltaTime);
        enemy.Attack?.Tick(enemy, deltaTime);
    }
}

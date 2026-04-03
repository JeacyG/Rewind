using UnityEngine;

public class SimpleBrain : IEnemyBrain
{
    public void Tick(Enemy enemy, float deltaTime)
    {
        enemy.Movement?.Tick(enemy, deltaTime);
        enemy.Attack?.Tick(enemy, deltaTime);
    }
}

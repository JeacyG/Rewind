using UnityEngine;

public class ShootAttack : IAttack
{
    private float cooldown = 1f;
    private float timer;
    
    public void Tick(Enemy enemy, float deltaTime)
    {
        timer -= deltaTime;

        if (timer <= 0f)
        {
            Debug.Log("Enemy shoot");
            timer = cooldown;
        }
    }
}

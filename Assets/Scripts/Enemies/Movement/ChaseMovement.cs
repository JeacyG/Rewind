using Unity.VisualScripting;
using UnityEngine;

public class ChaseMovement : IMovement
{
    private System.Func<Transform> getTarget;
    private float speed;
    private float stopDistance;

    public ChaseMovement(System.Func<Transform> getTarget, float speed, float stopDistance)
    {
        this.getTarget = getTarget;
        this.speed = speed;
        this.stopDistance = stopDistance;
    }

    public void Init(IRandom rng)
    {
        
    }

    public void Tick(Enemy enemy, float deltaTime)
    {
        Transform target = getTarget?.Invoke();
        if (target.IsUnityNull())
            return;

        Vector2 dir = (target.position - enemy.transform.position);

        if (dir.magnitude <= stopDistance)
            return;
        
        dir.Normalize();
        
        enemy.transform.position += (Vector3)(dir * speed * deltaTime);
    }
}

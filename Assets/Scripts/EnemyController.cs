using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Damageable damageable;
    
    private Transform target;
    private int ID = -1;

    public void Initialize(Transform target, int enemyIndex)
    {
        this.target = target;
        this.ID = enemyIndex;

        damageable.OnDeath += OnDeathReveived;
    }

    private void OnDestroy()
    {
        damageable.OnDeath -= OnDeathReveived;
    }

    private void FixedUpdate()
    {
        if (!target.IsUnityNull())
        {
            Vector2 direction = target.position - transform.position;
            direction.Normalize();
            
            rb.linearVelocity = speed * direction;
            transform.rotation = GameUtils.GetRotationFromDirection(direction);
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }
    }
    
    private void OnDeathReveived()
    {
        Destroy(this.gameObject);
    }

    public void TakeDamage(int damage)
    {
        damageable.TakeDamage(damage);
    }

    public int GetID()
    {
        return ID;
    }
}

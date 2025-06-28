using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody2D rb;
    
    private Transform target;

    public void Initialize(Transform target)
    {
        this.target = target;
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
}

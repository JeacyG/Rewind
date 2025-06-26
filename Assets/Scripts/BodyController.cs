using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BodyController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;

    private float speed = 4f;

    public void ChangeVelocity(Vector2 moveInput)
    {
        if (moveInput != Vector2.zero)
        {
            rb.linearVelocity = speed * moveInput;
        
            float angle = Mathf.Atan2(moveInput.y, moveInput.x) * Mathf.Rad2Deg - 90f;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }
    }

    public void ResetVelocity()
    {
        rb.linearVelocity = Vector2.zero;
    }
}

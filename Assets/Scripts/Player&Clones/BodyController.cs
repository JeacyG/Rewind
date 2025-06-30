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
            transform.rotation = GameUtils.GetRotationFromDirection(moveInput);
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

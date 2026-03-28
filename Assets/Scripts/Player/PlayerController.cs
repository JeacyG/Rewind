using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private WeaponBase weapon;
    
    public void Simulate(PlayerInput input)
    {
        float speed = 8f * Time.fixedDeltaTime;
        Vector2 move = input.Move;
        transform.position += new Vector3(move.x * speed, move.y * speed, 0);

        Vector2 dir = input.Aim;
        if (dir.sqrMagnitude > 0.001f)
        {
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
        else if (move.sqrMagnitude > 0.001f)
        {
            float angle = Mathf.Atan2(move.y, move.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
        
        if (input.Shoot)
            weapon.TryUse();
        weapon.Tick(Time.fixedDeltaTime);
    }
}

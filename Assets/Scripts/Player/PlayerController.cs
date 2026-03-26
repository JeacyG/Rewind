using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;

    private void Awake()
    {
        
    }

    public void Simulate(PlayerInput input)
    {
        float speed = 8f;
        Vector2 move = input.Move;
        transform.position += new Vector3(move.x * speed, move.y * speed, 0);

        Vector2 dir = input.Aim;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        if (input.Shoot)
            Shoot();
    }

    private void Shoot()
    {
        Debug.Log("BAM!");
    }
}

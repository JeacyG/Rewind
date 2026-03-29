using UnityEngine;

public class Gun : WeaponBase
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float cooldown = 0.3f;

    private float timer = 0f;

    public override void Tick(float deltaTime)
    {
        if (timer > 0f)
            timer -= deltaTime;
    }

    public override void TryUse()
    {
        if (timer > 0f)
            return;

        Fire();
        
        timer = cooldown;
    }

    private void Fire()
    {
        Instantiate(bulletPrefab, transform.position, transform.rotation);
    }
}

using System.Collections.Generic;
using UnityEngine;

public class DamageZone : MonoBehaviour, IDamageZone
{
    private List<EnemyController> damageables = new List<EnemyController>();
    
    public void Damage(int damage, out List<HitData> hitData)
    {
        hitData = new List<HitData>();
        for (int index = 0; index < damageables.Count; index++)
        {
            EnemyController controller = damageables[index];
            hitData.Add(new HitData(controller.GetID(), damage));
            controller.GetDamageable().TakeDamage(damage);
        }
    }

    public void ResetDamageZone()
    {
        damageables.Clear();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(GameUtils.TAG_ENEMY))
        {
            damageables.Add(other.GetComponentInParent<EnemyController>());
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(GameUtils.TAG_ENEMY))
        {
            damageables.Remove(other.GetComponentInParent<EnemyController>());
        }
    }
}

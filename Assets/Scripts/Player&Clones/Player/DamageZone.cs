using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZone : MonoBehaviour, IDamageZone
{
    [Header("Debug")]
    [SerializeField] private SpriteRenderer debugZone;
    [SerializeField, Range(0f, 1f)] private float idleAlpha;
    [SerializeField] private float flickerDebugDuration;
    
    public event Action<List<HitData>> OnEnemiesHit;
    
    private List<EnemyController> enemiesInZone = new List<EnemyController>();

    private void Awake()
    {
        SetAlpha(idleAlpha);
    }

    public void Damage(int damage)
    {
        StartCoroutine(DebugFlicker());
        
        List<HitData> hitData = new List<HitData>();
        for (int index = 0; index < enemiesInZone.Count; index++)
        {
            EnemyController controller = enemiesInZone[index];
            hitData.Add(new HitData(controller.GetID(), damage));
            controller.TakeDamage(damage);
        }

        if (hitData.Count > 0)
        {
            OnEnemiesHit?.Invoke(hitData);
        }
    }

    private IEnumerator DebugFlicker()
    {
        SetAlpha(1f);
        yield return new WaitForSeconds(flickerDebugDuration);
        SetAlpha(idleAlpha);
    }

    private void SetAlpha(float alpha)
    {
        Color currentColor = debugZone.color;
        currentColor.a = alpha;
        debugZone.color = currentColor;
    }

    public void ResetDamageZone()
    {
        enemiesInZone.Clear();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(GameUtils.TAG_ENEMY))
        {
            enemiesInZone.Add(other.GetComponentInParent<EnemyController>());
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(GameUtils.TAG_ENEMY))
        {
            enemiesInZone.Remove(other.GetComponentInParent<EnemyController>());
        }
    }
}

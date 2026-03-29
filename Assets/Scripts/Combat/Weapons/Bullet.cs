using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    
    private float speed = 16f;
    private float lifeTime = 2f;

    private void Awake()
    {
        rb.linearVelocity = transform.right * speed;
        StartCoroutine(LifeTimeCoroutine());
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    private IEnumerator LifeTimeCoroutine()
    {
        float timeLeft = lifeTime;

        while (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            yield return null;
        }
        
        Destroy(gameObject);
    }
}

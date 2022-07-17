using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private int damage = 1;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rb.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player" || collider.gameObject.tag == "Bullet")
        {
            if(collider.gameObject.CompareTag("Player"))
            {
                if(collider.gameObject.TryGetComponent<UnitStats>(out UnitStats stats))
                {
                    stats.onTakeDamage(damage, transform.position);
                }
                if(collider.gameObject.TryGetComponent<CharacterHealth>(out CharacterHealth health))
                {
                    health.Hit(damage);
                }
            }
            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}

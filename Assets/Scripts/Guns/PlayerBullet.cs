using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
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
        if (collider.gameObject.tag == "Enemy" || collider.gameObject.tag == "Bullet")
        {
            if(collider.gameObject.CompareTag("Enemy"))
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
            Debug.Log("hit");

            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.transform.name == "BulletTrigger")
        {
            Vector3 direction = Camera.main.WorldToScreenPoint(collision.transform.position) - Camera.main.WorldToScreenPoint(transform.position);
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
}

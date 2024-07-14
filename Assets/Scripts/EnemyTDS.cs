using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTDS : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float fireRate = 1f;
    public GameObject bulletPrefab;
    public Transform firePoint;

    private Transform player;
    private Rigidbody2D rb;
    private float nextFireTime;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        nextFireTime = Time.time + Random.Range(1.5f, 2.5f);
    }

    void Update()
    {
        if (player != null)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            rb.velocity = direction * moveSpeed;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            rb.rotation = angle;
            if (Time.time >= nextFireTime)
            {
                Shoot();
                nextFireTime = Time.time + 1f / fireRate;
            }
        }
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("BalaPlayer"))
        {
            Destroy(other.gameObject);
            TakeDamage();
        }
    }

    void TakeDamage()
    {
        Destroy(gameObject);
    }
}
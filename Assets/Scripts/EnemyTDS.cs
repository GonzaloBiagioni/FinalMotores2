using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTDS : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float fireRate = 2f;
    public float detectionRange = 10f;
    public int maxHealth = 3;
    public GameObject bulletPrefab;
    public GameObject coinPrefab;
    public Transform firePoint;

    private float nextFireTime;
    private int currentHealth;
    private Transform player;

    void Start()
    {
        currentHealth = maxHealth;
        nextFireTime = Time.time + fireRate;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceToPlayer <= detectionRange)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);

            if (Time.time >= nextFireTime)
            {
                Shoot(direction);
                nextFireTime = Time.time + fireRate;
            }
        }
    }

    void Shoot(Vector2 direction)
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = direction * moveSpeed;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            DestroyEnemy();
        }
        else if (other.CompareTag("BalaPlayer"))
        {
            TakeDamage();
            Destroy(other.gameObject);
        }
    }

    void TakeDamage()
    {
        currentHealth--;
        if (currentHealth <= 0)
        {
            DestroyEnemy();
        }
    }

    void DestroyEnemy()
    {
        if (coinPrefab != null)
        {
            Instantiate(coinPrefab, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}
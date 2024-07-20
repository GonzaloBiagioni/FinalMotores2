using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniJefe : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 2f;
    public GameObject bulletPrefab;
    public Transform firepoint1;
    public Transform firepoint2;
    public float fireRate = 1f;
    public float detectionRange = 10f;
    public int maxHealth = 10;
    public GameObject explosionPrefab;
    private int currentHealth;
    private bool isActive = false;
    private float nextFireTime = 0f;
    private MovementPlayer playerMovement;

    void Start()
    {
        currentHealth = maxHealth;
        playerMovement = FindObjectOfType<MovementPlayer>();
    }

    void Update()
    {
        if (isActive)
        {
            Vector3 targetPosition = new Vector3(transform.position.x, player.position.y, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            if (Time.time > nextFireTime)
            {
                Shoot();
                nextFireTime = Time.time + fireRate;
            }
        }
        else
        {
            if (Vector3.Distance(transform.position, player.position) <= detectionRange)
            {
                isActive = true;
            }
        }
    }

    void Shoot()
    {
        AudioManager.Instance.PlaySFX(2);
        Instantiate(bulletPrefab, firepoint1.position, firepoint1.rotation);
        Instantiate(bulletPrefab, firepoint2.position, firepoint2.rotation);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isActive = true;
        }

        if (other.CompareTag("BalaPlayer"))
        {
            TakeDamage(1);
            Destroy(other.gameObject);
        }
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        playerMovement.EnableFreeMovement();

        if (explosionPrefab != null)
        {
            AudioManager.Instance.PlaySFX(6);
            Instantiate(explosionPrefab, transform.position, transform.rotation);
        }

        Destroy(gameObject);
    }
}
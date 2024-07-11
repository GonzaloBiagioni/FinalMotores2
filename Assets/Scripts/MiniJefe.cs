using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniJefe : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 2f;
    public GameObject bulletPrefab;
    public float fireRate = 1f;
    public float detectionRange = 10f;
    public int maxHealth = 10;
    private int currentHealth;
    private bool isActive = false;
    private float nextFireTime = 0f;
    public Transform firepoint1;
    public Transform firepoint2;
    public ScrollCamara cameraFollow; // Referencia al script de la cámara

    void Start()
    {
        currentHealth = maxHealth;
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
        Destroy(gameObject);
        player.GetComponent<MovementPlayer>().EnableFreeMovement();
        cameraFollow.StartFollowingPlayer(); // Iniciar el seguimiento del jugador en modo top-down
    }
}
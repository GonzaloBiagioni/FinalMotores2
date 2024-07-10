using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniJefe : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 2f;
    public GameObject bulletPrefab;
    public float fireRate = 1f;
    public Transform firePoint1;
    public Transform firePoint2;
    public float detectionRange = 10f;
    public int maxHealth = 10;
    private int currentHealth;
    private bool isActive = false;
    private float nextFireTime = 0f;
    public GameObject transitionZone;
    public ScrollCamara cameraController; // Nueva referencia a la cámara
    public MovementPlayer playerController; // Nueva referencia al jugador



    void Start()
    {
        currentHealth = maxHealth;
        transitionZone.SetActive(true); // Asegúrate de que la zona de transición esté activa al inicio
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
                nextFireTime = Time.time + 1f / fireRate;
                //AudioManager.Instance.PlaySFX(1);
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
        Instantiate(bulletPrefab, firePoint1.position, firePoint1.rotation);
        Instantiate(bulletPrefab, firePoint2.position, firePoint2.rotation);
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
        AudioManager.Instance.PlaySFX(2);
        transitionZone.SetActive(false); // Desactiva la zona de transición al morir
        cameraController.StopMoving(); // Detiene el movimiento de la cámara
        cameraController.StartFollowingPlayer(); // Hace que la cámara siga al jugador
        playerController.EnableFreeMovement(); // Habilita el movimiento libre del jugador
        playerController.EnableShooting(); // Habilita el disparo del jugador
        Destroy(gameObject);
    }
}
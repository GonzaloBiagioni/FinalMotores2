using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBossTDS : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public float rotationSpeed = 5f;
    public float fireRate = 1f; 
    public GameObject bulletPrefab;
    public Transform firePoint1;
    public Transform firePoint2;
    public Transform firePoint3;
    public Transform firePoint4;

    private float nextFireTime;
    private bool isActive = false;

    private void Start()
    {
        currentHealth = maxHealth;
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (isActive)
        {
            RotateTowardsPlayer();
            if (Time.time > nextFireTime)
            {
                Shoot();
                nextFireTime = Time.time + fireRate; 
            }
        }
    }

    private void RotateTowardsPlayer()
    {
        if (PlayerIsInRange())
        {
            Vector3 direction = PlayerDirection();
            Quaternion lookRotation = Quaternion.LookRotation(Vector3.forward, direction);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
        }
    }

    private bool PlayerIsInRange()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            float distance = Vector2.Distance(transform.position, player.transform.position);
            return distance < 20f; 
        }
        return false;
    }

    private Vector3 PlayerDirection()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            return player.transform.position - transform.position;
        }
        return Vector3.zero;
    }

    private void Shoot()
    {
        Instantiate(bulletPrefab, firePoint1.position, firePoint1.rotation);
        Instantiate(bulletPrefab, firePoint2.position, firePoint2.rotation);
        Instantiate(bulletPrefab, firePoint3.position, firePoint3.rotation);
        Instantiate(bulletPrefab, firePoint4.position, firePoint4.rotation);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("BalaPlayer"))
        {
            TakeDamage();
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("BossTrigger"))
        {
            ActivateBoss();
        }
    }

    public void ActivateBoss()
    {
        isActive = true;
        gameObject.SetActive(true);
    }

    private void TakeDamage()
    {
        currentHealth -= 1;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
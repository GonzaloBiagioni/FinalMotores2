using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPlayer : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 movement;
    public GameObject bala;
    public Transform firepoint;
    public float fireRate = 15f;
    private float nextFireTime = 0.5f;
    private bool canMove = true;
    private bool canShoot = false;
    private bool isFreeMovement = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (canMove)
        {
            float moveHorizontal = Input.GetAxisRaw("Horizontal");
            float moveVertical = Input.GetAxisRaw("Vertical");
            movement = new Vector2(moveHorizontal, moveVertical).normalized;

            if (canShoot && Input.GetMouseButtonDown(0) && Time.time > nextFireTime)
            {
                AudioManager.Instance.PlaySFX(0);
                Shoot();
                nextFireTime = Time.time + 2f / fireRate;
            }
        }
    }

    private void Shoot()
    {
        Instantiate(bala, firepoint.position, firepoint.rotation);
    }

    void FixedUpdate()
    {
        if (canMove)
        {
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        }
    }

    public void EnableShooting()
    {
        canShoot = true;
    }

    public void EnableFreeMovement()
    {
        isFreeMovement = true;
    }
}
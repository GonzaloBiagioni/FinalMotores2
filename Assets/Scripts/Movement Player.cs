using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPlayer : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 movement;
    public GameObject bala;
    public Transform firepoint1;
    public Transform firepoint2;
    public float fireRate = 15f;
    private float nextFireTime = 0.5f;
    private bool canMove = true;
    private bool canShoot = true;
    private bool isFreeMovement = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        HideCursor();
    }

    void Update()
    {
        HandleMovementInput();
        HandleShootingInput();
    }

    private void HandleMovementInput()
    {
        if (canMove)
        {
            float moveHorizontal = Input.GetAxisRaw("Horizontal");
            float moveVertical = Input.GetAxisRaw("Vertical");
            movement = new Vector2(moveHorizontal, moveVertical).normalized;
        }
    }

    private void HandleShootingInput()
    {
        if (canShoot && Input.GetMouseButtonDown(0) && Time.time > nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + 2f / fireRate;
        }
    }

    private void Shoot()
    {
        Instantiate(bala, firepoint1.position, firepoint1.rotation);
        Instantiate(bala, firepoint2.position, firepoint2.rotation);
    }

    void FixedUpdate()
    {
        if (canMove)
        {
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

            if (isFreeMovement)
            {
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 lookDir = mousePos - rb.position;
                rb.rotation = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
            }
        }
    }

    private void HideCursor()
    {
        Cursor.visible = false;
    }

    public void EnableShooting()
    {
        canShoot = true;
    }

    public void EnableFreeMovement()
    {
        isFreeMovement = true;
        Cursor.visible = true; 
    }
}
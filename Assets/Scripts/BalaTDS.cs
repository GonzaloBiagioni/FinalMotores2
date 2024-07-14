using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalaTDS : MonoBehaviour
{
    public float speed = 5f;
    public int damage = 1;

    private Transform player;
    private Vector2 target;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        if (player != null)
        {
            target = player.position;
            Vector2 direction = (target - (Vector2)transform.position).normalized;
            GetComponent<Rigidbody2D>().velocity = direction * speed;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (Vector2.Distance(transform.position, target) < 0.2f)
        {
            Destroy(gameObject);
        }
    }
}
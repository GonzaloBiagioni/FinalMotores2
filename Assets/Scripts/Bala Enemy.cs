using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalaEnemy : MonoBehaviour
{
    public float velocidad = 10f;
    public float tiempoVida = 1.7f;

    void Start()
    {
        Destroy(gameObject, tiempoVida);
    }

    void FixedUpdate()
    {
        transform.Translate(Vector3.left * velocidad * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}

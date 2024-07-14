using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalaTDS : MonoBehaviour
{
    public float velocidad = 10f;
    public float tiempoVida = 3f;

    void Start()
    {
        Destroy(gameObject, tiempoVida);
    }

    void FixedUpdate()
    {
        transform.Translate(Vector3.up * velocidad * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
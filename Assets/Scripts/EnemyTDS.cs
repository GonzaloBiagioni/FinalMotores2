using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTDS : MonoBehaviour
{
    public float velocidadMovimiento = 3f;
    public float rangoDisparo = 7f;
    public int vida = 3;
    public GameObject balaEnemigo;
    public Transform puntoDeDisparo;
    public GameObject prefabMoneda;
    public GameObject prefabConsumibleVida; 
    public GameObject explosionPrefab;
    public float tiempoEsperaEntreDisparos = 2f;
    public float probabilidadDropVida = 0.2f;

    private float tiempoUltimoDisparo;

    void Start()
    {
        tiempoUltimoDisparo = Time.time;
    }

    void Update()
    {
        MoverHaciaJugador();

        if (Time.time - tiempoUltimoDisparo > tiempoEsperaEntreDisparos)
        {
            Disparar();
            tiempoUltimoDisparo = Time.time;
        }
    }

    void MoverHaciaJugador()
    {
        GameObject jugador = GameObject.FindGameObjectWithTag("Player");

        if (jugador != null)
        {
            Vector3 direccion = jugador.transform.position - transform.position;
            float distanciaAlJugador = direccion.magnitude;

            if (distanciaAlJugador > rangoDisparo)
            {
                transform.Translate(direccion.normalized * velocidadMovimiento * Time.deltaTime, Space.World);
            }

            Quaternion rotacionHaciaJugador = Quaternion.LookRotation(Vector3.forward, direccion);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotacionHaciaJugador, velocidadMovimiento * Time.deltaTime);
        }
    }

    void Disparar()
    {
        GameObject jugador = GameObject.FindGameObjectWithTag("Player");

        if (jugador != null)
        {
            Vector3 direccion = jugador.transform.position - transform.position;
            float distanciaAlJugador = direccion.magnitude;
            if (distanciaAlJugador <= rangoDisparo)
            {
                Vector3 posicionDisparo = puntoDeDisparo != null ? puntoDeDisparo.position : transform.position;
                AudioManager.Instance.PlaySFX(1);
                Instantiate(balaEnemigo, posicionDisparo, transform.rotation);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D otro)
    {
        if (otro.CompareTag("BalaPlayer"))
        {
            vida--;

            if (vida <= 0)
            {
                SoltarObjeto();
                DestruirEnemigo();
            }
            Destroy(otro.gameObject);
        }
    }

    void SoltarObjeto()
    {
        if (Random.value < probabilidadDropVida && prefabConsumibleVida != null)
        {
            Instantiate(prefabConsumibleVida, transform.position, Quaternion.identity);
        }
        else if (prefabMoneda != null)
        {
            Instantiate(prefabMoneda, transform.position, Quaternion.identity);
        }
    }

    void DestruirEnemigo()
    {
        if (explosionPrefab != null)
        {
            AudioManager.Instance.PlaySFX(6);
            Instantiate(explosionPrefab, transform.position, transform.rotation);
        }
        Destroy(gameObject);
    }
}
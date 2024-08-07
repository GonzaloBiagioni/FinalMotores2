using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMina : MonoBehaviour
{
    public float moveSpeed = 0.5f;
    public GameObject prefabMoneda;
    public GameObject prefabConsumibleVida;
    public GameObject explosionPrefab;
    public float tiempoDestruccion = 0f;
    public float probabilidadDropVida = 0.1f;

    private void Start()
    {
        Destroy(gameObject, 22f);
    }

    void Update()
    {
        transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D otro)
    {
        if (otro.CompareTag("Player"))
        {
            JugadorImpactado(otro.GetComponent<MovementPlayer>());
        }
        else if (otro.CompareTag("BalaPlayer"))
        {
            BalaImpactada(otro.gameObject);
        }
    }

    void JugadorImpactado(MovementPlayer jugador)
    {
        DestruirEnemigo();
    }

    void BalaImpactada(GameObject bala)
    {
        Destroy(bala);
        SoltarObjeto();
        DestruirEnemigo();
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
        Destroy(gameObject, tiempoDestruccion);
    }
}
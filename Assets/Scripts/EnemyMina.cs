using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMina : MonoBehaviour
{
    public float moveSpeed = 0.5f;
    public GameObject prefabMoneda;
    public float tiempoDestruccion = 0f;
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
        else if (otro.CompareTag("Bala Player"))
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
        SoltarMonedas();
        DestruirEnemigo();
    }
    void SoltarMonedas()
    {
        if (prefabMoneda != null)
        {
            Instantiate(prefabMoneda, transform.position, Quaternion.identity);
        }
    }
    void DestruirEnemigo()
    {
        AudioManager.Instance.PlaySFX(2);
        Destroy(gameObject, tiempoDestruccion);
    }
}

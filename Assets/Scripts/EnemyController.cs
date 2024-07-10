using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed = 1f;
    public GameObject prefabMoneda;
    public float tiempoDestruccion = 0f;
    public float fireRate = 2f;
    public GameObject bulletPrefab;
    private float nextFireTime = 0f;

    private void Start()
    {
        Destroy(gameObject, 11f);
    }
    void Update()
    {
        transform.Translate(Vector2.up * moveSpeed * Time.deltaTime);
        if (Time.time > nextFireTime)
        {
            //AudioManager.Instance.PlaySFX(1);
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }
    void Shoot()
    {
        Instantiate(bulletPrefab, transform.position, Quaternion.identity);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed = 1f;
    public GameObject prefabMoneda;
    public GameObject explosionPrefab;
    public float tiempoDestruccion = 0f;
    public float fireRate = 2f;
    public GameObject bulletPrefab;
    private float nextFireTime = 0f;
    private Vector2 moveDirection;

    private void Start()
    {
        Destroy(gameObject, 11f);
    }

    void Update()
    {
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);
        if (Time.time > nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    public void SetDirection(Vector2 direction)
    {
        moveDirection = direction.normalized;
    }

    void Shoot()
    {
        AudioManager.Instance.PlaySFX(1);
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
        if (explosionPrefab != null)
        {
            AudioManager.Instance.PlaySFX(6);
            Instantiate(explosionPrefab, transform.position, transform.rotation);
        }
        Destroy(gameObject, tiempoDestruccion);
    }
}
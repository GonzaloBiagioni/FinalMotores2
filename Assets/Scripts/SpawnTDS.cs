using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTDS : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public Transform player;
    public float spawnRange = 10f;
    public float spawnIntervalMin = 2f;
    public float spawnIntervalMax = 6f;
    public int hitsToDestroy = 5;
    public int maxEnemies = 10;
    public GameObject explosionPrefab;
    private float nextSpawnTime;
    private int currentHits = 0;
    private int enemyCount = 0;
    private bool isActive = false;

    void Start()
    {
        SetRandomSpawnTime();
    }

    void Update()
    {
        if (isActive && Vector3.Distance(transform.position, player.position) <= spawnRange && enemyCount < maxEnemies)
        {
            if (Time.time >= nextSpawnTime)
            {
                SpawnEnemy();
                SetRandomSpawnTime();
            }
        }
    }

    void SetRandomSpawnTime()
    {
        nextSpawnTime = Time.time + Random.Range(spawnIntervalMin, spawnIntervalMax);
    }

    void SpawnEnemy()
    {
        int enemyIndex = Random.Range(0, enemyPrefabs.Length);
        GameObject enemy = Instantiate(enemyPrefabs[enemyIndex], transform.position, Quaternion.identity);
        Vector3 enemyPosition = enemy.transform.position;
        enemyPosition.z = enemyPrefabs[enemyIndex].transform.position.z;
        enemy.transform.position = enemyPosition;

        enemyCount++;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("BalaPlayer"))
        {
            TakeDamage();
            Destroy(other.gameObject);
        }
    }

    void TakeDamage()
    {
        currentHits++;
        AudioManager.Instance.PlaySFX(5);

        if (currentHits >= hitsToDestroy)
        {
            DestroySpawner();
        }
    }

    void DestroySpawner()
    {
        if (explosionPrefab != null)
        {
            Instantiate(explosionPrefab, transform.position, transform.rotation);
        }

        AudioManager.Instance.PlaySFX(6); // Reproduce el sonido de destrucción
        Destroy(gameObject);
    }

    public void ActivateSpawner()
    {
        isActive = true;
    }
}
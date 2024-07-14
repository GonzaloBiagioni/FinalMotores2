using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public Transform player;
    public float spawnRange = 10f;
    public float minSpawnInterval = 2f;
    public float maxSpawnInterval = 6f;
    public float minInitialDelay = 0f;
    public float maxInitialDelay = 3f; 
    private bool isSpawning = false;
    public Vector2 spawnDirection;

    void Update()
    {
        if (Vector3.Distance(transform.position, player.position) <= spawnRange)
        {
            if (!isSpawning)
            {
                StartCoroutine(SpawnEnemies());
            }
        }
        else
        {
            if (isSpawning)
            {
                StopAllCoroutines();
                isSpawning = false;
            }
        }
    }

    IEnumerator SpawnEnemies()
    {
        isSpawning = true;

        float initialDelay = Random.Range(minInitialDelay, maxInitialDelay);
        yield return new WaitForSeconds(initialDelay);

        while (true)
        {
            int enemyIndex = Random.Range(0, enemyPrefabs.Length);
            GameObject enemy = Instantiate(enemyPrefabs[enemyIndex], transform.position, Quaternion.identity);
            EnemyController enemyMovement = enemy.GetComponent<EnemyController>();
            if (enemyMovement != null)
            {
                enemyMovement.SetDirection(spawnDirection);
            }

            float angle = Mathf.Atan2(spawnDirection.y, spawnDirection.x) * Mathf.Rad2Deg - 90;
            enemy.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

            float spawnInterval = Random.Range(minSpawnInterval, maxSpawnInterval);
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
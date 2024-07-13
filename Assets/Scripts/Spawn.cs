using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform player;
    public float spawnRange = 10f;
    public float spawnInterval = 5f;
    private bool isSpawning = false;

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
                StopCoroutine(SpawnEnemies());
                isSpawning = false;
            }
        }
    }

    IEnumerator SpawnEnemies()
    {
        isSpawning = true;
        while (true)
        {
            Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
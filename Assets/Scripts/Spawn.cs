using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject[] enemies;
    public Transform[] spawnPoints;
    private float spawnTime;

    void Start()
    {
        SetRandomSpawnTime();
        Invoke("SpawnEnemy", spawnTime);
    }

    void SetRandomSpawnTime()
    {
        spawnTime = Random.Range(0f, 3f);
    }

    void SpawnEnemy()
    {
        int enemyIndex = Random.Range(0, enemies.Length);
        int spawnPointIndex = Random.Range(0, spawnPoints.Length);
        Instantiate(enemies[enemyIndex], spawnPoints[spawnPointIndex].position, Quaternion.Euler(0, 0, 180));
        SetRandomSpawnTime();
        Invoke("SpawnEnemy", spawnTime);
    }
}
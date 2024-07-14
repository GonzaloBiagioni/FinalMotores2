using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerActivationTDS : MonoBehaviour
{
    public GameObject[] spawners;
    public GameObject puerta;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (GameObject spawner in spawners)
            {
                spawner.GetComponent<SpawnTDS>().ActivateSpawner();
            }
            if (puerta != null)
            {
                puerta.SetActive(true);
            }
            gameObject.SetActive(false); 
        }
    }
}
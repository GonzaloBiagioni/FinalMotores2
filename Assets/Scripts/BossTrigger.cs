using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrigger : MonoBehaviour
{
    public GameObject finalBoss; 

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ActivateFinalBoss();
        }
    }

    private void ActivateFinalBoss()
    {
        if (finalBoss != null)
        {
            finalBoss.SetActive(true);
            finalBoss.GetComponent<FinalBossTDS>().ActivateBoss();
            gameObject.SetActive(false); 
        }
    }
}
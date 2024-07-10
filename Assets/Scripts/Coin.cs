using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public int value;
    void OnTriggerEnter2D(Collider2D Coin)
    {
        if (Coin.CompareTag("Player"))
        {
            //AudioManager.Instance.PlaySFX(3);
            Destroy(gameObject);
            CoinCount.Instance.IncreaseCoin(value);
        }
    }
}

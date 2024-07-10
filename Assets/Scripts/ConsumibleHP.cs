using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumibleHP : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            bool Vidarecuperada = CanvasManager.Instance.RecuperarHP();

            if (Vidarecuperada)
            {
                AudioManager.Instance.PlaySFX(3);
                Destroy(this.gameObject);
            }
        }
    }
}

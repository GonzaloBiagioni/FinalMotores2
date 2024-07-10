using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasHP : MonoBehaviour
{
    public GameObject[] hp;

    public void DesactivarHP(int indice)
    {
        hp[indice].SetActive(false);
    }
    public void ActivarHP(int indice)
    {
        hp[indice].SetActive(true);
    }
}

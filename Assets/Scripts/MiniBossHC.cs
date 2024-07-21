using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MiniBossHC : MonoBehaviour
{
    public Image healthImage;
    private MiniJefe miniBoss;

    private void Start()
    {
        miniBoss = FindObjectOfType<MiniJefe>();
    }

    private void Update()
    {
        if (miniBoss != null)
        {
            float healthRatio = (float)miniBoss.currentHealth / miniBoss.maxHealth;
            healthImage.fillAmount = healthRatio;
        }
    }
}
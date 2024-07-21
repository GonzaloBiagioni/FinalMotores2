using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalBossHC : MonoBehaviour
{
    public Image healthImage;
    private FinalBossTDS finalBoss;

    private void Start()
    {
        finalBoss = FindObjectOfType<FinalBossTDS>();
    }

    private void Update()
    {
        if (finalBoss != null)
        {
            float healthRatio = (float)finalBoss.currentHealth / finalBoss.maxHealth;
            healthImage.fillAmount = healthRatio;
        }
    }
}
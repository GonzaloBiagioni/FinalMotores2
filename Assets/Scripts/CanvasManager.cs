using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager Instance { get; private set; }

    private int hp = 3;

    public CanvasHP canvas;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    public void PerderHP()
    {
        hp -= 1;
        AudioManager.Instance.PlaySFX(5);
        if (hp == 0)
        {           
            SceneManager.LoadScene(3);
        }

        canvas.DesactivarHP(hp);
    }
    public bool RecuperarHP()
    {
        if (hp == 3)
        {
            return false;
        }

        canvas.ActivarHP(hp);
        hp += 1;
        return true;
    }

}

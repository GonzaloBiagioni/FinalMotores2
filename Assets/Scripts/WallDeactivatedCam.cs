using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallDeactivatedCam : MonoBehaviour
{
    public GameObject wall1;
    public GameObject wall2;
    public Transform player;
    public Transform deactivationPoint; 
    private bool wallsDeactivated = false;

    void Update()
    {
        if (!wallsDeactivated && player.position.x > deactivationPoint.position.x)
        {
            wall1.SetActive(false);
            wall2.SetActive(false);
            wallsDeactivated = true;
        }
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionZone : MonoBehaviour
{
    public MovementPlayer playerController;
    public ScrollCamara cameraController;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerController.EnableFreeMovement();
            playerController.EnableShooting();
            cameraController.StartFollowingPlayer();
        }
    }
}
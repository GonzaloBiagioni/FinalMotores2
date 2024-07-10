using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollCamara : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 2f;
    public float stopPositionX;

    private bool shouldMove = true;
    private bool followPlayer = false;

    void Update()
    {
        if (shouldMove)
        {
            Vector3 newPosition = new Vector3(transform.position.x + moveSpeed * Time.deltaTime, transform.position.y, transform.position.z);
            transform.position = newPosition;

            if (transform.position.x >= stopPositionX)
            {
                shouldMove = false;
            }
        }

        if (followPlayer)
        {
            Vector3 targetPosition = new Vector3(player.position.x, player.position.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
    }

    public void StopMoving()
    {
        shouldMove = false;
    }

    public void StartFollowingPlayer()
    {
        followPlayer = true;
    }
}
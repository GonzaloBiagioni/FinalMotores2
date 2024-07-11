using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollCamara : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 2f; 
    public float stopXPosition = 90f; 
    public Vector3 offset; 
    private bool followPlayer = false; 

    void Start()
    {
        transform.position = player.position + offset;
    }

    void Update()
    {
        if (transform.position.x < stopXPosition)
        {
            transform.position += new Vector3(moveSpeed * Time.deltaTime, 0, 0);
        }
        else if (followPlayer)
        {
           
            Vector3 targetPosition = player.position + offset;
            transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
    }

    public void StartFollowingPlayer()
    {
        followPlayer = true;
    }
}
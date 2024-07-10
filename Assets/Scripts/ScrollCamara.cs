using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollCamara : MonoBehaviour
{
    public Transform target;
    public float moveSpeed = 2f;
    public float stopPositionX;

    void Update()
    {
        if (transform.position.x < stopPositionX)
        {
            Vector3 newPosition = new Vector3(target.position.x, transform.position.y, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, newPosition, moveSpeed * Time.deltaTime);
        }
    }
}
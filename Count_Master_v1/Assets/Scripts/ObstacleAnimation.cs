using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleAnimation : MonoBehaviour
{
    [Header("Y Axis Movement")]
    public bool yMovement;

    public float yMin;
    public float yMax;
    public float animSpeed;

    public bool moveDown;
    public bool moveUp;

    private Vector3 movement;

    private void Update()
    {
        movement = gameObject.transform.position;

        if (yMovement)
        {
            if (movement.y >= yMax)
            {
                moveDown = true;
                moveUp = false;
            }

            if (movement.y <= yMin)
            {
                moveUp = true;
                moveDown = false;
            }

            if (moveUp)
            {
                movement.y += animSpeed * Time.deltaTime;
            }
            else
            {
                movement.y -= animSpeed * Time.deltaTime;
            }
        }
        gameObject.transform.position = movement;
    }
}

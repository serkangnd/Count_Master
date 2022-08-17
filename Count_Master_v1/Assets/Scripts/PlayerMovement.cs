using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float movementSpeed;
    public float controlSpeed;
    bool isTouching = false;
    float touchPosX;

    void Update()
    {
        GetInput();
        Move();

    }

    void Move()
    {
        //Get animation
        transform.Translate(Vector3.forward * Time.deltaTime * movementSpeed);

        if (isTouching)
        {
           // We are taking our touch position to a variable and add to multiplied with control speed
           touchPosX += Input.GetAxis("Mouse X") * controlSpeed * Time.fixedDeltaTime;
        }
        // We are changing our position via new touchPosX - We don't want to any changes on y or z axis
        // so they will take our local position.
        transform.position = new Vector3(touchPosX, transform.position.y, transform.position.z);
    }

    //Testing Inputs
    void GetInput()
    {
        if (Input.GetMouseButton(0))
        {
            isTouching = true;
        }
        else
        {
            isTouching = false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [Header(" Components ")]
    [SerializeField] private float movementSpeed;
    [SerializeField] private float controlSpeed;
    [SerializeField] bool isTouching = false;
    [SerializeField] float touchPosX;
    [SerializeField] float xRange; //keep player in bounds
    public GameManager.GameState gameState;

    void Update()
    {
        gameState = GameManager.instance.state;
        if(gameState == GameManager.GameState.Playing || gameState == GameManager.GameState.MainMenu)
        {
            GetInput();
            GetInputMobile();
        }
        Move();
        KeepBounds();
    }

    void Move()
    {
        if (gameState == GameManager.GameState.Playing)
        {
            //Get animation
            transform.Translate(Vector3.forward * Time.deltaTime * movementSpeed);
        }

        if (isTouching && gameState == GameManager.GameState.Playing)
        {
            // We are taking our touch position to a variable and add to multiplied with control speed
            touchPosX += Input.GetAxis("Mouse X") * controlSpeed * Time.fixedDeltaTime;
        }
        // We are changing our position via new touchPosX - We don't want to any changes on y or z axis
        // so they will take our local position.
        transform.position = new Vector3(touchPosX, transform.position.y, transform.position.z);
    }

    //Keep in bounds
    void KeepBounds()
    {
        if(transform.position.x < -xRange)
        {
            transform.position = new Vector3(-xRange, transform.position.y, transform.position.z);
        }
        if (transform.position.x > xRange)
        {
            transform.position = new Vector3(xRange, transform.position.y, transform.position.z);
        }
    }

    //Testing Inputs
    void GetInput()
    {
        if (Input.GetMouseButton(0))
        {
            isTouching = true;
            GameManager.instance.UpdateGameState(GameManager.GameState.Playing);
        }
        else
        {
            isTouching = false;
        }
    }

    //Mobile Inputs
    void GetInputMobile()
    {
        if (Input.touchCount > 0)
        {
            isTouching = true;
            GameManager.instance.UpdateGameState(GameManager.GameState.Playing);
        }
        else
        {
            isTouching = false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


using System;

public class Runner : MonoBehaviour
{
    [Header(" Components ")]
    [SerializeField] private Animator animator;
    [SerializeField] private Collider collider;
    [SerializeField] private Renderer renderer;
    public static int currentIndex;

    PlayerMovement playerState;

    //[SerializeField] public Canvas gameOverCanvas; 
    private bool targeted;

    [Header(" Detection ")]
    [SerializeField] private LayerMask obstaclesLayer;
    [SerializeField] private LayerMask finishLayer;
    [SerializeField] private GameObject explodeParticle;
    

    // Start is called before the first frame update
    void Start()
    {
        currentIndex = SceneManager.GetActiveScene().buildIndex;
    }

    // Update is called once per frame
    void Update()
    {
        if (!collider.enabled)
            return;

        DetectObstacles();
        DetectFinish();
    }



    private void DetectObstacles()
    {
        if (Physics.OverlapSphere(transform.position, 0.1f, obstaclesLayer).Length > 0)
            Explode();

    }

    private void DetectFinish()
    {
        if (Physics.OverlapSphere(transform.position, 0.1f, finishLayer).Length > 0)
        {
            GameManager.instance.UpdateGameState(GameManager.GameState.Win);
        }
    }

    public void SetAsTarget()
    {
        targeted = true;
    }

    public bool IsTargeted()
    {
        return targeted;
    }

    public void Explode()
    {
        Debug.Log("explode");
        collider.enabled = false;
        renderer.enabled = false;

        Instantiate(explodeParticle, transform.position, Quaternion.identity);

        if (transform.parent != null && transform.parent.childCount <= 1)
        {
            //Change here with canvas death
            SceneManager.LoadScene("RookieUI");
            GameManager.instance.UpdateGameState(GameManager.GameState.Lost);
        }

        transform.parent = null;
        Destroy(gameObject);
    }
}

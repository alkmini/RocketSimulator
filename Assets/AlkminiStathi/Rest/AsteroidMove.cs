using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AsteroidMove : MonoBehaviour
{
    GameObject Vessel;
    public AsteroidManager ast;
    Vector3 moveDirection = Vector3.zero;
    bool gameOver = false;
   
    private void Start()
    {
        Vessel = GameObject.Find("Vessel");
        moveDirection = (Vector3.right - Vector3.up);
    }

    private void Update()
    {
        AsteroidMovement();
        GameStateCheck();
    }

    void AsteroidMovement()
    {
        transform.position += moveDirection * (5f * Time.deltaTime);
    }

    void GameStateCheck()
    {
        float asteroidCollider = 3;

        float dist = Vector3.Distance(Vessel.transform.position, transform.position);

        if(dist < asteroidCollider && !gameOver)
        {
            ast.SendGameOver();
            gameOver = true;
            
            moveDirection = Vector3.Cross(moveDirection, Vector3.forward);
        }
        
    }
}

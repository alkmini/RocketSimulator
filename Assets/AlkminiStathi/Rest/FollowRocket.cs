using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowRocket : MonoBehaviour
{
    //camera moves member variables
    private GameObject rocket;
    private Vector3 camPos;
    private float newPos;
    public float distFromRocket;

    private void Start()
    {
        rocket = GameObject.Find("Vessel");

    }
    private void Update()
    {
        CalcDist();
        transform.position = camPos;
    }

    void CalcDist()
    {
        newPos = rocket.transform.position.z - distFromRocket;
        camPos = new Vector3(rocket.transform.position.x, rocket.transform.position.y, newPos);
    }
}


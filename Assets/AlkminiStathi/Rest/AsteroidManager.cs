using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AsteroidManager : MonoBehaviour
{
    GameObject Vessel;
    [SerializeField] private GameObject Asteroid;
    public Text gameOver;
    private float PosY = 10;
    float timeLeft = 0;


    private void Start()
    {
        Vessel = GameObject.Find("Vessel");
        timeLeft = 1;
        
        gameOver.enabled = false;
    }
    private void Update()
    {
        PosY = Random.Range(-10f, 10f);
        TimeToSpawn();
        
    }

    void TimeToSpawn()
    {
        
        timeLeft -= Time.deltaTime;
        
        if (timeLeft < 0)
        {
            InstantiateAsteroid();
            timeLeft = 1;
        }
    }

    void InstantiateAsteroid()
    {
        Vector3 posOffset = new Vector3(Vessel.transform.position.x - 5, Vessel.transform.position.y + PosY, Vessel.transform.position.z);
        if (Vessel.transform.position.y > 5f)
        {
            GameObject newAsteroid = Instantiate(Asteroid, posOffset, Quaternion.identity);
            newAsteroid.gameObject.GetComponent<AsteroidMove>().ast = this;
        }
    }

    public void SendGameOver()
    {
        gameOver.enabled = true;
    }
}

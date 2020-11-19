using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeterCounter : MonoBehaviour
{
    GameObject Vessel;
    Vector3 dist;
    float startPoint;
    float currentPoint;
    [SerializeField] private Text txt;
    private void Start()
    {
        Vessel = GameObject.Find("Vessel");
        txt = gameObject.GetComponent<Text>();
        startPoint = Vessel.transform.position.y;
    }

    private void Update()
    {
        currentPoint = Vessel.transform.position.y;
        int dist = Mathf.RoundToInt(currentPoint - startPoint);
        txt.text = dist.ToString() + " km";
    }
}

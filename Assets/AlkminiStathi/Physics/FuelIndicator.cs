using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuelIndicator : MonoBehaviour
{
    public Slider slider;
   
    public void SetMaxFuel(float fuel)
    {
        slider.maxValue = fuel;
        slider.value = fuel;
    }

    public void SetFuel(float fuel)
    {
        slider.value = fuel;
    }
}

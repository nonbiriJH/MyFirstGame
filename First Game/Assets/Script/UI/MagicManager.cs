using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagicManager : MonoBehaviour
{
    public Slider slider;
    public floatValue magicValue;

    private void Start()
    {
        //Assign max value to slider
        slider.maxValue = magicValue.maxValue;
        //For change between scenes.
        slider.value = magicValue.runtimeValue;
    }

    //Update the slider UI
    public void UpdateSlider()
    {
        if (slider.maxValue < magicValue.runtimeValue)
        {
            slider.value = magicValue.maxValue;
        }
        else
        {
            slider.value = magicValue.runtimeValue;
        }
     }
}

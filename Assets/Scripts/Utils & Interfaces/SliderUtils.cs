using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class SliderUtils
{
    public static void SmoothAnimation(Slider slider, float speed, int targetValue)
    {
        slider.value = Mathf.Lerp(slider.value, targetValue, Time.deltaTime * speed);
    }
    public static void ChangeColorByValue(Slider slider, Color color, float desiredValue)
    {
        if(slider.value <= desiredValue)
        {
            slider.fillRect.GetComponent<Image>().color = color;
        }
        else
        {
            slider.fillRect.GetComponent<Image>().color = new Color(1, 1, 1);
        }
    }
    public static void SetupSlider(Slider slider, float value,float maxValue)
    {
        slider.value = value;
        slider.maxValue = maxValue;
    }
}

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
}

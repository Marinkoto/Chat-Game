using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public static class TimeController
{
    public static void StartTime()
    {
        Time.timeScale = 1.0f;
    }
    public static void StopTime()
    {
        Time.timeScale = 0.0f;
    }
}

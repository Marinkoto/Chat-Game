using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSystem
{
    public static void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }
    public static int GetCurrentScene()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }
}

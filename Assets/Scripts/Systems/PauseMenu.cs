using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] GameObject pauseMenu;
    public static event Action OnPause;
    public static event Action OnUnpause;
    public static bool isPaused;

    void Start()
    {
        isPaused = false;
        pauseMenu.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
                OnUnpause?.Invoke();
            }
            else
            {
                PauseGame();
                OnPause?.Invoke();
            }
        }
    }
    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        TimeController.StopTime();
        isPaused = true;
    }
    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        TimeController.StartTime();
        isPaused = false;
    }
    public void QuitGame()
    {
        SceneSystem.ExitGame();
    }
    public void LoadMenu()
    {
        SceneSystem.LoadScene(SceneSystem.GetCurrentScene() - 1);
    }
}

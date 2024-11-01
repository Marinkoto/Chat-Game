using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] GameObject pauseMenu;
    public bool isPaused;

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
            }
            else
            {
                PauseGame();
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

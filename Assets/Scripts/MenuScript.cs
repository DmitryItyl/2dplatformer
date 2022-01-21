using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;
    public GameObject scoreText;
    public GameObject pauseButton;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        scoreText.SetActive(false);
        pauseButton.SetActive(false);

        Time.timeScale = 0f;
        GameIsPaused = true;

    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        scoreText.SetActive(true);
        pauseButton.SetActive(true);

        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Resume();
    }

    public void Quit()
    {
        Application.Quit();
    }
}

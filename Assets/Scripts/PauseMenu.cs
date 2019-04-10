using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;

    [SerializeField]
    private GameObject pauseMenuPanel;
    [SerializeField]
    private GameObject statsPanel;
    [SerializeField]
    private GameObject darkPanel;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    private void Pause()
    {
        pauseMenuPanel.SetActive(true);
        darkPanel.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;        
    }

    public void Resume()
    {
        pauseMenuPanel.SetActive(false);
        statsPanel.SetActive(false);
        darkPanel.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    public void EndGame()
    {
        Application.Quit();
    }
}

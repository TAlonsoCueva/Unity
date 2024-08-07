using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class botones : MonoBehaviour
{
    public GameObject CanvasPause;
    public int levels = 1;
    public int currentLevel = 0;
    private void Awake()
    {
        levels = 5;
    }
    public void LoadNextLevel() 
    {
        Time.timeScale = 1;
        currentLevel++;
        if (currentLevel > levels)
            currentLevel = 1;
        SceneManager.LoadScene(currentLevel);
    }
    public void LoadMenuScene()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene("Level" + currentLevel);
        currentLevel = 1;
        Time.timeScale = 1;
    }

    public void ReStartGame()
    {
        CanvasPause.SetActive(false);
        Time.timeScale = 1;
    }

    public void Pause()
    {
        CanvasPause.SetActive(true);
        Time.timeScale = 0;
    }
    public void Exit()
    {
        Application.Quit();
    }
}

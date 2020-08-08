using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private void OnEnable()
    {
        Player.restartLevel += RestartLevel;
        Goal.loadLevel += LoadNextLevel;
    }

    private void OnDisable()
    {
        Player.restartLevel -= RestartLevel;
        Goal.loadLevel -= LoadNextLevel;
    }

    public void LoadNextLevel(GameObject g)
    {
        Torch.firstTorch = true;
        EscapeMenu.isPaused = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void RestartLevel()
    {
        Torch.firstTorch = true;
        EscapeMenu.isPaused = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

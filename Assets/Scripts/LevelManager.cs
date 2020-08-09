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
        StartCoroutine(AsyncLoadNextLevel());
    }

    IEnumerator AsyncLoadNextLevel()
    {
        AsyncOperation loadNextLevel = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        while (!loadNextLevel.isDone)
        {
            yield return null;
        }
    }

    public void RestartLevel()
    {
        //DontDestroyOnLoad(Camera.main);
        Torch.firstTorch = true;
        EscapeMenu.isPaused = false;
        StartCoroutine(AsyncRestartLevel());
    }

    IEnumerator AsyncRestartLevel()
    {
        AsyncOperation restartLevel = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        while (!restartLevel.isDone)
        {
            yield return null;
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

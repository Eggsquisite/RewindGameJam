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

    private void ResetVariables(bool newLevel)
    {
        if (newLevel)
            Player.checkpointReached = false;

        Torch.firstTorch = true;
        EscapeMenu.isPaused = false;
        EndTrigger.backtrackBegin = false;
    }

    public void LoadNextLevel(GameObject g)
    {
        ResetVariables(true);
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
        ResetVariables(false);
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

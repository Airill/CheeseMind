using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeScript : MonoBehaviour
{
    //Кнопки

    public void MainMenuButton()
    {
        SceneManager.LoadScene(0);
      //  Time.timeScale = 1;
    }

    public void RetryButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Scores.RestartScores();
      //  Time.timeScale = 1;
    }

    public void NextButton()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}

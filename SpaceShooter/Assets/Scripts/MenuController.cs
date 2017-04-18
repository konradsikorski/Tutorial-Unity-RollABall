using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {
    public CanvasGroup GameScreen;
    public CanvasGroup MenuScreen;

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void StartGame()
    {
        GameObject.FindGameObjectWithTag("Player").SetActive(true);
        GameScreen.alpha = 1;
        MenuScreen.alpha = 0;
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}

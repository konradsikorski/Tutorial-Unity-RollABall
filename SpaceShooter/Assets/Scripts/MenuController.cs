﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {
    public CanvasGroup OptionsMenu;

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void RestartGame()
    {
        GameController.Instance.RestartGame();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Continue()
    {
        GameController.Instance.IsPaused = false;
    }

    public void Options()
    {
        this.GetComponent<CanvasGroup>().alpha = 0;
        this.gameObject.SetActive(false);

        OptionsMenu.gameObject.SetActive(true);
        OptionsMenu.alpha = 1;
    }

    public void ExitGame()
    {
        // save any game data here
#if UNITY_EDITOR
        // Application.Quit() does not work in the editor so
        // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }
}

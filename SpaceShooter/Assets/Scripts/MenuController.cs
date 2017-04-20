using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {
    public CanvasGroup GameScreen;
    public CanvasGroup MenuScreen;

    private Animator playerAnimator;

    private void Awake()
    {
        Time.timeScale = 0;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void StartGame()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        player.SetActive(true);
        playerAnimator = player.GetComponent<Animator>();
        playerAnimator.SetBool("GameStarting", true);

        StartCoroutine(GameStarting());

        GameScreen.alpha = 1;
        MenuScreen.alpha = 0;

        Time.timeScale = 1;
    }

    private IEnumerator GameStarting()
    {
        yield return new WaitForSeconds(1.5f);
        playerAnimator.enabled = false;
        UIController.Instance.GameReady = true;
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

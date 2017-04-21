using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController Instance;
    public Text GameEndScoreText;
    public CanvasGroup GameOverScreen;
    public CanvasGroup GameScreen;

    private Animator playerAnimator;

    private void Awake()
    {
        Instance = this;
    }

    private void OnDestroy()
    {
        Instance = null;
    }

    private void Start()
    {
        StartGame();
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
    }

    private IEnumerator GameStarting()
    {
        yield return new WaitForSeconds(1.5f);
        playerAnimator.enabled = false;
        UIController.Instance.GameReady = true;
    }

    public void GameOver()
    {
        GameEndScoreText.text = "Your Score: " + UIController.Instance.Points;
        GameScreen.alpha = 0;
        GameOverScreen.alpha = 1;
    }
}

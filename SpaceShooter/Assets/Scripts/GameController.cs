using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    public bool IsActive { get; private set; }

    private bool _isPaused;
    public bool IsPaused
    {
        get { return _isPaused; }
        set
        {
            if (value == _isPaused) return;
            if (!IsActive) return;

            _isPaused = value;

            Time.timeScale = System.Convert.ToInt32(!_isPaused);
            UIController.Instance.ShowPauseScreen(_isPaused);
        }
    }

    private Animator playerAnimator;

    private void Awake()
    {
        Instance = this;
        this.IsActive = false;
        Time.timeScale = 1;
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
        IsActive = true;
    }

    private void Update()
    {
        if( Input.GetKeyDown(KeyCode.Escape) ) IsPaused = !IsPaused;
    }

    public void GameOver()
    {
        IsActive = false;
        UIController.Instance.ShowGameOverScreen();
    }
}

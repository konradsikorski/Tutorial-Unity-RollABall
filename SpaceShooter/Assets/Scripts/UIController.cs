using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {
    public static UIController Instance;

    public Text PointsText;
    public Text PlayerHealthText;
    public Text GameEndScoreText;
    public Slider PlayerHealthSlider;

    public CanvasGroup GameScreen;
    public CanvasGroup GameOverScreen;

    private int _points;
    public int Points
    {
        get { return _points; }
        set {
            _points = value;
            PointsText.text = value.ToString();
        }
    }

    private float _playerHealth;
    public float PlayerHealth
    {
        get { return _playerHealth; }
        set {
            _playerHealth = value;
            PlayerHealthText.text = value.ToString();
            PlayerHealthSlider.value = value;
        }
    }

    private void Awake()
    {
        Instance = this;
    }

    private void OnDestroy()
    {
        Instance = null;
    }

    internal void ShowGameOverScreen()
    {
        GameEndScoreText.text = UIController.Instance.Points.ToString();
        GameScreen.GetComponent<Animator>().enabled = false;
        GameScreen.alpha = 0;
        GameOverScreen.alpha = 1;
    }
}

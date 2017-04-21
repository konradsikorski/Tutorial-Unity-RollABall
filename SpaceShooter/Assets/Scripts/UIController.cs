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

    public bool GameReady { get; set; }

    public CanvasGroup GameScreen;
    public CanvasGroup GameEndScreen;
    
    private int _points;
    public int Points
    {
        get { return _points; }
        set {
            _points = value;
            PointsText.text = "Score: " + value;
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
}

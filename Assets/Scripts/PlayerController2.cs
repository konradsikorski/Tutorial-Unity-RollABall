using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController2 : MonoBehaviour {
    private Rigidbody Rb;
    public float Speed;

    public CanvasGroup GameCanva;
    public Text CoinsCount;
    public CanvasGroup WinCanva;
    public Text Score;
    public CanvasGroup LooseCanva;

    private int _allCoinsCount;
    private int _colletedCoins;
    private int CollectedCoins
    {
        get { return _colletedCoins; }
        set
        {
            _colletedCoins = value;
            CoinsCount.text = "Coins: " + CollectedCoins + "/" + _allCoinsCount;
        }
    }

    private bool _isEnd;
    public bool IsEnd
    {
        get
        {
            return _isEnd;
        }

        set
        {
            _isEnd = value;
            if (_isEnd) GameCanva.alpha = 0;
        }
    }

    // Use this for initialization
    void Start ()
    {
        Rb = GetComponent<Rigidbody>();
        WinCanva.alpha = 0;
        LooseCanva.alpha = 0;
        _allCoinsCount = GameObject.FindGameObjectsWithTag("Pick Up").Length;
        CollectedCoins = 0;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void FixedUpdate()
    {
        if (transform.position.y < 0)
        {
            Loose();
            this.gameObject.SetActive(false);
            return;
        }

        var moveHorizontal = Input.GetAxis("Horizontal");
        var moveVertical = Input.GetAxis("Vertical");

        var movement = new Vector3(0, 0, moveHorizontal);
        Rb.AddForce(movement * Speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
            CollectedCoins++;
        }
        else if(other.gameObject.CompareTag("Finish"))
        {
            Win();
        }
    }

    private void Win()
    {
        IsEnd = true;
        WinCanva.alpha = 1;
        Score.text = "Score: " + CollectedCoins.ToString();
    }

    private void Loose()
    {
        IsEnd = true;
        LooseCanva.alpha = 1;
    }
}

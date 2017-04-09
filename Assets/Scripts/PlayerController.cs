using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
    private Rigidbody rb;
    public Text countText;
    public Text winText;
    public float speed;

    private int _allCoinsCount;
    private int _colletedCoins;
    private int CollectedCoins
    {
        get { return _colletedCoins; }
        set
        {
            _colletedCoins = value;
            countText.text = "Count: " + CollectedCoins;

            if(_colletedCoins == _allCoinsCount)
                winText.text = "THE END";
        }
    }

    //called at the first frame and we can do some initialization stuff
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        CollectedCoins = 0;
        winText.text = string.Empty;
        _allCoinsCount = GameObject.FindGameObjectsWithTag("Pick Up").Length;
    }

    // perform before frame calculation
    private void Update()
    {
        
    }

    // perform before phisics calculation
    private void FixedUpdate()
    {
        if (Input.GetKeyDown("space"))
        {
            rb.velocity = Vector3.zero;
        }

        var moveHorizontal = Input.GetAxis("Horizontal");
        var moveVertical = Input.GetAxis("Vertical");

        var movement = new Vector3(moveHorizontal, 0, moveVertical);
        rb.AddForce(movement * speed);
    }

    // fired when first tuch the triger collider
    void OnTriggerEnter(Collider other)
    {
        if( other.gameObject.CompareTag("Pick Up") )
        {
            other.gameObject.SetActive(false);
            CollectedCoins++;
        }
    }
}

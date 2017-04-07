using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    private Rigidbody rb;

    //called at the first frame and we can do some initialization stuff
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // perform before frame calculation
    private void Update()
    {
        
    }

    // perform before phisics calculation
    private void FixedUpdate()
    {
        var moveHorizontal = Input.GetAxis("Horizontal");
        var moveVertical = Input.GetAxis("Vertical");

        var movement = new Vector3(moveHorizontal, 0, moveVertical);
        rb.AddForce(movement);
    }
}

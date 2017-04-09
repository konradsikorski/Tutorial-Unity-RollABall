using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController2 : MonoBehaviour
{
    public GameObject player;
    public Vector3 startOffset;

    // Use this for initialization
    void Start ()
    {
        startOffset = transform.position - player.transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        transform.position = player.transform.position + startOffset;
    }
}

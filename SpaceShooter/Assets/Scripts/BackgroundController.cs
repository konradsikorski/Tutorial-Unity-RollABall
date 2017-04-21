using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour {

    public GameObject[] Backgrounds;
    public float Speed;

	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        foreach (var background in Backgrounds)
        {
            var z = background.transform.position.z + 1 * -Speed;
            if (z < -background.transform.localScale.y) z += background.transform.localScale.y * 2;

            background.transform.position = new Vector3(
                background.transform.position.x,
                background.transform.position.y,
                z
                );

            //background.transform.position.Set(
            //    background.transform.position.x,
            //    background.transform.position.y, 
            //    background.transform.position.z + 1 * Speed);
        }
	}
}

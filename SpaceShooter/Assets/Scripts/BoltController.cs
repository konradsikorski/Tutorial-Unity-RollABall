using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoltController : MonoBehaviour {
    public float Speed;
    public float Demage;
    public float zMax;
    private Rigidbody boltBody;
    public bool IsDestroyed;

	// Use this for initialization
	void Start () {
        boltBody = GetComponent<Rigidbody>();
        boltBody.velocity = transform.forward * Speed;
	}

    private void Update()
    {
        // replaced by GameBoundaryController
        //if (transform.position.z >= zMax)
        //    Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
    public float Speed;

    private Rigidbody enemyBody;
    
	// Use this for initialization
	void Start () {
        enemyBody = GetComponent<Rigidbody>();
        enemyBody.velocity = transform.forward * Speed;
	}
}

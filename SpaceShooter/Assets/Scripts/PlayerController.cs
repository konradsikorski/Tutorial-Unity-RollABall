using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Boundry
{
    public float xMin, xMax;
    public float zMin, zMax;
}

public class PlayerController : MonoBehaviour {
    public float Speed;
    public float Tilt;
    public Boundry Boundary;
    public float StartHealth;
    public GameObject Explosion;
    public AudioSource DestroyClip;

    private float _health;
    public float Health
    {
        get { return _health; }
        set
        {
            _health = value;

            if (_health <= 0) Death();

            UIController.Instance.PlayerHealth = _health;
        }
    }

    private void Death()
    {
        _health = 0;
        Destroy(gameObject);

        var explosion = Instantiate(Explosion, transform.position, transform.rotation);
        explosion.GetComponent<AudioSource>().Play();
        DestroyObject(explosion, 2);

        GameController.Instance.GameOver();
    }

    private Rigidbody playerBody;
    private Vector3 movement;

	// Use this for initialization
	void Start () {
        playerBody = GetComponent<Rigidbody>();
        Health = StartHealth;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");

        if (h == 0 && v == 0)
        {
            h = Input.GetAxis("Mouse X");
            v = Input.GetAxis("Mouse Y");
        }

        if (transform.position.z > Boundary.zMax && v > 0) v = 0;
        if (transform.position.z < Boundary.zMin && v < 0) v = 0;

        if (transform.position.x > Boundary.xMax && h > 0) h = 0;
        if (transform.position.x < Boundary.xMin && h < 0) h = 0;

        movement.Set(h, 0, v);
        playerBody.velocity = movement * Speed;
        playerBody.rotation = Quaternion.Euler(0,0, playerBody.velocity.x * -Tilt);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BoltEnemy")) HitBolt(other.gameObject);
    }

    private void HitBolt(GameObject bolt)
    {
        var boltController = bolt.GetComponent<BoltController>();
        if (boltController.IsDestroyed) return;

        boltController.IsDestroyed = true;
        Health -= boltController.Demage;

        Destroy(bolt);
    }
}

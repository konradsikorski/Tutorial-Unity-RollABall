using System;
using UnityEngine;

public class AsteroidController : MonoBehaviour {
    public float Speed;
    public float Tumble;
    public float Demage;
    public GameObject Explosin;
    public AudioSource DestroyClip;
    public int Score;

    private float _size;
    public float Size
    {
        get { return _size; }
        set
        {
            _size = value;
            Health = (float)Math.Floor(value * 3f);
            Demage = (float)Math.Floor(_size) * 10f;
            Score = (int)Math.Floor(value * 3f) * 10;
        }
    }

    private float _health;
    public float Health
    {
        get { return _health; }
        set
        {
            _health = value;
            if (_health <= 0) DestroyAsteroid();
        }
    }

    private Rigidbody asteroidBody;

	// Use this for initialization
	void Start () {
        asteroidBody = GetComponent<Rigidbody>();
        asteroidBody.velocity = -Vector3.forward * Speed;

        asteroidBody.angularVelocity = UnityEngine.Random.insideUnitSphere * Tumble;
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) HitPlayer(other.gameObject);
        else if (other.CompareTag("Bolt")) HitBolt(other.gameObject);
    }

    private void HitBolt(GameObject bolt)
    {
        var boltController = bolt.GetComponent<BoltController>();
        if (boltController.IsDestroyed) return;

        boltController.IsDestroyed = true;
        Health -= boltController.Demage;

        Destroy(bolt);
    }

    private void HitPlayer(GameObject player)
    {
        var playerController = player.GetComponent<PlayerController>();
        playerController.Health -= Demage;
        DestroyAsteroid();
    }

    private void DestroyAsteroid()
    {
        Destroy(gameObject);
        UIController.Instance.Points += Score;
        var explosion = Instantiate(Explosin, transform.position, transform.rotation);
        explosion.GetComponent<AudioSource>().Play();
        Destroy(explosion, 2);
    }
}

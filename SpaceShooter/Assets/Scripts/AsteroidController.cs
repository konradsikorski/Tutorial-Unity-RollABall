using System;
using UnityEngine;
using UnityEngine.UI;

public class AsteroidController : MonoBehaviour, IHealth {
    public float Speed;
    public float Tumble;
    public float Demage;
    public GameObject Explosin;
    public int Score;

    private float _size;
    public float Size
    {
        get { return _size; }
        set
        {
            _size = value;
            var size = (float)Math.Round(_size);
            InitialHealth = size * 4f;
            Health = InitialHealth;
            Demage = size * 10f;
            Score = (int)size * 10;
        }
    }

    private float _health;
    public float Health
    {
        get { return _health; }
        set
        {
            if (_health == value) return;
            _health = value;

            if (HealthChanged != null) HealthChanged(this, System.EventArgs.Empty);
            if (_health <= 0) DestroyAsteroid();
        }
    }

    public float InitialHealth { get; set; }

    private Rigidbody asteroidBody;

    public event EventHandler HealthChanged;

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
        Instantiate(Explosin, transform.position, transform.rotation);
    }
}

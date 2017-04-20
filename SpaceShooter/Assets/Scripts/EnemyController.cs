using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
    public float Speed;
    public float Tilt;
    public Boundry Boundary;
    public float StartHealth;
    public GameObject Explosion;
    public AudioSource DestroyClip;
    public int Score;
    public float Demage;

    private Rigidbody enemyBody;

    private float _health;
    public float Health
    {
        get { return _health; }
        set
        {
            _health = value;
            if (_health <= 0) Death();
        }
    }

    private void Death()
    {
        Destroy(gameObject);
        UIController.Instance.Points += Score;
        var explosion = Instantiate(Explosion, transform.position, transform.rotation);
        explosion.GetComponent<AudioSource>().Play();
        Destroy(explosion, 2);
    }

    // Use this for initialization
    void Start () {
        enemyBody = GetComponent<Rigidbody>();
        enemyBody.velocity = -transform.forward * Speed;
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
        Death();
    }
}

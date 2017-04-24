using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
    public float Speed;
    public float Tilt;
    public Boundry Boundary;
    public float StartHealth;
    public GameObject Explosion;
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
        Instantiate(Explosion, transform.position, transform.rotation);        
    }

    // Use this for initialization
    void Start () {
        enemyBody = GetComponent<Rigidbody>();
        enemyBody.velocity = -transform.forward * Speed;

        StartCoroutine(Evade());
    }

    float evade;
    public Range evadeDelay = new Range(0, 1.5f);
    public Range evadeBoundries = new Range(-6, 6);
    public Range evadeDuration = new Range(0.5f, 2);
    public Range evadeSleep = new Range(0.5f, 2);

    private IEnumerator Evade()
    {
        yield return new WaitForSeconds(Random.Range(evadeDelay.From, evadeDelay.To));

        while(GameController.Instance.IsActive)
        {
            evade = Random.Range(evadeBoundries.From, evadeBoundries.To);
            yield return new WaitForSeconds(Random.Range(evadeDuration.From, evadeDuration.To));
            evade = 0;
            yield return new WaitForSeconds(Random.Range(evadeSleep.From, evadeSleep.To));
        }
    }

    private void FixedUpdate()
    {
        var move = Mathf.MoveTowards(enemyBody.velocity.x, evade, Time.deltaTime * Speed);
        enemyBody.velocity = new Vector3(move, 0, -Speed);
        enemyBody.rotation = Quaternion.Euler(0, 0, enemyBody.velocity.x * -Tilt);
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

[System.Serializable]
public class Range
{
    public float From;
    public float To;

    public Range() { }
    public Range(float from, float to)
    {
        From = from;
        To = to;
    }
}

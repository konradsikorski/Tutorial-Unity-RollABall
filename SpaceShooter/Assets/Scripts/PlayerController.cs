using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public Image HitEffectImage;

    private float _health;
    public float Health
    {
        get { return _health; }
        set
        {
            if (value < _health)
                StartCoroutine(HitEffect());

            _health = value;
            UIController.Instance.PlayerHealth = _health;

            if (_health <= 0) Death();
        }
    }

    private Color hitEffectColor = new Color32(131, 44, 44, 80);
    private float hitEffectSpeed = 0.1f;

    private IEnumerator HitEffect()
    {
        HitEffectImage.color = hitEffectColor;
        
        do
        {
            yield return new WaitForSeconds(hitEffectSpeed);
            HitEffectImage.color = Color.Lerp(HitEffectImage.color, Color.clear, hitEffectSpeed);
        }
        while (HitEffectImage.color.a >= 0.001f);

        HitEffectImage.color = Color.clear;
    }

    private void Death()
    {
        _health = 0;
        Destroy(gameObject);

        var explosion = Instantiate(Explosion, transform.position, transform.rotation);
        explosion.GetComponent<AudioSource>().Play();
        DestroyObject(explosion, 2);

        HitEffectImage.color = Color.clear;
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

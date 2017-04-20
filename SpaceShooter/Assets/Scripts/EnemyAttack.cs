using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float FireSpeed;
    public GameObject Bolt;
    public AudioSource WeaponClip;

    private Transform shootSpown;
    private float nextFireTime;

    // Use this for initialization
    void Start () {
        shootSpown = transform.FindChild("ShootSpown");
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Time.time > nextFireTime)
        {
            nextFireTime = Time.time + FireSpeed;
            Fire();
        }
    }

    private void Fire()
    {
        Instantiate(Bolt, shootSpown.position, shootSpown.rotation);
        WeaponClip.Play();
    }
}

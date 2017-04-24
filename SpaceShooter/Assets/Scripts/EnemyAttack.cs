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
        InvokeRepeating("Fire", 0.5f, FireSpeed);
    }

    private void Fire()
    {
        Instantiate(Bolt, shootSpown.position, shootSpown.rotation);
        WeaponClip.Play();
    }
}

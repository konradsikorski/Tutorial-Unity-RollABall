using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {
    public bool IsAutoFire;
    public float FireSpeed;
    public GameObject Bolt;

    private Transform shootSpown;
    //private float fireDeltaTime;
    private float nextFireTime;

	// Use this for initialization
	void Start () {
        shootSpown = transform.FindChild("ShootSpown");
	}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
            IsAutoFire = !IsAutoFire;

        // first apouch
        //if (IsFireing() && fireDeltaTime >= FireSpeed)
        //    Fire();
        //else
        //    fireDeltaTime += Time.deltaTime;

        // seccond aprouch
        if (IsFireing() && Time.time > nextFireTime)
        {
            nextFireTime = Time.time + FireSpeed;
            Fire();
        }
    }

    private bool IsFireing()
    {
        return IsAutoFire || Input.GetButton("Fire1");
    }

    private void Fire()
    {
        Instantiate(Bolt, shootSpown.position, shootSpown.rotation);
    }
}

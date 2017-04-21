using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour {
    public float Speed;
    private Vector3 _startPosition;

    private void Start()
    {
        _startPosition = transform.position;
    }

    // Update is called once per frame
    void Update () {
        var move = Mathf.Repeat(Time.time * Speed, transform.localScale.y);
        transform.position = _startPosition + Vector3.forward * move;
    }
}

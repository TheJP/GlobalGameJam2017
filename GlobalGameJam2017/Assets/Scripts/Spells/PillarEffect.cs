﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarEffect : MonoBehaviour
{

    float strength = 0f;
    bool rising = false;
    bool lowering = false;
    bool hold = false;
    float radius = 0f;
    Vector3 pos = Vector3.zero;

    public float sleep = 1.7f;
    public float motion = 0.3f;
    public float wait = 0.4f;
    private float factor = 0.4f;

    // Use this for initialization
    void Start()
    {

    }

    private void LateUpdate()
    {
        if (hold)
        {
            transform.position = new Vector3(pos.x, transform.position.y, pos.z);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        if (rising)
        {
            transform.Translate(Vector3.up * strength * factor);
        }
        if (lowering)
        {
            transform.Translate(Vector3.down * strength * factor);
        }

    }

    private void StartRising()
    {
        transform.localScale = new Vector3(radius, 1, radius);
        rising = true;
        hold = true;
        Invoke("StopRising", motion);
    }

    private void StopRising()
    {
        rising = false;
        Invoke("StartLowering", wait);
    }

    private void StartLowering()
    {
        lowering = true;
        Invoke("StopLowering", motion);
    }
    private void StopLowering()
    {
        lowering = false;
        hold = false;
        Reset();
    }

    public void StartEffect(Vector3 pos, float radius, float strength)
    {
        this.radius = radius;
        this.pos = pos;
        this.strength = strength;
        Invoke("StartRising", sleep);
    }
    private void Reset()
    {
        transform.localPosition = Vector3.zero;
        transform.localScale = new Vector3(1e-5f, 1, 1e-5f);
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform menuLocation;
    public Transform gameLocation;

    private Transform targetLocation;

    public Camera skyboxCamera;

    private void Start()
    {
        SwitchToMenu();
    }

    private void FixedUpdate()
    {
        skyboxCamera.transform.Rotate(Vector3.up, 2f / 60f);
        transform.position = Vector3.Lerp(transform.position, targetLocation.position, 0.03f);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetLocation.rotation, 0.03f);
    }

    public void SwitchToGame() { targetLocation = gameLocation; }
    public void SwitchToMenu() { targetLocation = menuLocation; }
}

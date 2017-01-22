using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform menuLocation;
    public Transform gameLocation;

    public float animationSpeed = 0.03f;
    public float skyboxSpeed = 2f / 60f;

    private Transform targetLocation;

    public Camera skyboxCamera;

    private void Start()
    {
        SwitchToMenu();
    }

    private void FixedUpdate()
    {
        skyboxCamera.transform.Rotate(Vector3.up, skyboxSpeed);
        transform.position = Vector3.Lerp(transform.position, targetLocation.position, animationSpeed);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetLocation.rotation, animationSpeed);
    }

    public void SwitchToGame() { targetLocation = gameLocation; }
    public void SwitchToMenu() { targetLocation = menuLocation; }
}

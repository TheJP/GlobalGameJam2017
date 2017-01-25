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

    [Tooltip("Add empty GameObject, that will contain all focused objects here")]
    public Transform groupOfFocusedObjects;

    private Transform targetLocation;
    private bool inGame = false;

    public Camera skyboxCamera;

    private void Start() { SwitchToMenu(); }

    private void FixedUpdate()
    {
        skyboxCamera.transform.Rotate(Vector3.up, skyboxSpeed);

        transform.position = Vector3.Lerp(transform.position, TargetPosition(), animationSpeed);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetLocation.rotation, animationSpeed);
    }

    private Vector3 TargetPosition()
    {
        if (!inGame) { return targetLocation.position; }

        //Find center of all focused objects
        var focusTarget = Vector3.zero;
        var count = groupOfFocusedObjects.childCount;
        for (int i = count - 1; i >= 0; --i)
        {
            focusTarget += groupOfFocusedObjects.GetChild(i).position;
        }
        if (count > 0) { focusTarget /= count; }

        //Move camera gradually to center
        var direction = transform.TransformDirection(Vector3.forward);
        var factor = (focusTarget.y - targetLocation.position.y) / direction.y;
        if (float.IsInfinity(factor)) { return targetLocation.position; }

        //Position, where the camera intersects the viewed plane
        var camera = targetLocation.position + factor * direction;
        var target = targetLocation.position + (focusTarget - camera);

        return new Vector3(target.x, targetLocation.position.y, target.z);
    }

    public void SwitchToGame()
    {
        targetLocation = gameLocation;
        inGame = true;
    }

    public void SwitchToMenu()
    {
        targetLocation = menuLocation;
        inGame = false;
    }
}

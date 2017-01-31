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
    private float zoomFactor = 0.0f;

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

        //Find maximal distance of a focus object to found center
        var maxDistances = Vector2.zero;
        for (int i = count - 1; i >= 0; --i)
        {
            var distance = Camera.main.WorldToViewportPoint(groupOfFocusedObjects.GetChild(i).position);
            maxDistances.x = Mathf.Max(maxDistances.x, Mathf.Abs(distance.x - 0.5f)); //(0.5f, 0.5f) is the camera position
            maxDistances.y = Mathf.Max(maxDistances.y, Mathf.Abs(distance.y - 0.5f));
        }

        //Move camera gradually to center
        var direction = transform.TransformDirection(Vector3.forward);
        var factor = (focusTarget.y - targetLocation.position.y) / direction.y;
        if (float.IsInfinity(factor)) { return targetLocation.position; }

        //Position, where the camera intersects the viewed plane
        var camera = targetLocation.position + factor * direction;
        var target = targetLocation.position + (focusTarget - camera);
        target.y = targetLocation.position.y;

        //Zoom in or out, so that: 1. All golems are visible 2. The camera is as near as possible
        //3. The camera does not jitter back and forth too much
        zoomFactor += maxDistances.x > 0.35f || maxDistances.y > 0.35f ? -0.01f : (
            maxDistances.x < 0.30f && maxDistances.y < 0.30f ? 0.01f : 0.0f);
        zoomFactor = Mathf.Clamp(zoomFactor, 0.0f, 0.7f);
        var zoomDirection = (focusTarget - target) * zoomFactor;
        target += zoomDirection;

        return target;
    }

    public void SwitchToGame()
    {
        targetLocation = gameLocation;
        zoomFactor = 0.0f;
        inGame = true;
    }

    public void SwitchToMenu()
    {
        targetLocation = menuLocation;
        inGame = false;
    }
}

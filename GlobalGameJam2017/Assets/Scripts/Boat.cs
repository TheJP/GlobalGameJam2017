using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat : MonoBehaviour
{

    public Transform boatRegion;
    public float speed = 20.0f;
    public float turnSpeed = 0.01f;
    public WaterGen water;

    private Vector3 _target;

    void Start()
    {
        _target = RandomPointOnPlane();
    }

    void FixedUpdate()
    {
        var rigidbody = GetComponent<Rigidbody>();
        var direction = (_target - transform.position).normalized;
        var force = transform.TransformDirection(Vector3.forward) * speed;
        force.y = 0.0f;
        rigidbody.AddForce(force, ForceMode.Force);

        if (Vector3.Distance(_target, transform.position) < rigidbody.velocity.magnitude)
        {
            _target = RandomPointOnPlane();
        }

        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direction), turnSpeed);
        var waterPosition = water.transform.InverseTransformPoint(transform.position);
        transform.position = new Vector3(transform.position.x, boatRegion.position.y + Mathf.Sin(Time.time * water.speed + waterPosition.x + waterPosition.z) * 10f + 2f, transform.position.z);
        //Debug.DrawLine(transform.position, _target, Color.red);
    }

    private Vector3 RandomPointOnPlane()
    {
        return boatRegion.TransformPoint(new Vector3(Random.Range(-0.5f, 0.5f), 0.0f, Random.Range(-0.5f, 0.5f)));
    }
}

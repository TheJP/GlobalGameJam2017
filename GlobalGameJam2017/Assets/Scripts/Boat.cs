using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat : MonoBehaviour
{

    public Transform boatRegion;
    public float speed = 20.0f;
    public float turnSpeed = 0.01f;

    private Vector3 _target;

    void Start()
    {
        _target = RandomPointOnPlane();
    }

    void FixedUpdate()
    {
        var rigidbody = GetComponent<Rigidbody>();
        var direction = (_target - transform.position).normalized;
        rigidbody.AddForce(transform.TransformDirection(Vector3.forward) * speed, ForceMode.Force);

        if (Vector3.Distance(_target, transform.position) < rigidbody.velocity.magnitude)
        {
            _target = RandomPointOnPlane();
        }

        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direction), turnSpeed);
        //Debug.DrawLine(transform.position, _target, Color.red);
    }

    private Vector3 RandomPointOnPlane()
    {
        return boatRegion.TransformPoint(new Vector3(Random.Range(-0.5f, 0.5f), 0.0f, Random.Range(-0.5f, 0.5f)));
    }
}

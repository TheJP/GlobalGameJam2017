using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Rigidbody))]
public class Player : Entity {

    public float speed = 0.1f;

    protected override void Start () {
        base.Start();
        base.EntityFaction = Faction.PC;
	}

    protected override void FixedUpdate () {
        base.FixedUpdate();
        Move();        
    }

    private void Move()
    {
        Vector3 v = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
        {
            v += (Vector3.forward);
        }
        if (Input.GetKey(KeyCode.S))
        {
            v += (Vector3.back);
        }
        if (Input.GetKey(KeyCode.A))
        {
            v += (Vector3.left);
        }
        if (Input.GetKey(KeyCode.D))
        {
            v += (Vector3.right);
        }
        //transform.Translate(v.normalized * speed);
        GetComponent<NavMeshAgent>().Move(v.normalized * speed);
    }

}

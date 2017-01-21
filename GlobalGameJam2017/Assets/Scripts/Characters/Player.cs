using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Rigidbody))]
public class Player : Entity {

    public string playerName = "A";
    public float speed = 0.1f;

    protected override void Start () {
        base.Start();
        base.EntityFaction = Faction.PC;
	}

    protected override void FixedUpdate () {
        base.FixedUpdate();
        if (Health > 0.0f) { Move(); }
    }

    private void Move()
    {
        Vector3 v = new Vector3(Input.GetAxisRaw(playerName + "_Horizontal"), 0.0f, Input.GetAxisRaw(playerName + "_Vertical"));
        GetComponent<NavMeshAgent>().Move(v.normalized * speed);
    }

}

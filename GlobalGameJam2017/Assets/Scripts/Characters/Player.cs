using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Assertions;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Rigidbody))]
public class Player : Entity {

    public string playerName = "A";
    public float speed = 0.1f;
    public Animator animator;

    private const string SlamTrigger = "Slam";

    protected override void Start () {
        base.Start();
        base.EntityFaction = Faction.PC;
        Assert.IsNotNull(animator, "Need golem animator in Player");
	}

    protected override void FixedUpdate () {
        base.FixedUpdate();
        if (Health > 0.0f)
        {
            Move();
            if (Input.GetButtonDown(playerName + "_a")) { animator.SetTrigger(SlamTrigger); }
        }
    }

    private void Move()
    {
        Vector3 v = new Vector3(Input.GetAxisRaw(playerName + "_Horizontal"), 0.0f, Input.GetAxisRaw(playerName + "_Vertical")).normalized;
        var isWalking = v.sqrMagnitude > 0.5f;
        animator.SetBool("Walking", isWalking);
        if (isWalking) { transform.rotation = Quaternion.LookRotation(v); }
        GetComponent<NavMeshAgent>().Move(v * speed);
    }

}

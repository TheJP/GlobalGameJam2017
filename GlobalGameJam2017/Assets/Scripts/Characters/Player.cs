using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Assertions;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Rigidbody))]
public class Player : Entity {

    private const string SlamTrigger = "Slam";

    public string playerName = "A";
    public Animator animator;

    public Spell Spell { get; set; }

    protected override void Start () {
        base.Start();
        base.EntityFaction = Faction.PC;
        Assert.IsNotNull(animator, "Need golem animator in Player");
	}

    protected override void FixedUpdate () {
        base.FixedUpdate();
    }

    private void Update()
    {
        if(Health > 0.0f)
        {
            Move();
            if (Input.GetButtonDown(playerName + "_a") && Spell != null) { Spell.StartChanneling(); GetComponentInChildren<AudioPlay>().Channeling(); }
            if (Input.GetButtonUp(playerName + "_a"))
            {
                if (Spell != null && Spell.Cast()) { animator.SetTrigger(SlamTrigger); GetComponentInChildren<AudioPlay>().Stop(); }
            }
        }
    }

    private void Move()
    {
        Vector3 v = new Vector3(Input.GetAxisRaw(playerName + "_Horizontal"), 0.0f, Input.GetAxisRaw(playerName + "_Vertical")).normalized;
        var isWalking = v.sqrMagnitude > 0.5f;
        animator.SetBool("Walking", isWalking);
        if (isWalking) { transform.rotation = Quaternion.LookRotation(v); }
        var agent = GetComponent<NavMeshAgent>();
        agent.Move(v * agent.speed);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Assertions;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Rigidbody))]
public class Player : Entity {

    private const string SlamAnimation = "Slam";
    private const string WalkingAnimation = "Walking";
    private const string DamagedAnimation = "Damaged";

    public string playerName = "A";
    public Animator animator;
    [Tooltip("Chance that the player does not get stunned by an enemy (higher = better for the player")]
    public float bashEvasionChance = 0.9f;
    public GameObject golem_t;

    public Spell Spell { get; set; }

    protected override void Start () {
        base.Start();
        base.EntityFaction = Faction.PC;
        Assert.IsNotNull(animator, "Need golem animator in Player");
        foreach (Collider collider in golem_t.GetComponentsInChildren<Collider>())
        {
            collider.enabled = false;
        }
	}

    protected override void FixedUpdate () {
        base.FixedUpdate();
    }

    private void Update()
    {
        if (Health <= 0.0f) { return; }

        Move();
        if (Input.GetButtonDown(playerName + "_a") && Spell != null && Spell.StartChanneling())
        {
            GetComponentInChildren<AudioPlay>().Channeling();
        }
        if (Input.GetButtonUp(playerName + "_a"))
        {
            if (Spell != null && Spell.Cast()) { animator.SetTrigger(SlamAnimation); GetComponentInChildren<AudioPlay>().Stop(); }
        }
    }

    private void Move()
    {
        Vector3 v = new Vector3(Input.GetAxisRaw(playerName + "_Horizontal"), 0.0f, Input.GetAxisRaw(playerName + "_Vertical")).normalized;
        v = Quaternion.Euler(0f, Camera.main.transform.rotation.eulerAngles.y, 0f) * v;
        var isWalking = v.sqrMagnitude > 0.5f;

        //Rotation
        if (isWalking && !Spell.IsCasting) { transform.rotation = Quaternion.LookRotation(v); }

        //Movement
        if (!Spell.IsChanneling && !Spell.IsCasting)
        {
            animator.SetBool(WalkingAnimation, isWalking);
            var agent = GetComponent<NavMeshAgent>();
            agent.Move(v * agent.speed);
        }
    }

    protected override void OnDamageTaken(float damage)
    {
        if (Random.Range(0.0f, 1.0f) < bashEvasionChance) { return; } //Evaded
        if (Spell.IsChanneling) { Spell.CancelChanneling(); }
        animator.SetTrigger(DamagedAnimation);
    }

    protected override void Kill()
    {
        foreach (Collider collider in golem_t.GetComponentsInChildren<Collider>())
        {
            collider.enabled = true;
        }
        transform.GetComponentInChildren<Animator>().enabled = false;
        Destroy(gameObject, 5f);
    }
}

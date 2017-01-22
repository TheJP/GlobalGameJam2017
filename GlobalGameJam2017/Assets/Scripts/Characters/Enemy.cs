using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;
using UnityEngine.Assertions;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Rigidbody))]
public class Enemy : Entity
{

    public float attackRange;
    public int attackSpeed;
    [Tooltip("Physical force which the enemy can use to walk")]
    public float walkingForce = 1.0f;
    public float fallingForce = 100.0f;
    public float shockwaveForce = 4000.0f;
    public Animator animator;

    private const float turnRate = 0.1f;

    private Player agro = null;
    private bool attacking = false;
    private int tick = 0;

    protected override void Start()
    {
        base.Start();
        base.EntityFaction = Faction.NPC;
        var agent = GetComponent<NavMeshAgent>();
        agent.updatePosition = false;
        agent.updateRotation = false;
        Assert.IsNotNull(animator, "Need furby / dwarvf animator in Enemy");
        animator.SetBool("Walking", true);
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        var agent = GetComponent<NavMeshAgent>();
        agent.nextPosition = transform.position;

        //Navigation
        if (agro == null || agro.Health <= 0.0f) { GoToNearestPlayer(); }
        else { agent.SetDestination(agro.transform.position); }

        var direction = agent.desiredVelocity.normalized;

        //Handle rotation
        if (direction.sqrMagnitude > 0.5f)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direction), turnRate);
        }

        //Handle walking force
        var force = direction * walkingForce;
        force += new Vector3(Random.Range(0f, 100f), 0f, Random.Range(0f, 100f)).normalized * Random.Range(0.1f, 0.3f) * walkingForce;
        GetComponent<Rigidbody>().AddForce(new Vector3(force.x, -fallingForce, force.z));

        //Attacking
        attacking = agro != null && Vector3.Distance(transform.position, agro.transform.position) < attackRange;
        if (attacking) { Attack(); }
        else { tick = 0; }
    }

    private void GoToNearestPlayer()
    {
        //Search nearest player on nav mesh
        NavMeshPath path;
        NavMeshPath bestPath = null;
        float bestRemainingDistance = float.PositiveInfinity;
        Player nearestPlayer = null;
        var agent = GetComponent<NavMeshAgent>();
        foreach (var player in FindObjectsOfType<Player>().Where(p => p.Health > 0.0f))
        {
            path = new NavMeshPath();
            if (!NavMesh.CalculatePath(transform.position, player.transform.position, -1, path)) { continue; }
            agent.SetPath(path);
            if (agent.remainingDistance < bestRemainingDistance)
            {
                bestPath = path;
                bestRemainingDistance = agent.remainingDistance;
                nearestPlayer = player;
            }
        }

        //Go to nearest player
        if (bestPath != null)
        {
            agent.SetPath(bestPath);
            agro = nearestPlayer;
        }
    }

    private void Attack()
    {
        if (tick == attackSpeed)
        {
            agro.DoDamage(this.Damage);
            animator.SetTrigger("Attack");
            tick = 0;
        }
        tick++;
    }

    private void OnParticleCollision(GameObject other)
    {
        //Applies knock back from the shockwave ability
        GetComponent<Rigidbody>().AddForce((transform.position - other.transform.position).normalized * shockwaveForce);
    }
}

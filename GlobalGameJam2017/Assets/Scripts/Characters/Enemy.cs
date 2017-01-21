using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Rigidbody))]
public class Enemy : Entity
{

    public int attackRange;
    public int attackSpeed;
    public GameObject playersGroup;
    private Player agro = null;
    private bool attacking = false;
    private int tick = 0;

    protected override void Start()
    {
        base.Start();
        base.EntityFaction = Faction.NPC;
	}

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if (agro == null || agro.Health <= 0.0f) { AttackNearestPlayer(); }
        else { GetComponent<NavMeshAgent>().SetDestination(agro.transform.position); }
        if (agro != null && Vector3.Distance(transform.position, agro.transform.position) < attackRange)
        {
            attacking = true;
        } else
        {
            attacking = false;
            tick = 0;
        }
        if(attacking)
        {
            Attack();
        }
    }

    private void AttackNearestPlayer()
    {
        //Search nearest player on nav mesh
        NavMeshPath path;
        NavMeshPath bestPath = null;
        float bestRemainingDistance = float.PositiveInfinity;
        Player nearestPlayer = null;
        var agent = GetComponent<NavMeshAgent>();
        foreach (var player in playersGroup.GetComponentsInChildren<Player>().Where(p => p.Health > 0.0f))
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

        //Attack nearest player
        if (bestPath != null)
        {
            agent.SetPath(bestPath);
            agro = nearestPlayer;
        }
    }

    private void Attack()
    {
        if(tick == attackSpeed)
        {
            agro.DoDamage(this.Damage);
            tick = 0;
        }
        tick++;
    }

}

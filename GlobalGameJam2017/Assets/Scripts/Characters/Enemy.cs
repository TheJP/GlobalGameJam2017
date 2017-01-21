using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class Enemy : Entity
{

    public GameObject playersGroup;
    private Player agro = null;

    void Start()
    {
        Health = 100;
    }

    void FixedUpdate()
    {
        if(agro == null || agro.Health <= 0.0f) { AttackNearestPlayer(); }
        else { GetComponent<NavMeshAgent>().SetDestination(agro.transform.position); }
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
            if (!NavMesh.CalculatePath(transform.position, player.transform.position, 0, path)) { continue; }
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

}

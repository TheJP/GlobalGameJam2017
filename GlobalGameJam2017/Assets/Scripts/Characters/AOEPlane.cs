using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class AOEPlane : Entity {

    public int attackSpeed = 20;

    private Dictionary<Entity, int> inRange = new Dictionary<Entity, int>();

    // Use this for initialization
    protected override void Start () {
        base.Start();
        base.EntityFaction = Faction.Neutral;
	}

    // Update is called once per frame
    protected override void FixedUpdate () {
        base.FixedUpdate();
        foreach(Entity e in inRange.Keys.ToList())
        {
            int i = inRange[e];
            if(inRange[e] == 0)
            {
                if (e.DoDamage(this.Damage))
                {
                    inRange.Remove(e);
                    continue;
                }
            }
            i++;
            if(i >= attackSpeed)
            {
                i = 0;
            }
            inRange[e] = i;
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        Entity entity = other.GetComponent<Entity>();
        if(entity != null)
        {
            inRange.Add(entity, 0);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Entity entity = other.GetComponent<Entity>();
        if (entity != null)
        {
            inRange.Remove(entity);
        }
    }
}

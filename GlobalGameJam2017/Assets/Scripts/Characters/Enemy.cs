using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity {

	// Use this for initialization
	protected override void Start () {
        base.Start();
        base.faction = Faction.NPC;
	}

    // Update is called once per frame
    protected override void FixedUpdate () {
        base.FixedUpdate();
    }

}

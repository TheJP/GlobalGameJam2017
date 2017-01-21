using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOEPlane : Entity {


	// Use this for initialization
	protected override void Start () {
        base.Start();
        base.EntityFaction = Faction.Neutral;
        base.Immunity = true;
	}

    // Update is called once per frame
    protected override void FixedUpdate () {
        base.FixedUpdate();
	}
}

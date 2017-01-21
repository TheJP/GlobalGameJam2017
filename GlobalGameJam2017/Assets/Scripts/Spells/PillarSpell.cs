using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarSpell : Spell {

    public Transform spellIndicator;
    public float minRadius = 3;
    public float maxRadius = 6;

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if (IsChanneling)
        {
            var distance = ((Time.time - ChannelingStartTime) / maxChannelingTime) * maxCastRange;
            var radius = ((Time.time - ChannelingStartTime) / maxChannelingTime) * (maxRadius - minRadius) + minRadius;
            spellIndicator.localPosition = new Vector3(0f, 0f, distance);
            spellIndicator.localScale = new Vector3(radius, spellIndicator.localScale.y, radius);
        }
        else
        {
            spellIndicator.localPosition = Vector3.zero;
            spellIndicator.localScale = new Vector3(1e-5f, spellIndicator.localScale.y, 1e-5f);
        }
    }

    protected override bool Cast(float channelingTime)
    {
        

        return true;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarSpell : Spell {

    public Transform spellIndicator;
    public Transform maxRangeIndicator;
    public GameObject effect;
    public float minRadius = 3;
    public float maxRadius = 6;

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if (IsChanneling)
        {
            var factor = Math.Min(((Time.time - ChannelingStartTime) / maxChannelingTime), 1.0f);
            var distance = factor * maxCastRange;
            var radius = factor * (maxRadius - minRadius) + minRadius;
            spellIndicator.localPosition = new Vector3(0f, 0f, distance);
            spellIndicator.localScale = new Vector3(radius, spellIndicator.localScale.y, radius);
            maxRangeIndicator.localScale = new Vector3(maxRadius, maxRangeIndicator.localScale.y, maxRadius);
            maxRangeIndicator.localPosition = new Vector3(0f, 0f, maxCastRange);
        }
        else
        {
            spellIndicator.localPosition = Vector3.zero;
            spellIndicator.localScale = new Vector3(1e-5f, spellIndicator.localScale.y, 1e-5f);
            maxRangeIndicator.localScale = new Vector3(1e-5f, maxRangeIndicator.localScale.y, 1e-5f);

        }
    }

    protected override bool Cast(float channelingTime)
    {
        var factor = Math.Min(((Time.time - ChannelingStartTime) / maxChannelingTime), 1.0f);
        var radius = factor * (maxRadius - minRadius) + minRadius;
        effect.GetComponentInChildren<PillarEffect>().StartEffect(spellIndicator.position, radius, channelingTime / maxChannelingTime);
        return true;
    }
}

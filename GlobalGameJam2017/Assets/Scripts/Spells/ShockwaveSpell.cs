using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class ShockwaveSpell : Spell
{
    public Transform spellIndicator;
    public Transform spellMaxRangeIndicator;
    public float force;

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if (IsChanneling)
        {
            var radius = Mathf.Min((Time.time - ChannelingStartTime) / maxChannelingTime, 1.0f) * maxCastRange;
            spellIndicator.localScale = new Vector3(radius * 2, spellIndicator.localScale.y, radius * 2);
            spellMaxRangeIndicator.localScale = new Vector3(maxCastRange * 2, spellMaxRangeIndicator.localScale.y, maxCastRange * 2);
        }
        else
        {
            spellIndicator.localScale = new Vector3(1e-5f, spellIndicator.localScale.y, 1e-5f);
            spellMaxRangeIndicator.localScale = new Vector3(1e-5f, spellMaxRangeIndicator.localScale.y, 1e-5f);
        }
    }

    protected override bool Cast(float channelingTime)
    {

        return true;
    }
}

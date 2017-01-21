using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class ShockwaveSpell : Spell
{
    public Transform spellIndicator;

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if (IsChanneling)
        {
            var radius = ((Time.time - ChannelingStartTime) / maxChannelingTime) * maxCastRange;
            spellIndicator.localScale = new Vector3(radius * 2, spellIndicator.localScale.y, radius * 2);
        }
        else
        {
            spellIndicator.localScale = new Vector3(1e-5f, spellIndicator.localScale.y, 1e-5f);
        }
    }
}

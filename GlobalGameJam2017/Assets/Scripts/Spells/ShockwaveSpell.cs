using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class ShockwaveSpell : Spell
{
    public Transform spellIndicator;
    public Transform spellMaxRangeIndicator;
    public GameObject dustParticles;
    public float force = 1.0f;

    private const float WaitBevoreDust = 1.3f;
    private const float WaitBevoreResetDust = 2.1f;
    private const float DustVelocity = 10.0f;

    private Vector3? dustParticlesPosition = null;

    protected override float CastingDuration
    {
        get { return WaitBevoreDust; }
    }

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

    private void LateUpdate()
    {
        if(dustParticlesPosition.HasValue) { dustParticles.transform.position = dustParticlesPosition.Value; }
    }

    protected override bool Cast(float channelingTime)
    {
        var scale = (channelingTime / maxChannelingTime) * maxCastRange / DustVelocity;
        var main = dustParticles.GetComponent<ParticleSystem>().main;
        main.startLifetime = scale;
        Invoke("ShowDust", WaitBevoreDust);
        return true;
    }

    private void ShowDust()
    {
        dustParticlesPosition = dustParticles.transform.position;
        dustParticles.SetActive(true);
        Invoke("ResetDust", WaitBevoreResetDust);
        transform.parent.GetComponentInChildren<AudioPlay>().ImpactWind();
    }

    private void ResetDust()
    {
        dustParticles.SetActive(false);
        dustParticles.GetComponent<ParticleSystem>().Clear();
        dustParticles.transform.localPosition = new Vector3(0.0f, dustParticles.transform.localPosition.y, 0.0f);
        dustParticlesPosition = null;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spell : MonoBehaviour
{
    public float maxChannelingTime = 5.0f;
    public float maxCastRange = 15.0f;
    public float channelingTimeBonus = 0.5f;

    private float lastCastTime = -100f;

    protected virtual float CastingDuration
    {
        get { return 1.3f; }
    }

    protected float ChannelingStartTime { get; private set; }

    public bool IsChanneling
    {
        get { return !float.IsNaN(ChannelingStartTime); }
    }

    public bool IsCasting
    {
        get { return Time.time <= lastCastTime + CastingDuration; }
    }

    protected virtual void Awake()
    {
        ChannelingStartTime = float.NaN;
        //Attach spell to player if possible
        var player = transform.parent.GetComponent<Player>();
        if (player != null) { player.Spell = this; }
    }

    protected virtual void Start() { }

    protected virtual void FixedUpdate()
    {
        if (Time.time > ChannelingStartTime + maxChannelingTime + channelingTimeBonus) { ChannelingStartTime = float.NaN; }
    }

    public bool StartChanneling()
    {
        if (IsChanneling || IsCasting) { return false; }
        ChannelingStartTime = Time.time;
        return true;
    }

    public bool Cast()
    {
        var success = IsChanneling && Cast(Mathf.Min(Time.time - ChannelingStartTime, maxChannelingTime));
        if (success) { lastCastTime = Time.time; }
        ChannelingStartTime = float.NaN;
        return success;
    }

    protected abstract bool Cast(float channelingTime);

    public void CancelChanneling()
    {
        ChannelingStartTime = float.NaN;
    }
}

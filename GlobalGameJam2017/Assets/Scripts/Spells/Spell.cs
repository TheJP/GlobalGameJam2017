using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spell : MonoBehaviour
{
    public float maxChannelingTime = 5.0f;
    public float maxCastRange = 15.0f;

    protected float ChannelingStartTime { get; private set; }

    public bool IsChanneling
    {
        get { return !float.IsNaN(ChannelingStartTime); }
    }

    protected virtual void Awake()
    {
        ChannelingStartTime = float.NaN;
        //Attach spell to player if possible
        var player = transform.parent.GetComponent<Player>();
        if(player != null) { player.Spell = this; }
    }

    protected virtual void Start() { }

    protected virtual void FixedUpdate()
    {
        if(Time.time > ChannelingStartTime + maxChannelingTime) { ChannelingStartTime = float.NaN; }
    }

    public virtual void StartChanneling()
    {
        ChannelingStartTime = Time.time;
    }

    public virtual bool Cast()
    {
        var channeling = IsChanneling;
        ChannelingStartTime = float.NaN;
        return channeling;
    }
}

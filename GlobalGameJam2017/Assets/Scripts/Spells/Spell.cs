using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spell : MonoBehaviour
{
    public float maxChannelingTime = 5.0f;
    protected float channelingStartTime { get; private set; }

    public bool IsChanneling
    {
        get { return !float.IsNaN(channelingStartTime); }
    }

    protected virtual void Awake()
    {
        channelingStartTime = float.NaN;
        //Attach spell to player if possible
        var player = transform.parent.GetComponent<Player>();
        if(player != null) { player.Spell = this; }
    }

    protected virtual void Start() { }

    protected virtual void FixedUpdate()
    {
        if(channelingStartTime + maxChannelingTime > Time.time) { channelingStartTime = float.NaN; }
    }

    public virtual void StartChanneling()
    {
        channelingStartTime = Time.time;
    }

    public virtual bool Cast()
    {
        var channeling = IsChanneling;
        channelingStartTime = float.NaN;
        return channeling;
    }
}

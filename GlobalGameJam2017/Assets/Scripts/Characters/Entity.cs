using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour {

    public float Health
    {
        protected set;
        get;
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void DoDamage(float dmg)
    {
        Health -= dmg;
        if(Health <= 0.0)
        {
            Kill();
        }
    }

    private void Kill()
    {
        Destroy(transform.parent);
    }
}

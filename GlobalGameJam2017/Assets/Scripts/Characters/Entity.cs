using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{

    public float maxHealth = 100;
    public float baseDamage = 20;


    public float Health
    {
        protected set;
        get;
    }

    public float Damage
    {
        protected set;
        get;
    }

    public Faction EntityFaction
    {
        protected set;
        get;
    }

    // Use this for initialization
    protected virtual void Start()
    {
        Health = maxHealth;
        Damage = baseDamage;
    }

    // Update is called once per frame
    protected virtual void FixedUpdate()
    {
    }

    public bool DoDamage(float damage)
    {
        Health -= damage;
        OnDamageTaken(damage);
        if (Health <= 0.0f)
        {
            Kill();
            return true;
        }
        return false;
    }

    protected virtual void OnDamageTaken(float damage) { }

    private void Kill()
    {
        Destroy(gameObject);
    }

}

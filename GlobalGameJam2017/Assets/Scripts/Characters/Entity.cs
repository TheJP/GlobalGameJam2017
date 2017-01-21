using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour {

    public float maxHealth = 100;
    public float baseDamage = 20;

    private readonly List<Entity> damageDealers = new List<Entity>();
    private int tick = 0;

    public bool Immunity
    {
        protected set;
        get;
    }

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
    protected virtual void Start () {
        Health = maxHealth;
        Damage = baseDamage;
        Immunity = false;
	}

    // Update is called once per frame
    protected virtual void FixedUpdate () {
        DoDamage();
    }

    public void AddDamageDealer(Entity entity)
    {
        damageDealers.Add(entity);
    }

    public bool RemoveDamageDealer(Entity entity)
    {
        return damageDealers.Remove(entity);
    }

    private void DoDamage()
    {
        if (tick == 60)
        {
            foreach(Entity entity in damageDealers) {
                if (this.EntityFaction != entity.EntityFaction && !Immunity)
                {
                    Health -= entity.Damage;
                    Debug.Log("Damage dealt: " + entity.Damage);
                    if (Health <= 0.0f)
                    {
                        Kill();
                        break;
                    }
                }
            }
            tick = 0;
        }
        tick++;
    }

    private void Kill()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        Entity entity = other.GetComponent<Entity>();
        if (entity != null)
        {
            entity.AddDamageDealer(this);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Entity entity = other.GetComponent<Entity>();
        if (entity != null)
        {
            entity.RemoveDamageDealer(this);
        }
    }
}

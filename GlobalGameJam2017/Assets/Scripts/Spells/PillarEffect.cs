using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class PillarEffect : MonoBehaviour
{

    float strength = 0f;
    bool rising = false;
    bool lowering = false;
    bool hold = false;
    float radius = 0f;
    Vector3 pos = Vector3.zero;
    Vector3 rot = Vector3.zero;

    public float sleep = 1.7f;
    public float motion = 0.3f;
    public float wait = 0.4f;
    //public ParticleSystem particle;
    private float factor = 0.4f;

    public float Damage
    {
        get;
        set;
    }


    // Use this for initialization
    void Start()
    {
        Damage = transform.parent.parent.GetComponent<PillarSpell>().baseDamage;
    }

    private void LateUpdate()
    {
        if (hold)
        {
            transform.position = new Vector3(pos.x, transform.position.y, pos.z);
            transform.eulerAngles = rot;
            //particle.transform.position = transform.position;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        if (rising)
        {
            transform.Translate(Vector3.up * strength * factor);
            if(transform.position.y > 0f)
            {
                transform.position = new Vector3(transform.position.x, 0, transform.position.z);
            }
        }
        if (lowering)
        {
            transform.Translate(Vector3.down * strength * factor);
        }

    }

    private void StartRising()
    {
        transform.localScale = new Vector3(radius, radius, radius);
        transform.position = new Vector3(pos.x, transform.position.y, pos.z);

        transform.parent.parent.parent.GetComponentInChildren<AudioPlay>().MiniMountain();
        //var module = particle.shape;
        //module.radius = radius; 
        //particle.Play();
        rising = true;
        hold = true;
        Invoke("StopRising", motion);
    }

    private void StopRising()
    {
        rising = false;
        Invoke("StartLowering", wait);
    }

    private void StartLowering()
    {
        lowering = true;
        Invoke("StopLowering", motion);
    }
    private void StopLowering()
    {
        lowering = false;
        hold = false;
        Reset();
    }

    public void StartEffect(Vector3 pos, float radius, float strength)
    {
        this.radius = radius;
        this.pos = pos;
        this.rot = transform.eulerAngles;
        this.strength = strength;
        Invoke("StartRising", sleep);
    }
    private void Reset()
    {
        transform.localPosition = Vector3.zero;
        transform.localScale = new Vector3(1e-7f, 1e-7f, 1e-7f);
    }

    private void OnTriggerEnter(Collider other)
    {
        Enemy entity = other.GetComponent<Enemy>();
        if(entity != null)
        {
            entity.DoDamage(strength * Damage);
            //Debug.Log((strength * Damage) + " damage done.");
            Player tmp = transform.parent.parent.parent.GetComponent<Player>();
            if(tmp != null)
            {
                entity.setAgro(tmp);
            }
        }
    }

}

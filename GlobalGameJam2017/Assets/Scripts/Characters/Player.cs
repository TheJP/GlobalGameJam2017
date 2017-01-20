using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity {


    private List<GameObject> damageDealer;
    private float speed = 0.1f;

    void Start () {
        Health = 500;
	}
	
	void FixedUpdate () {

        Move();
        
    }

    private void Move()
    {
        Vector3 v = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
        {
            v += (Vector3.forward);
        }
        if (Input.GetKey(KeyCode.S))
        {
            v += (Vector3.back);
        }
        if (Input.GetKey(KeyCode.A))
        {
            v += (Vector3.left);
        }
        if (Input.GetKey(KeyCode.D))
        {
            v += (Vector3.right);
        }
        transform.Translate(v.normalized * speed);
    }

    private void OnTriggerEnter(Collider other)
    {

    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat : MonoBehaviour
{

    public GameObject BoatRegion;
    private Vector3 _target;
    public float speed;

	// Use this for initialization
	void Start () {
        _target = RandomPointOnPlane();
    }
	
	// Update is called once per frame
	void Update () {
	
        
        this.GetComponent<Rigidbody>().velocity = ((_target - transform.localPosition).normalized * speed);

        if (Vector3.Distance(_target, this.transform.localPosition) < speed * 2)
	    {

            _target = RandomPointOnPlane();
        }

        //Debug.DrawRay(transform.pCKosition, Camera^§ _target - transform.position, Color.red);
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(this.GetComponent<Rigidbody>().velocity), 0.1f);
        //Debug.Log(_target);


    }

    private Vector3 RandomPointOnPlane()
    {
        var x = Random.Range(-(BoatRegion.transform.position.x), BoatRegion.transform.position.x);
        var y = 0;// Random.Range(position.y - scale.x / 2f, position.y + scale.y / 2f);
        //var z = Random.Range(position.z - scale.x / 0.2f, position.z + scale.z / 0.2f);
        var z = Random.Range(-(BoatRegion.transform.position.z), BoatRegion.transform.position.z);
        Vector3 randomPoint = new Vector3(x*10, y*10, z*10);

        return randomPoint;
    }
}

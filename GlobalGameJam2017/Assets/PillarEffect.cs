using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarEffect : MonoBehaviour {

    float strength = 0f;
    bool animated = false;
    int timer = 0;
    private float factor = 0.13333f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(animated)
        {
            if (timer <= 15)
            {
                transform.Translate(Vector3.up * strength * factor);
            }
            if (timer > 15 && timer < 30)
            {
                transform.Translate(Vector3.up * strength * -factor);
            }
            timer++;
            if(timer == 30)
            {
                Reset();
            }
        }
        Reset();

    }

    public void StartEffect(Vector3 pos, float radius, float strength)
    {
        transform.localPosition = pos;
        transform.localScale = new Vector3(radius, 1, radius);
        this.strength = strength;
        animated = true;
    }
    private void Reset()
    {
        timer = 0;
        transform.localPosition = Vector3.zero;
    }
}

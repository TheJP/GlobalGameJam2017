using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlay : MonoBehaviour {

	// Use this for initialization
	void Start () {
        AudioSource audio = Camera.main.GetComponent<AudioSource>();
        audio.Play();
        audio.Play(44100);
    }
	
}

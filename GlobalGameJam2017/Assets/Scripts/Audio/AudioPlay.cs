using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlay : MonoBehaviour
{

    public AudioClip step1;
    public AudioClip step2;
    public AudioClip channeling;
    public AudioClip impactWind;
    public AudioClip miniMountain;


    
    private bool _walkToggle = false;
    // Use this for initialization
    public void Stop () {
        var audio = GetComponent<AudioSource>();

        audio.Stop();
    }

    public void TogglesStep()
    {
        var audio = GetComponent<AudioSource>();
        audio.clip = _walkToggle ? step1 : step2;

        _walkToggle = !_walkToggle;

        audio.Play();
        //audio.Play(44100);
    }

    public void Channeling()
    {
        var audio = GetComponent<AudioSource>();
        audio.clip = channeling;


        audio.Play();
        //audio.Play(44100);
    }

    public void ImpactWind()
    {
        var audio = GetComponent<AudioSource>();
        audio.clip = impactWind;


        audio.Play();
        //audio.Play(44100);
    }

    public void MiniMountain()
    {
        var audio = GetComponent<AudioSource>();
        audio.clip = miniMountain;


        audio.Play();
        //audio.Play(44100);
    }


}

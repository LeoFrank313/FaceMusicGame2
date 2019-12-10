using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {


    public static AudioManager instance = null;
    // Use this for initialization

    public AudioSource mainSouce;
    public AudioSource noseSouce;
    public AudioSource supriseSource;
    public AudioSource eyeCloseSource;

    public AudioClip[] audioTracks;
   
	void Awake () {
        if(instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
	}

    private void Start()
    {
        mainSouce = gameObject.AddComponent<AudioSource>();
        noseSouce = gameObject.AddComponent<AudioSource>();
        supriseSource = gameObject.AddComponent<AudioSource>();
        eyeCloseSource = gameObject.AddComponent<AudioSource>();
        mainSouce.clip = audioTracks[0];
        noseSouce.clip = audioTracks[1];
        supriseSource.clip = audioTracks[2];
        eyeCloseSource.clip = audioTracks[3];
        mainSouce.mute = true;
        noseSouce.mute = true;
        supriseSource.mute = true;
        eyeCloseSource.mute = true;
        mainSouce.loop = true;
        noseSouce.loop = true;
        supriseSource.loop = true;
        eyeCloseSource.loop = true;

    }

    // Update is called once per frame
    public void startPlay()
    {
        mainSouce.Play();
        noseSouce.Play();
        supriseSource.Play();
        eyeCloseSource.Play();
    }

    public void playMainMusic()
    {
        mainSouce.mute = false;
    }
    public void playNoseMusic()
    {
        noseSouce.mute = false;
    }
    public void playSupriseMusic()
    {
        supriseSource.mute = false;
    }

    public void playeyeMusic()
    {
        eyeCloseSource.mute = false;
    }
    public void volumeChange(float s,float n,float e)
    {
        noseSouce.volume = n;
        supriseSource.volume = s;
        eyeCloseSource.volume = e;
    }
    public void pitchChange(float p)
    {
        mainSouce.pitch = p;
        noseSouce.pitch = p;
        supriseSource.pitch = p;
        eyeCloseSource.pitch = p;
    }
}

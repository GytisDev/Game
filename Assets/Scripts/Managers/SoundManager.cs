using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour {

    public Slider volumeslider;
    public float volume = 1;
    AudioSource audio;

	// Use this for initialization
	void Start () {
        volumeslider.value = 1;
        audio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        volume = volumeslider.value;
        audio.volume = volume;
	}
}

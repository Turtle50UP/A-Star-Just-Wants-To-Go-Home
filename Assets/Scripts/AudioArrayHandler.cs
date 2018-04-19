using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioArrayHandler : MonoBehaviour {

	public AudioSource[] audioArray;

	public void PlayAudio(AudioClip clip){
		foreach(AudioSource aSource in audioArray){
			if(!aSource.isPlaying){
				aSource.clip = clip;
				aSource.Play();
			}
		}
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

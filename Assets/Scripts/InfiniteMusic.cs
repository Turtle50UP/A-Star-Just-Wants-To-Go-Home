using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteMusic : MonoBehaviour {

	// Use this for initialization
	public AudioSource audioSource;
	void Start () {
		audioSource.Play();
	}
	
	// Update is called once per frame
	void Update () {
		if(!audioSource.isPlaying){
			audioSource.Play();
		}
	}
}

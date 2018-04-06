using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	// Use this for initialization
	public float maxrad = 4.0f;
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		GameManager gm = GameObject.Find("GameManager").GetComponent<GameManager>();
		Vector2 thispos = this.transform.position;
		Vector2 playerpos = gm.player.transform.position;
		Vector2 change;
		float dist = 0f;
		bool isOutsideRad;
		thispos = playerpos - thispos;
		dist = thispos.magnitude;
		isOutsideRad = dist > maxrad;
		if(isOutsideRad){
			change = thispos.normalized*(dist - maxrad);
		}
		else{
			change = Vector2.zero;
		}
		thispos = this.transform.position;
		this.transform.position = new Vector3(thispos.x + change.x,
											  thispos.y + change.y,
											  this.transform.position.z);
	}
}

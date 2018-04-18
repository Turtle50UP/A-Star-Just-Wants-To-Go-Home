using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	// Use this for initialization
	public float maxrad = 4.0f;
	public GameObject player;
	public GameManager gm;
	bool isPlayer1;
	void Start () {
		string thisobj = this.name;
		string plnum = thisobj.Substring(thisobj.Length - 1);
		if(plnum == "1"){
			isPlayer1 = true;
		}
		else{
			isPlayer1 = false;
		}
	}

	void InPlayCamera(){
		Vector2 thispos = this.transform.position;
		Vector2 playerpos = player.transform.position;
		Vector2 change;
		float dist = 0f;
		bool isOutsideRad;
		thispos = playerpos - thispos;
		dist = thispos.magnitude;
		isOutsideRad = dist > maxrad;
		if(isOutsideRad){
			if(dist > 70f){
				this.transform.position = new Vector3(
											playerpos.x,
											playerpos.y,
											this.transform.position.z);
				return;
			}
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
	
	// Update is called once per frame
	void Update () {
		if(gm.startingMenu){
			this.transform.position = new Vector3(gm.p2startloc.x,
												gm.p2startloc.y,
												this.transform.position.z);
		}
		else{
			if(isPlayer1){
				if(gm.isViewingMinimapP1){
					this.transform.position = new Vector3(gm.viewloc.x,
													gm.viewloc.y,
													this.transform.position.z);
				}
				else{
					InPlayCamera();
				}
			}
			else{
				if(gm.isViewingMinimapP2){
					this.transform.position = new Vector3(gm.viewloc.x,
													gm.viewloc.y,
													this.transform.position.z);
				}
				else{
					InPlayCamera();
				}
			}
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	// Use this for initialization
	public float maxrad = 4.0f;
	public GameObject player;
	public GameManager gm;
	public CameraHintPositionManager cameraHintPositionManager;
	bool isPlayer1;
	public float viewYOffset;
	public float regularYOffset;
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
		thispos.y += regularYOffset;
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

	void InClueCamera(){
		Vector2 playerLoc = new Vector2(player.transform.position.x,
								player.transform.position.y);
		float playerMag = playerLoc.magnitude;
		playerMag /= gm.viewRadius;
		playerMag *= gm.teleRadius;
		playerLoc.Normalize();
		playerLoc *= playerMag;
		playerLoc += gm.patternloc;
		Debug.Log(playerLoc);
		Vector3 res = playerLoc;
		res.z = this.transform.position.z;
		this.transform.position = res;
	}
	
	// Update is called once per frame
	void Update () {
		if(gm.startingMenu){
			this.transform.position = new Vector3(gm.startloc.x,
												gm.startloc.y,
												this.transform.position.z);
		}
		else{
			if(player.GetComponent<PlayerMove>().viewingMinimap){
				this.transform.position = new Vector3(gm.viewloc.x,
													viewYOffset,
													this.transform.position.z);
			}
			else if(player.GetComponent<PlayerViewHints>().viewingHints){
				InClueCamera();
				/*

				int camerapos = player.GetComponent<PlayerViewHints>().numViewing;
				Vector3 newpos = cameraHintPositionManager.cameraViews[camerapos].transform.position;
				this.transform.position = new Vector3(newpos.x,
												newpos.y,
												this.transform.position.z);*/
			}
			else{
				InPlayCamera();
			}
		}
	}
}

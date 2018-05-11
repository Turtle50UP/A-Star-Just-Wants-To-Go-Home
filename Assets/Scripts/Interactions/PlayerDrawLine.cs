using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDrawLine : AbstractBehavior {

	public GameObject selectedStar;
	string starPickup = "starPickup";
	public GameObject edge;
	public SingleSpawner edgeSpawner;
	public AudioClip lineDrawn;
	public AudioClip lineBroken;
	public AudioArrayHandler audioArray;
	// Use this for initialization
	void Start () {
	}

	bool DrawConstellationLine(GameObject otherStar){
		edgeSpawner.Despawn();
		edge = null;
		if(otherStar == null){
			selectedStar = null;
			return false;
		}
		StarDetails sd1 = selectedStar.GetComponent<StarDetails>();
		StarDetails sd2 = otherStar.GetComponent<StarDetails>();
		selectedStar = null;
		if(sd1.groupName == sd2.groupName || sd2.groupName.Contains(sd1.groupName)){
			return sd1.cm.DrawEdge(sd1.index,sd2.index);
		}
		else if(sd1.groupName.Contains(sd2.groupName)){
			return sd2.cm.DrawEdge(sd1.index,sd2.index);
		}
		return false;
	}

	void DrawEdge(){
		if(selectedStar != null){
			Vector2 vve1 = new Vector2(this.transform.position.x,
										this.transform.position.y);
			Vector2 vve2 = new Vector2(selectedStar.transform.position.x,
										selectedStar.transform.position.y);
			if(vve1.y < vve2.y){
				Vector2 temp = vve1;
				vve1 = vve2;
				vve2 = temp;
			}
			Vector2 vdesc = vve1 - vve2;
			float linelen = vdesc.magnitude;
			Vector2 midpoint = (vve1 + vve2) / 2.0f;
			float angle = Vector2.Angle(new Vector2(1.0f,0f),vdesc) + (
                    vdesc.y > 0 ? - 90f : +90f);
			edge.transform.eulerAngles = new Vector3(
				edge.transform.eulerAngles.x,
				edge.transform.eulerAngles.y,
				angle);
			edge.transform.localScale = new Vector3(
				edge.transform.localScale.x,
				linelen,
				edge.transform.localScale.z);
			edge.transform.position = midpoint;
		}
	}
	public void ResetEdge(){
		DrawConstellationLine(null);
	}
	
	// Update is called once per frame
	protected virtual void FixedUpdate () {
		if(selectedStar != null){
			if(edge == null){
				edge = edgeSpawner.Spawn();
				//edge.transform.position = new Vector3(
					//);
			}
			DrawEdge();

		}
		else{
			if(edge != null){
				edgeSpawner.Despawn();
				edge = null;
			}
		}
		if(inputState.GetButtonValue(inputButtons[0])){
			if(collisionState.colliderStatus[starPickup]){
				Collider2D[] collmems = collisionState.collidingMembers[starPickup];
				GameObject temp = GetClosestCollidingMember(collmems);
				if(inputState.GetButtonHoldTime(inputButtons[0]) < gm.epsilon){
					if(selectedStar == null){
						selectedStar = temp;
					}
					else if(selectedStar != temp){
						bool res = DrawConstellationLine(temp);
						if(res){
							plm.playerExpressionManager.Glee();
							audioArray.PlayAudio(lineDrawn);
						}
						else{
							plm.playerExpressionManager.Sadness();
							audioArray.PlayAudio(lineBroken);
						}
					}
					else{
						selectedStar = null;
					}
				}
			}
			else{
				ResetEdge();
			}
		}
	}
}

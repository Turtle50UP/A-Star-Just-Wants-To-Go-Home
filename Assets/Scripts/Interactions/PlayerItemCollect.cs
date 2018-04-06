using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemCollect : AbstractBehavior {

	public int currentCollected = 0;

	string starPickup = "starPickup";
	
	// Update is called once per frame
	void FixedUpdate () {
		if(collisionState.colliderStatus[starPickup]){
			Collider2D[] collArr = collisionState.collidingMembers[starPickup];
			foreach(Collider2D coll in collArr){
				GameObjUtil.Destroy(coll.gameObject);
				currentCollected ++;
			}
		}
	}
}

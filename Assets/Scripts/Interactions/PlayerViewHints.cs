using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerViewHints : AbstractBehavior {

	public bool viewingHints;
	public int numViewing = 0;
	public CameraHintPositionManager cameraHintPositionManager;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(!viewingHints){
			if(inputState.GetButtonValue(inputButtons[0])){
				if(inputState.GetButtonHoldTime(inputButtons[0]) < gm.epsilon){
					viewingHints = true;
				}
			}
		}
		else{
			if(inputState.GetButtonValue(inputButtons[0])){
				if(inputState.GetButtonHoldTime(inputButtons[0]) < gm.epsilon){
					viewingHints = false;
				}
			}
		}
		if(inputState.GetButtonValue(inputButtons[1])){
				if(inputState.GetButtonHoldTime(inputButtons[1]) < gm.epsilon){
					gm.acm.SelectPrevConstellation();

					if(numViewing - 1 < 0){
						numViewing = cameraHintPositionManager.cameraViews.Length - 1;
					}
					else{
						numViewing --;
					}
				}
			}
			if(inputState.GetButtonValue(inputButtons[2])){
				if(inputState.GetButtonHoldTime(inputButtons[2]) < gm.epsilon){
					gm.acm.SelectNextConstellation();

					if(numViewing + 1 >= cameraHintPositionManager.cameraViews.Length){
						numViewing = 0;
					}
					else{
						numViewing ++;
					}
				}
			}
	}
}

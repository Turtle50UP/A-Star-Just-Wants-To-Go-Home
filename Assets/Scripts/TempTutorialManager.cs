using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TempTutorialManager : AbstractBehavior {
	public Text[] texts;
	/* 0: Select Character
	   1: Ready Up
	   2: Wait for Other, press x again
	   3: First select
	   4: Second select
	   5: View main map
	   6: View hint
	   7: View nav hint
	   8: Leave nav
	   9: Good luck!
	 */
	public bool[] events;
	int curStep = 0;

	void DrawHint(){
		for(int i = 0; i < texts.Length; i++){
			if(i == curStep){
				texts[i].canvasRenderer.SetAlpha(1);
			}
			else{
				texts[i].canvasRenderer.SetAlpha(0);
			}
		}
	}
	public void Restart(){
		curStep = 0;
	}

	// Use this for initialization
	void Start () {
		DrawHint();
	}

	bool SwitchStep(Buttons button, bool inPlay){
		if(inputState.GetButtonValue(button) && (gm.inPlay == inPlay)){
			curStep ++;
			return true;
		}
		return false;
	}
	bool SwitchStep(Buttons button){
		if(inputState.GetButtonValue(button)){
			curStep ++;
			return true;
		}
		return false;
	}
	
	// Update is called once per frame
	void Update () {
		print(inputState);
		if(inputState.GetIfAnyPressed()){
			switch(curStep){
				case 0:
				SwitchStep(inputButtons[0],false);  //x
				break;
				case 1:
				SwitchStep(inputButtons[1]);  //y
				break;
				case 2:
				SwitchStep(inputButtons[2],true);  //b
				break;
				case 3:
				SwitchStep(inputButtons[0],true); //x
				break;
				case 4:
				SwitchStep(inputButtons[2],true); //x
				break;
				case 5:
				SwitchStep(inputButtons[1],true);  //y
				break;
				case 6:
				SwitchStep(inputButtons[3],true);  //a
				break;
				case 7:
				if(!SwitchStep(inputButtons[4],true)){ //L
					SwitchStep(inputButtons[5],true);  //R
				}  //L/R
				break;
				case 8:
				SwitchStep(inputButtons[3],true);  //a
				break;
				case 9:
				SwitchStep(inputButtons[0],true);  //x
				break;
				default:
				break;
			}
			if(curStep > 10){
				return;
			}
			DrawHint();
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class TutorialStrings{
	public string text;
	public Color color;
}

[System.Serializable]
public class TutorialText{
	public string name;
	public string labelText;
	public Text controlLabel;
	public Text controlText;
	public TutorialStrings[] tutorialStrings;

	public void SetText(int i){
		if(i >= tutorialStrings.Length){
			return;
		}
		TutorialStrings tstring = tutorialStrings[i];
		controlText.text = tstring.text;
		controlText.color = tstring.color;
	}
}

public class ControlTutorialManager : MonoBehaviour {
	string interact = "Interact";
	string clues = "Clues";
	string hint = "Hint";
	string minimap = "Minimap";
	string next = "Next";
	string prev = "Prev";
	string[] keynames = {"Interact", "Clues", "Hint", "Minimap","Next","Prev"};
	public TutorialText[] tutorialText;
	public GameManager gm;

	void SetupTutorialTexts(){
		for(int i = 0; i < tutorialText.Length; i++){
			TutorialText tt = tutorialText[i];
			string posKey = "";
			/*foreach(AxisData ad in axisData){
				if(ad.name == tt.name){
					posKey = ad.posButton;
					break;
				}
			}*/
			tt.labelText = tt.labelText;// + posKey;
			tt.controlLabel.text = tt.labelText;
		}
	}
	void Start(){
		//base.Start();
		SetupTutorialTexts();
	}
	
	// Update is called once per frame
	void Update () {
		if(gm.startingMenu){
			for(int i = 0; i < tutorialText.Length; i++){
				TutorialText tt = tutorialText[i];
				switch(tt.name){
					case "Minimap":
						if(gm.playerManager.playerLevelSelect.SelectedConstellationName == "NOTHING"){
							tt.SetText(0);
						}
						else{
							tt.SetText(1);
						}
						break;
					default:
						tt.SetText(0);
						break;
				}
			}
		}
		else if(gm.inPlay){
			for(int i = 0; i < tutorialText.Length; i++){
				TutorialText tt = tutorialText[i];
				switch(tt.name){
					case "Interact":
						if(gm.playerManager.playerDrawLine.selectedStar == null){
							tt.SetText(1);
						}
						else{
							tt.SetText(2);
						}
						break;
					case "Minimap":
						tt.SetText(2);
						break;
					case "Clues":
						if(!gm.playerManager.playerViewHints.viewingHints){
							tt.SetText(1);
						}
						else{
							tt.SetText(2);
						}
						break;
					case "Next":
					case "Prev":
						tt.SetText(1);
						break;
					default:
						tt.SetText(0);
						break;
				}
			}
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstellationViewManager : MonoBehaviour {
	public GameManager gameManager;
	public ConstellationManager[] constellationManagers;
	public ImageSwitch imageSwitch;
	public bool[] whichAlwaysOn;
	public int expertSpawn;
	public int hardSpawn;
	public int mediumSpawn;
	public int easySpawn;
	public bool[] whichToTurnOn;
	string focus1;
	string focus2;
	bool simpleMode = true;
	int howmanytoturnon = 5;

	public void SetupScreen(string difficultyp1,string difficultyp2,string constname1, string constname2){//PlayerLevelSelect p1Selected, PlayerLevelSelect p2Selected){
		//string difficultyp1 = p1Selected.selectObject.GetComponent<ConstellationManager>().constellation.difficulty;
		//string difficultyp2 = p2Selected.selectObject.GetComponent<ConstellationManager>().constellation.difficulty;
		focus1 = constname1;//p1Selected.SelectedConstellationName;
		focus2 = constname2;//p2Selected.SelectedConstellationName;
		Debug.Log(difficultyp1);
		System.Random random = new System.Random();
		for(int i = 0; i < whichToTurnOn.Length; i++){
			whichToTurnOn[i] = whichAlwaysOn[i];
		}
		int randomNumber1;
		for(int i = 0; i < howmanytoturnon; i++){
			randomNumber1 = random.Next(0,whichAlwaysOn.Length);
			while(whichToTurnOn[randomNumber1] || (constellationManagers[randomNumber1].name == focus1)||(constellationManagers[randomNumber1].name == focus2)){
				randomNumber1 = random.Next(0,whichAlwaysOn.Length);
			}
			whichToTurnOn[randomNumber1] = true;
		}
		if(simpleMode){
			for(int i = 0; i < whichToTurnOn.Length; i++){
			if(whichToTurnOn[i]){
				constellationManagers[i].DrawAllEdges();
			}
		}
			return;
		}

		if(difficultyp1 == "Insane" && difficultyp2 == "Insane"){
			for(int i = 0; i < whichToTurnOn.Length; i++){
			if(whichToTurnOn[i]){
				constellationManagers[i].DrawAllEdges();
			}
		}
			return;
		}
		randomNumber1 = random.Next(0,whichAlwaysOn.Length);
		while(whichToTurnOn[randomNumber1] || (constellationManagers[randomNumber1].name == focus1)||(constellationManagers[randomNumber1].name == focus2)){
			randomNumber1 = random.Next(0,whichAlwaysOn.Length);
		}
		whichToTurnOn[randomNumber1] = true;
		randomNumber1 = random.Next(0,whichAlwaysOn.Length);
		while(whichToTurnOn[randomNumber1] || (constellationManagers[randomNumber1].name == focus1)||(constellationManagers[randomNumber1].name == focus2)){
			randomNumber1 = random.Next(0,whichAlwaysOn.Length);
		}
		whichToTurnOn[randomNumber1] = true;
		if(difficultyp1 == "Hard" || difficultyp2 == "Hard"){
			for(int i = 0; i < whichToTurnOn.Length; i++){
			if(whichToTurnOn[i]){
				constellationManagers[i].DrawAllEdges();
			}
		}
			return;
		}
		randomNumber1 = random.Next(0,whichAlwaysOn.Length);
		while(whichToTurnOn[randomNumber1] || (constellationManagers[randomNumber1].name == focus1)||(constellationManagers[randomNumber1].name == focus2)){
			randomNumber1 = random.Next(0,whichAlwaysOn.Length);
		}
		whichToTurnOn[randomNumber1] = true;
		randomNumber1 = random.Next(0,whichAlwaysOn.Length);
		while(whichToTurnOn[randomNumber1] || (constellationManagers[randomNumber1].name == focus1)||(constellationManagers[randomNumber1].name == focus2)){
			randomNumber1 = random.Next(0,whichAlwaysOn.Length);
		}
		whichToTurnOn[randomNumber1] = true;
		if(difficultyp1 == "Medium" || difficultyp2 == "Medium"){
			for(int i = 0; i < whichToTurnOn.Length; i++){
			if(whichToTurnOn[i]){
				constellationManagers[i].DrawAllEdges();
			}
		}
			return;
		}
		randomNumber1 = random.Next(0,whichAlwaysOn.Length);
		while(whichToTurnOn[randomNumber1] || (constellationManagers[randomNumber1].name == focus1)||(constellationManagers[randomNumber1].name == focus2)){
			randomNumber1 = random.Next(0,whichAlwaysOn.Length);
		}
		whichToTurnOn[randomNumber1] = true;
		randomNumber1 = random.Next(0,whichAlwaysOn.Length);
		while(whichToTurnOn[randomNumber1] || (constellationManagers[randomNumber1].name == focus1)||(constellationManagers[randomNumber1].name == focus2)){
			randomNumber1 = random.Next(0,whichAlwaysOn.Length);
		}
		whichToTurnOn[randomNumber1] = true;
		for(int i = 0; i < whichToTurnOn.Length; i++){
			if(whichToTurnOn[i]){
				constellationManagers[i].DrawAllEdges();
			}
		}
	}

	public bool FinishedDrawing(){
		int count = 0;
		for(int i = 0; i < constellationManagers.Length; i++){
			//check each
			bool hasCompleted = constellationManagers[i].FinishedDrawing;
			if(hasCompleted){
				Debug.Log(name);
				Debug.Log(focus1);
				if(constellationManagers[i].name == focus1){
					count ++;
				}
				if(constellationManagers[i].name == focus2){
					count ++;
				}
			}
		}
		Debug.Log(count);
		return count >= 2;
	}

	public void UpdateSuccesses(){
		for(int i = 0; i < imageSwitch.trophyList.Length; i++){
			Debug.Log(imageSwitch.trophyList[i].name);
			Debug.Log(focus1);
			if(imageSwitch.trophyList[i].name == focus1 || imageSwitch.trophyList[i].name == focus2){
				Debug.Log("It should update here");
				imageSwitch.initOn[i] = true;
			}
		}
		imageSwitch.UpdateTrophies();
	}

	public void ResetGame(){
		for(int i = 0; i < whichToTurnOn.Length; i++){
			constellationManagers[i].DespawnConstellation();
			Debug.Log(constellationManagers[i].FinishedDrawing);
			whichToTurnOn[i] = false;
		}
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyUp(KeyCode.C)){
			if(howmanytoturnon > 0){
				howmanytoturnon --;
			}
		}
		if(Input.GetKeyUp(KeyCode.O)){
			if(howmanytoturnon < 6){
				howmanytoturnon ++;
			}
		}
	}
}

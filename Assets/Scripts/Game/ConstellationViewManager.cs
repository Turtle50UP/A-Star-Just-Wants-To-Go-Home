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
	string focus{
		get{
			return gameManager.playerManager.playerLevelSelect.SelectedConstellationName;
		}
	}
	bool simpleMode = true;
	int howmanytoturnon = 0;

	public void SetupScreen(string difficultyp,string constname){
		System.Random random = new System.Random();
		for(int i = 0; i < whichToTurnOn.Length; i++){
			whichToTurnOn[i] = whichAlwaysOn[i];
		}
		int randomNumber1;
		for(int i = 0; i < howmanytoturnon; i++){
			randomNumber1 = random.Next(0,whichAlwaysOn.Length);
			while(whichToTurnOn[randomNumber1] || (constellationManagers[randomNumber1].name == focus)){
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

		if(difficultyp == "Insane"){
			for(int i = 0; i < whichToTurnOn.Length; i++){
			if(whichToTurnOn[i]){
				constellationManagers[i].DrawAllEdges();
			}
		}
			return;
		}
		randomNumber1 = random.Next(0,whichAlwaysOn.Length);
		while(whichToTurnOn[randomNumber1] || (constellationManagers[randomNumber1].name == focus)){
			randomNumber1 = random.Next(0,whichAlwaysOn.Length);
		}
		whichToTurnOn[randomNumber1] = true;
		randomNumber1 = random.Next(0,whichAlwaysOn.Length);
		while(whichToTurnOn[randomNumber1] || (constellationManagers[randomNumber1].name == focus)){
			randomNumber1 = random.Next(0,whichAlwaysOn.Length);
		}
		whichToTurnOn[randomNumber1] = true;
		if(difficultyp == "Hard"){
			for(int i = 0; i < whichToTurnOn.Length; i++){
			if(whichToTurnOn[i]){
				constellationManagers[i].DrawAllEdges();
			}
		}
			return;
		}
		randomNumber1 = random.Next(0,whichAlwaysOn.Length);
		while(whichToTurnOn[randomNumber1] || (constellationManagers[randomNumber1].name == focus)){
			randomNumber1 = random.Next(0,whichAlwaysOn.Length);
		}
		whichToTurnOn[randomNumber1] = true;
		randomNumber1 = random.Next(0,whichAlwaysOn.Length);
		while(whichToTurnOn[randomNumber1] || (constellationManagers[randomNumber1].name == focus)){
			randomNumber1 = random.Next(0,whichAlwaysOn.Length);
		}
		whichToTurnOn[randomNumber1] = true;
		if(difficultyp == "Medium"){
			for(int i = 0; i < whichToTurnOn.Length; i++){
			if(whichToTurnOn[i]){
				constellationManagers[i].DrawAllEdges();
			}
		}
			return;
		}
		randomNumber1 = random.Next(0,whichAlwaysOn.Length);
		while(whichToTurnOn[randomNumber1] || (constellationManagers[randomNumber1].name == focus)){
			randomNumber1 = random.Next(0,whichAlwaysOn.Length);
		}
		whichToTurnOn[randomNumber1] = true;
		randomNumber1 = random.Next(0,whichAlwaysOn.Length);
		while(whichToTurnOn[randomNumber1] || (constellationManagers[randomNumber1].name == focus)){
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
				if(constellationManagers[i].name == focus){
					count ++;
				}
			}
		}
		return count >= 1;
	}

	public void UpdateSuccesses(){
		for(int i = 0; i < imageSwitch.trophyList.Length; i++){
			if(imageSwitch.trophyList[i].name == focus){
				imageSwitch.initOn[i] = true;
			}
		}
		imageSwitch.UpdateTrophies();
	}

	public void ResetGame(){
		for(int i = 0; i < whichToTurnOn.Length; i++){
			constellationManagers[i].DespawnConstellation();
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

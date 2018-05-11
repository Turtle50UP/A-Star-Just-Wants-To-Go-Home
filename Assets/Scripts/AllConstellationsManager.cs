using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstellationData{
	public string name;
	public string difficulty;
	public bool isCompleted;
	public bool hasRegisteredCompleted;
	public bool completedAsSelected;
	public ConstellationManager gameConstellation;
	public GameObject viewConstellation;
	public GameObject trophy;
	public bool isSelected;
	public float constellationScore;
}

public class AllConstellationsManager : MonoBehaviour {
	public GameObject[] constellations;
	string[] constellationNames;
	bool loadedConstellations;
	public string constellationManagerEndString;
	public string constellationTrophyEndString;
	public ConstellationData[] constellationsData;
	public GameManager gm;
	public ConstellationImageManager cim;
	public int currentConstellationNumber;
	int constNum{
		get{
			return constellations.Length;
		}
	}

	public void SetCurrentConstNum(string name){
		for(int i = 0; i < constNum; i ++){
			if(constellationsData[i].name == name){
				currentConstellationNumber = i;
				break;
			}
		}
		Debug.Log(currentConstellationNumber);
	}

	public void SelectNextConstellation(){
		if(currentConstellationNumber < 0){
			return;
		}
		gm.constellationViewManager.FinishedDrawing();
		for(int i = 1; i < constNum; i ++){
			int temp = (currentConstellationNumber + i) % constNum;
			if(!constellationsData[temp].isCompleted){
				currentConstellationNumber = temp;
				gm.playerManager.playerLevelSelect.Select(constellationsData[temp].trophy);
				break;
			}
		}
		Debug.Log(currentConstellationNumber);
		gm.playerManager.playerLevelSelect.constellationImageManager.ColorTheseConstellations();
	}

	public void SelectPrevConstellation(){
		if(currentConstellationNumber < 0){
			return;
		}
		gm.constellationViewManager.FinishedDrawing();
		for(int i = constNum - 1; i > 0; i --){
			int temp = (currentConstellationNumber + i) % constNum;
			if(!constellationsData[temp].isCompleted){
				currentConstellationNumber = temp;
				gm.playerManager.playerLevelSelect.Select(constellationsData[temp].trophy);
				break;
			}
		}
		Debug.Log(currentConstellationNumber);
		gm.playerManager.playerLevelSelect.constellationImageManager.ColorTheseConstellations();
	}

	void LoadConstellations(){
		constellationNames = new string[constNum];
		constellationsData = new ConstellationData[constNum];
		for(int i = 0; i < constNum; i ++){
			ConstellationData cd = new ConstellationData();
			constellationsData[i] = cd;
			constellationNames[i] = constellations[i].name;
			string cmanagerStr = constellationNames[i] + constellationManagerEndString;
			string ctrophStr = constellationNames[i] + constellationTrophyEndString;
			constellationsData[i].name = constellationNames[i];
			constellationsData[i].isCompleted = false;
			constellationsData[i].hasRegisteredCompleted = false;
			constellationsData[i].completedAsSelected = false;
			constellationsData[i].gameConstellation = GameObject.Find(cmanagerStr).GetComponent<ConstellationManager>();
			constellationsData[i].viewConstellation = constellations[i];
			constellationsData[i].trophy = GameObject.Find(ctrophStr);
			constellationsData[i].difficulty = GameObject.Find(cmanagerStr).GetComponent<ConstellationManager>().constellation.difficulty;
			constellationsData[i].constellationScore = gm.GetDifficultyScore(constellationsData[i].difficulty);
		}
	}

	public void ResetConstellations(){
		for(int i = 0; i < constNum; i ++){
			constellationsData[i].isCompleted = false;
			constellationsData[i].hasRegisteredCompleted = false;
			constellationsData[i].completedAsSelected = false;
		}
	}

	public void UpdateConstellationCompletion(string name){
		Debug.Log(name);
		for(int i = 0; i < constNum; i ++){
			if(constellationsData[i].name == name){
				if(!constellationsData[i].hasRegisteredCompleted){
					constellationsData[i].hasRegisteredCompleted = true;
					constellationsData[i].isCompleted = true;
					if(gm.playerManager.playerLevelSelect.SelectedConstellationName == name){
						constellationsData[i].completedAsSelected = true;
					}
				}
			}
		}
		cim.ColorTheseConstellations();
	}

	public bool GetConstellationCompletion(string name){
		for(int i = 0; i < constNum; i ++){
			if(constellationsData[i].name == name){
				return(constellationsData[i].isCompleted);
			}
		}
		return false;
	}

	public GameObject CheckForUncompleted(){
		for(int i = 0; i < constNum; i ++){
			if(!constellationsData[i].isCompleted){
				return constellationsData[i].trophy;
			}
		}
		gm.playerManager.playerLevelSelect.Deselect();
		return null;
	}

	public float GetAllConstellationScores(){
		float res = 0;
		for(int i = 0; i < constNum; i ++){
			if(constellationsData[i].isCompleted){
				float temp = gm.GetDifficultyScore(constellationsData[i].difficulty);
				if(constellationsData[i].completedAsSelected){
					temp *= 2;
				}
				res += temp;
			}
		}
		return res;
	}

	// Use this for initialization
	void Start () {
		if(constellations == null){
			loadedConstellations = false;
		}
		else{
			loadedConstellations = true;
			LoadConstellations();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(gm.playerManager.playerLevelSelect.SelectedConstellationName == "NOTHING"){
			currentConstellationNumber = -1;
		}
		else{
			SetCurrentConstNum(gm.playerManager.playerLevelSelect.SelectedConstellationName);
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Trophy{
	public string name;
	public GameObject trophyImg;
	bool trophyOn = true;

	public bool GetIfTrophyOn(){
		return trophyOn;
	}
	public void SetIfTrophyOn(){
		trophyOn = !trophyOn;
		trophyImg.transform.position = new Vector3(
			trophyImg.transform.position.x,
			trophyImg.transform.position.y,
			-trophyImg.transform.position.z);
	}

	public void SetTrophyOff(){
		if(trophyOn){
			SetIfTrophyOn();
		}
	}
}

public class ImageSwitch : MonoBehaviour {

	public Trophy[] trophyList;
	public bool[] initOn;
	bool[] starterOn;
	public bool isEasterEggMode;

	public bool GetAllTrophiesCheck(){
		bool res = true;
		foreach(Trophy trophy in trophyList){
			res = res && trophy.GetIfTrophyOn();
		}
		return res;
	}

	public void UpdateTrophies(){
		for(int i = 0; i < trophyList.Length; i++){
			if(trophyList[i].GetIfTrophyOn() != initOn[i]){
				trophyList[i].SetIfTrophyOn();
			}
		}
	}

	public void SetTrophiesOff(){
		for(int i = 0; i < trophyList.Length; i++){
			if(trophyList[i].GetIfTrophyOn() != starterOn[i] && !starterOn[i]){
				trophyList[i].SetTrophyOff();
			}
		}
	}

	// Use this for initialization
	void Start () {
		for(int i = 0; i < trophyList.Length; i++){
			if(trophyList[i].GetIfTrophyOn() != initOn[i]){
				trophyList[i].SetIfTrophyOn();
			}
		}
		isEasterEggMode = GetAllTrophiesCheck();
		starterOn = new bool[initOn.Length];
		for(int i = 0; i < initOn.Length; i++){
			starterOn[i] = initOn[i];
		}
	}
	
	// Update is called once per frame
	void Update () {
	}
}

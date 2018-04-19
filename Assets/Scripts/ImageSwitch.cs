﻿using System.Collections;
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
}

public class ImageSwitch : MonoBehaviour {

	public Trophy[] trophyList;
	public bool[] initOn;
	public bool isEasterEggMode;

	public bool GetAllTrophiesCheck(){
		bool res = true;
		foreach(bool b in initOn){
			res = res && b;
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

	// Use this for initialization
	void Start () {
		for(int i = 0; i < trophyList.Length; i++){
			if(trophyList[i].GetIfTrophyOn() != initOn[i]){
				trophyList[i].SetIfTrophyOn();
			}
		}
		isEasterEggMode = GetAllTrophiesCheck();
	}
	
	// Update is called once per frame
	void Update () {
	}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstellationImageManager : MonoBehaviour {
	public SpriteRenderer[] spriteList;
	public Color highlightColor;
	public Color defaultColor;
	public Color completeColor;
	public PlayerLevelSelect player1;
	public AllConstellationsManager allConstellationsManager;
	string PSelected{
		get{
			return player1.SelectedConstellationName;
		}
	}

	public void ColorTheseConstellations(){
		foreach(SpriteRenderer sr in spriteList){
			if(sr.gameObject.name == PSelected){
				sr.color = highlightColor;
			}
			else if(allConstellationsManager.GetConstellationCompletion(sr.gameObject.name)){
				sr.color = completeColor;
			}
			else{
				sr.color = defaultColor;
			}
		}
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

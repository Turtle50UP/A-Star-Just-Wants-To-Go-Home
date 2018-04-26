using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstellationImageManager : MonoBehaviour {
	public SpriteRenderer[] spriteList;
	public Color highlightColor;
	public Color defaultColor;
	public PlayerLevelSelect player1;
	public PlayerLevelSelect player2;
	string P1Selected{
		get{
			return player1.SelectedConstellationName;
		}
	}
	string P2Selected{
		get{
			return player2.SelectedConstellationName;
		}
	}

	public void ColorTheseConstellations(){
		foreach(SpriteRenderer sr in spriteList){
			if(sr.gameObject.name == P1Selected || sr.gameObject.name == P2Selected){
				sr.color = highlightColor;
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

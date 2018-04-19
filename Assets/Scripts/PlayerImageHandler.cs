using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerImageHandler : MonoBehaviour {

	public Image[] constellationImgs;

	public void SetCorrectImage(string ofinterest){
		foreach(Image i in constellationImgs){
			Debug.Log(i.name);
			if(i.name == ofinterest){
				i.canvasRenderer.SetAlpha(1.0f);
			}
			else{
				i.canvasRenderer.SetAlpha(0.0f);
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

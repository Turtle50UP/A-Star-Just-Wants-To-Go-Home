using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EasterEggManager : MonoBehaviour {

	public AudioArrayHandler audioArray;
	public AudioClip communism;
	public AudioClip democracy;
	public PlayerExpressionManager p1express;
	public PlayerExpressionManager p2express;
	public ImageSwitch imageSwitch;
	public bool debug;

	// Use this for initialization
	public void PlayEasterEgg(){
		System.Random rand = new System.Random();
		double res = rand.NextDouble();
		if(res > 0.5){
			audioArray.PlayAudio(communism);
			p1express.ForThePeople();
			p2express.ForThePeople();
		}
		else{
			audioArray.PlayAudio(democracy);
			p1express.FreeMarket();
			p2express.FreeMarket();
		}
	}
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(debug){
			if(Input.GetKeyDown(KeyCode.V)){
				PlayEasterEgg();
			}
		}
		
	}
}

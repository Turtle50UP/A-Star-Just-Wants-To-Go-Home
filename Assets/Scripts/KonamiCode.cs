using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KonamiCode : AbstractBehavior {

	public AudioClip communism;
	public AudioClip democracy;
	public AudioArrayHandler audioArray;
	public PlayerExpressionManager p1express;
	public PlayerExpressionManager p2express;
	public ImageSwitch imageSwitch;

	public string konami;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(konami == ""){
			if(inputState.GetButtonValue(inputButtons[0])) //up
			{
				konami = "^";
			}
			//else{
			//	konami = "";
			//}
		}
		else if(konami == "^"){
			if(inputState.GetButtonValue(inputButtons[0])) //up
			{
				konami = "^^";
			}
			//else{
			//	konami = "";
			//}
		}
		else if(konami == "^^"){
			if(inputState.GetButtonValue(inputButtons[1])) //down
			{
				konami = "^^v";
			}
			//else{
			//	konami = "";
			//}
		}
		else if(konami == "^^v"){
			if(inputState.GetButtonValue(inputButtons[1])) //down
			{
				konami = "^^vv";
			}
			//else{
			//	konami = "";
			//}
		}
		else if(konami == "^^vv"){
			if(inputState.GetButtonValue(inputButtons[2])) //l
			{
				konami = "^^vv<";
			}
			//else{
			//	konami = "";
			//}
		}
		else if(konami == "^^vv<"){
			if(inputState.GetButtonValue(inputButtons[3])) //r
			{
				konami = "^^vv<>";
			}
			//else{
			//	konami = "";
			//}
		}
		else if(konami == "^^vv<>"){
			if(inputState.GetButtonValue(inputButtons[2])) //l
			{
				konami = "^^vv<><";
			}
			//else{
			//	konami = "";
			//}
		}
		else if(konami == "^^vv<><"){
			if(inputState.GetButtonValue(inputButtons[3])) //l
			{
				konami = "^^vv<><>";
			}
			//else{
			//	konami = "";
			//}
		}
		else if(konami == "^^vv<><>"){
			if(inputState.GetButtonValue(inputButtons[4])) //b
			{
				konami = "^^vv<><>b";
			}
			//else{
			//	konami = "";
			//}
		}
		else if(konami == "^^vv<><>b"){
			if(inputState.GetButtonValue(inputButtons[5])) //b
			{
				konami = "^^vv<><>ba";
			}
			//else{
			//	konami = "";
			//}
		}
		else if(konami == "^^vv<><>ba"){
			if(inputState.GetButtonValue(inputButtons[6])) //sel
			{
				konami = "^^vv<><>basel";
			}
			//else{
			//	konami = "";
			//}
		}
		else if(konami == "^^vv<><>basel"){
			if(inputState.GetButtonValue(inputButtons[7])) //star
			{
				if(imageSwitch.isEasterEggMode){
					System.Random rand = new System.Random();
					int res = rand.Next(0,1);
					if(res == 0){
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
				konami = "";
			}
		}
	}
}

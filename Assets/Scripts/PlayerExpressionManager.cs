using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerExpressionManager : MonoBehaviour {

	public Image peeking;
	public Image neutral;
	public Image neutral2;
	public Image happy;
	public Image sad;
	public Image victorious;
	public Image depressed;
	public Image capitalist;
	public Image communist;
	public Vector2 emotionDuration;
	float actionStartTime;
	public Vector2 altNeutralDuration;
	float timeToSwitchNeutral;
	public Vector2 sampleNeutral;
	bool inSpecial = false;
	float specialDuration;
	System.Random random;
	public GameManager gameManager;
	float alternateDuration;
	float alternateTime;
	bool isAlternate;

	// Use this for initialization
	void Awake () {
		neutral.canvasRenderer.SetAlpha(1);
		neutral2.canvasRenderer.SetAlpha(0);
		happy.canvasRenderer.SetAlpha(0);
		sad.canvasRenderer.SetAlpha(0);
		victorious.canvasRenderer.SetAlpha(0);
		depressed.canvasRenderer.SetAlpha(0);
		capitalist.canvasRenderer.SetAlpha(0);
		communist.canvasRenderer.SetAlpha(0);
		peeking.canvasRenderer.SetAlpha(0);
		random = new System.Random();
		timeToSwitchNeutral = (float)random.Next((int)sampleNeutral.x,(int)sampleNeutral.y);
		actionStartTime = Time.fixedTime;
	}

	public void Glee(){
		inSpecial = true;
		specialDuration = (float)random.Next((int)emotionDuration.x,(int)emotionDuration.y);
		actionStartTime = Time.fixedTime;
		neutral.canvasRenderer.SetAlpha(0);
		neutral2.canvasRenderer.SetAlpha(0);
		happy.canvasRenderer.SetAlpha(1);
		sad.canvasRenderer.SetAlpha(0);
		victorious.canvasRenderer.SetAlpha(0);
		depressed.canvasRenderer.SetAlpha(0);
		capitalist.canvasRenderer.SetAlpha(0);
		communist.canvasRenderer.SetAlpha(0);
		peeking.canvasRenderer.SetAlpha(0);
	}

	public void Sadness(){
		inSpecial = true;
		specialDuration = (float)random.Next((int)emotionDuration.x,(int)emotionDuration.y);
		actionStartTime = Time.fixedTime;
		neutral.canvasRenderer.SetAlpha(0);
		neutral2.canvasRenderer.SetAlpha(0);
		happy.canvasRenderer.SetAlpha(0);
		sad.canvasRenderer.SetAlpha(1);
		victorious.canvasRenderer.SetAlpha(0);
		depressed.canvasRenderer.SetAlpha(0);
		capitalist.canvasRenderer.SetAlpha(0);
		communist.canvasRenderer.SetAlpha(0);
		peeking.canvasRenderer.SetAlpha(0);
	}

	public void Exuberant(){
		inSpecial = true;
		specialDuration = (float)random.Next((int)emotionDuration.x,(int)emotionDuration.y);
		actionStartTime = Time.fixedTime;
		neutral.canvasRenderer.SetAlpha(0);
		neutral2.canvasRenderer.SetAlpha(0);
		happy.canvasRenderer.SetAlpha(0);
		sad.canvasRenderer.SetAlpha(0);
		victorious.canvasRenderer.SetAlpha(1);
		depressed.canvasRenderer.SetAlpha(0);
		capitalist.canvasRenderer.SetAlpha(0);
		communist.canvasRenderer.SetAlpha(0);
		peeking.canvasRenderer.SetAlpha(0);
	}

	public void Gloomy(){
		inSpecial = true;
		specialDuration = (float)random.Next((int)emotionDuration.x,(int)emotionDuration.y);
		actionStartTime = Time.fixedTime;
		neutral.canvasRenderer.SetAlpha(0);
		neutral2.canvasRenderer.SetAlpha(0);
		happy.canvasRenderer.SetAlpha(0);
		sad.canvasRenderer.SetAlpha(0);
		victorious.canvasRenderer.SetAlpha(0);
		depressed.canvasRenderer.SetAlpha(1);
		capitalist.canvasRenderer.SetAlpha(0);
		communist.canvasRenderer.SetAlpha(0);
		peeking.canvasRenderer.SetAlpha(0);
	}

	public void FreeMarket(){
		inSpecial = true;
		specialDuration = (float)random.Next((int)emotionDuration.x,(int)emotionDuration.y);
		actionStartTime = Time.fixedTime;
		neutral.canvasRenderer.SetAlpha(0);
		neutral2.canvasRenderer.SetAlpha(0);
		happy.canvasRenderer.SetAlpha(0);
		sad.canvasRenderer.SetAlpha(0);
		victorious.canvasRenderer.SetAlpha(0);
		depressed.canvasRenderer.SetAlpha(0);
		capitalist.canvasRenderer.SetAlpha(1);
		communist.canvasRenderer.SetAlpha(0);
		peeking.canvasRenderer.SetAlpha(0);
	}

	public void ForThePeople(){
		inSpecial = true;
		specialDuration = (float)random.Next((int)emotionDuration.x,(int)emotionDuration.y);
		actionStartTime = Time.fixedTime;
		neutral.canvasRenderer.SetAlpha(0);
		neutral2.canvasRenderer.SetAlpha(0);
		happy.canvasRenderer.SetAlpha(0);
		sad.canvasRenderer.SetAlpha(0);
		victorious.canvasRenderer.SetAlpha(0);
		depressed.canvasRenderer.SetAlpha(0);
		capitalist.canvasRenderer.SetAlpha(0);
		communist.canvasRenderer.SetAlpha(1);
		peeking.canvasRenderer.SetAlpha(0);
	}

	void DoAlternate(int i){ //neutral2, happy, sad, really happy, really sad, capitalist, communist, peeking
		if(Time.fixedTime - alternateTime > alternateDuration){
			if(isAlternate){
				neutral.canvasRenderer.SetAlpha(1);
				neutral2.canvasRenderer.SetAlpha(0);
				happy.canvasRenderer.SetAlpha(0);
				sad.canvasRenderer.SetAlpha(0);
				victorious.canvasRenderer.SetAlpha(0);
				depressed.canvasRenderer.SetAlpha(0);
				capitalist.canvasRenderer.SetAlpha(0);
				communist.canvasRenderer.SetAlpha(0);
				peeking.canvasRenderer.SetAlpha(0);
				isAlternate = false;
			}
			else{
				switch(i){
					case 1:
						neutral.canvasRenderer.SetAlpha(0);
						neutral2.canvasRenderer.SetAlpha(1);
						happy.canvasRenderer.SetAlpha(0);
						sad.canvasRenderer.SetAlpha(0);
						victorious.canvasRenderer.SetAlpha(0);
						depressed.canvasRenderer.SetAlpha(0);
						capitalist.canvasRenderer.SetAlpha(0);
						communist.canvasRenderer.SetAlpha(0);
						peeking.canvasRenderer.SetAlpha(0);
						break;
					case 2:
						neutral.canvasRenderer.SetAlpha(0);
						neutral2.canvasRenderer.SetAlpha(0);
						happy.canvasRenderer.SetAlpha(1);
						sad.canvasRenderer.SetAlpha(0);
						victorious.canvasRenderer.SetAlpha(0);
						depressed.canvasRenderer.SetAlpha(0);
						capitalist.canvasRenderer.SetAlpha(0);
						communist.canvasRenderer.SetAlpha(0);
						peeking.canvasRenderer.SetAlpha(0);
						break;
					case 3:
						neutral.canvasRenderer.SetAlpha(0);
						neutral2.canvasRenderer.SetAlpha(0);
						happy.canvasRenderer.SetAlpha(0);
						sad.canvasRenderer.SetAlpha(1);
						victorious.canvasRenderer.SetAlpha(0);
						depressed.canvasRenderer.SetAlpha(0);
						capitalist.canvasRenderer.SetAlpha(0);
						communist.canvasRenderer.SetAlpha(0);
						peeking.canvasRenderer.SetAlpha(0);
						break;
					case 4:
						neutral.canvasRenderer.SetAlpha(0);
						neutral2.canvasRenderer.SetAlpha(0);
						happy.canvasRenderer.SetAlpha(0);
						sad.canvasRenderer.SetAlpha(0);
						victorious.canvasRenderer.SetAlpha(1);
						depressed.canvasRenderer.SetAlpha(0);
						capitalist.canvasRenderer.SetAlpha(0);
						communist.canvasRenderer.SetAlpha(0);
						peeking.canvasRenderer.SetAlpha(0);
						break;
					case 5:
						neutral.canvasRenderer.SetAlpha(0);
						neutral2.canvasRenderer.SetAlpha(0);
						happy.canvasRenderer.SetAlpha(0);
						sad.canvasRenderer.SetAlpha(0);
						victorious.canvasRenderer.SetAlpha(0);
						depressed.canvasRenderer.SetAlpha(1);
						capitalist.canvasRenderer.SetAlpha(0);
						communist.canvasRenderer.SetAlpha(0);
						peeking.canvasRenderer.SetAlpha(0);
						break;
					case 6:
						neutral.canvasRenderer.SetAlpha(0);
						neutral2.canvasRenderer.SetAlpha(0);
						happy.canvasRenderer.SetAlpha(0);
						sad.canvasRenderer.SetAlpha(0);
						victorious.canvasRenderer.SetAlpha(0);
						depressed.canvasRenderer.SetAlpha(0);
						capitalist.canvasRenderer.SetAlpha(1);
						communist.canvasRenderer.SetAlpha(0);
						peeking.canvasRenderer.SetAlpha(0);
						break;
					case 7:
						neutral.canvasRenderer.SetAlpha(0);
						neutral2.canvasRenderer.SetAlpha(0);
						happy.canvasRenderer.SetAlpha(0);
						sad.canvasRenderer.SetAlpha(0);
						victorious.canvasRenderer.SetAlpha(0);
						depressed.canvasRenderer.SetAlpha(0);
						capitalist.canvasRenderer.SetAlpha(0);
						communist.canvasRenderer.SetAlpha(1);
						peeking.canvasRenderer.SetAlpha(0);
						break;
					case 8:
						neutral.canvasRenderer.SetAlpha(0);
						neutral2.canvasRenderer.SetAlpha(0);
						happy.canvasRenderer.SetAlpha(0);
						sad.canvasRenderer.SetAlpha(0);
						victorious.canvasRenderer.SetAlpha(0);
						depressed.canvasRenderer.SetAlpha(0);
						capitalist.canvasRenderer.SetAlpha(0);
						communist.canvasRenderer.SetAlpha(0);
						peeking.canvasRenderer.SetAlpha(1);
						break;
					default:
						neutral.canvasRenderer.SetAlpha(1);
						neutral2.canvasRenderer.SetAlpha(0);
						happy.canvasRenderer.SetAlpha(0);
						sad.canvasRenderer.SetAlpha(0);
						victorious.canvasRenderer.SetAlpha(0);
						depressed.canvasRenderer.SetAlpha(0);
						capitalist.canvasRenderer.SetAlpha(0);
						communist.canvasRenderer.SetAlpha(0);
						peeking.canvasRenderer.SetAlpha(0);
						break;
				}
				isAlternate = true;
			}
			alternateTime = Time.fixedTime;
		}
	}
	
	public void SetSpeaking(int i){
		inSpecial = true;
		specialDuration = (float)random.Next((int)emotionDuration.x,(int)emotionDuration.y);
		actionStartTime = Time.fixedTime;
		alternateTime = actionStartTime;
		DoAlternate(i);
	}
	
	// Update is called once per frame
	/// <summary>
	/// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
	/// </summary>
	void FixedUpdate()
	{
		if(!inSpecial){
			if(Time.fixedTime - actionStartTime > timeToSwitchNeutral){
				specialDuration = (float)random.Next((int)altNeutralDuration.x,(int)altNeutralDuration.y);
				inSpecial = true;
				actionStartTime = Time.fixedTime;
				neutral.canvasRenderer.SetAlpha(0);
				neutral2.canvasRenderer.SetAlpha(1);
				happy.canvasRenderer.SetAlpha(0);
				sad.canvasRenderer.SetAlpha(0);
				victorious.canvasRenderer.SetAlpha(0);
				depressed.canvasRenderer.SetAlpha(0);
				capitalist.canvasRenderer.SetAlpha(0);
				communist.canvasRenderer.SetAlpha(0);
				peeking.canvasRenderer.SetAlpha(0);
			}
		}
		else{
			if(Time.fixedTime - actionStartTime > specialDuration){
				timeToSwitchNeutral = (float)random.Next((int)sampleNeutral.x,(int)sampleNeutral.y);
				inSpecial = false;
				actionStartTime = Time.fixedTime;
				neutral.canvasRenderer.SetAlpha(1);
				neutral2.canvasRenderer.SetAlpha(0);
				happy.canvasRenderer.SetAlpha(0);
				sad.canvasRenderer.SetAlpha(0);
				victorious.canvasRenderer.SetAlpha(0);
				depressed.canvasRenderer.SetAlpha(0);
				capitalist.canvasRenderer.SetAlpha(0);
				communist.canvasRenderer.SetAlpha(0);
				peeking.canvasRenderer.SetAlpha(0);
			}
		}
	} 
}

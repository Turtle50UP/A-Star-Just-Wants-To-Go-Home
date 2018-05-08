using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLevelSelect : AbstractBehavior {
	//to make a commit
	string playerSelect = "playerSelect";
	GameObject selectedConstellation;
	public ConstellationViewManager constellationViewManager;
	public GameObject selectObject;
	public float selectObjectOffset;
	public int minalpha;
	public bool startGameSelected;
	public bool ConstellationSelected{
		get{
			return selectObject != null;
		}
	}
	public Image ybutton;
	public Text difficulty;
	public Text constName;

	public PlayerImageHandler thisplayerImageHandler;
	public PlayerImageHandler otherplayerImageHandler;
	public ConstellationImageManager constellationImageManager;
	public string SelectedConstellationName{
		get{
			if(selectedConstellation != null){
				return selectedConstellation.GetComponent<ConstellationName>().Name;
			}
			else{
				return "NOTHING";
			}
		}
	}
	public bool isReady = false;
	public string CorrespondingCVMDifficulty{
		get{
			foreach(ConstellationManager cm in constellationViewManager.constellationManagers){
				if(cm.name == SelectedConstellationName){
					return cm.constellation.difficulty;
				}
			}
			return "";
		}
	}

	public Vector2 SelectedConstellationLoc{
		get{
			foreach(ConstellationManager cm in constellationViewManager.constellationManagers){
				if(cm.name == SelectedConstellationName){
					return cm.constellation.constellationImage.transform.position;
				}
			}
			return Vector2.zero;
		}
	}

	// Use this for initialization
	void Start () {
		SpriteRenderer sr = selectObject.transform.GetChild(0).GetComponent<SpriteRenderer>();
		Vector4 color = sr.color;
		color.w = (float)minalpha / 255f;
		sr.color = color;
		thisplayerImageHandler.SetCorrectImage("");
		otherplayerImageHandler.SetCorrectImage("");
		ybutton.canvasRenderer.SetAlpha(0);
		difficulty.canvasRenderer.SetAlpha(0);
		constName.text = "";
	}
	
	// Update is called once per frame

	public void Deselect(){
		selectedConstellation.GetComponent<TrophySelect>().isSelected = false;
		selectedConstellation = null;
		//Debug.Log("Deselected");
		SpriteRenderer sr = selectObject.transform.GetChild(0).GetComponent<SpriteRenderer>();
		Vector4 color = sr.color;
		color.w = (float)minalpha/255f;
		sr.color = color;
		thisplayerImageHandler.SetCorrectImage("");
		otherplayerImageHandler.SetCorrectImage("");
		ybutton.canvasRenderer.SetAlpha(0);
		difficulty.canvasRenderer.SetAlpha(0);
		constName.canvasRenderer.SetAlpha(0);
	}

	void Select(GameObject temp){
		selectedConstellation = temp;
		selectObject.transform.position = new Vector3(
			selectedConstellation.transform.position.x + selectObjectOffset,
			selectedConstellation.transform.position.y + selectObjectOffset,
			selectObject.transform.position.z
		);
		SpriteRenderer sr = selectObject.transform.GetChild(0).GetComponent<SpriteRenderer>();
		Vector4 color = sr.color;
		color.w = 1f;
		sr.color = color;
		temp.GetComponent<TrophySelect>().isSelected = true;
		thisplayerImageHandler.SetCorrectImage(SelectedConstellationName);
		otherplayerImageHandler.SetCorrectImage(SelectedConstellationName);
		ybutton.canvasRenderer.SetAlpha(1);
		difficulty.canvasRenderer.SetAlpha(1);
		constName.canvasRenderer.SetAlpha(1);
		difficulty.text = CorrespondingCVMDifficulty;
		constName.text = SelectedConstellationName;
	}
	void FixedUpdate () {
		if(!gm.startingMenu){
			ybutton.canvasRenderer.SetAlpha(0);
			difficulty.canvasRenderer.SetAlpha(0);
			constName.canvasRenderer.SetAlpha(0);
		}
		if(selectedConstellation != null && inputState.GetButtonValue(inputButtons[1])){
			startGameSelected = true;
			return;
		}
		else{
			startGameSelected = false;
		}
		if(collisionState.colliderStatus[playerSelect]){
			Collider2D[] collmems = collisionState.collidingMembers[playerSelect];
			GameObject temp = GetClosestCollidingMember(collmems);
			if(inputState.GetButtonValue(inputButtons[0])){
				if(inputState.GetButtonHoldTime(inputButtons[0]) < gm.epsilon){
					if(selectedConstellation == null || selectedConstellation != temp){
						Select(temp);
					}
					else{
						Deselect();
					}
					constellationImageManager.ColorTheseConstellations();
				}
			}
			else{
				if(selectedConstellation == null){
					selectObject.transform.position = new Vector3(
						temp.transform.position.x + selectObjectOffset,
						temp.transform.position.y + selectObjectOffset,
						selectObject.transform.position.z
					);
				}
			}
		}
	}
}

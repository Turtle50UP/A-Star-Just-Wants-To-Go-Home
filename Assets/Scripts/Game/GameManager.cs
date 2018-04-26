using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public Text winloss1;
	public Text winloss2;
	public float winlossDuration;
	public float winlossStartTime;
	public bool inTutorial;
	public TutorialManager tutorialManager;
	//public bool inStart;
	public Image p1Title;
	public Image p2Title;
	public AudioArrayHandler audioArray;
	public AudioClip lose;
	public AudioClip win;
	public PlayerExpressionManager p1pem;
	public PlayerExpressionManager p2pem;
    public InputManager im1;
	public InputManager im2;
    public PhysicsManager pm;
	public GameObject player1;
	public GameObject player2;
	public GameObject tutorial;
	public Text p1Timer;
	public Text p2Timer;
	public bool showGizmos;
	public float teleRadius;
	public float viewRadius;
	public float skyRadius;
	public Vector2 viewloc;
	public Vector2 p2viewloc;
	public Vector2 patternloc;
	public Vector2 tutorialLoc;
	public float tutorialrad;
	public Vector2 tutorialMoveLoc;
	public float tutorialMoverad;
	public Vector2 tutorialHintLoc;
	public float tutorialHintrad;
	public float offsetForSplitscreen;
	public Vector2 startingDim;
	public Vector2 p1startloc;
	public Vector2 p2startloc;
	public bool startingMenu;
	public bool isViewingMinimapP1;
	public bool isViewingMinimapP2;
	public bool debugMode;
	public Vector2 p1playloc;
	public Vector2 p2playloc;
	public bool inPlay;
	public bool finalState;
	public float timeLimit;
	float SecondsTimeLimit{
		get{
			return timeLimit * 60f;
		}
	}
	System.Random random;
	Vector2 SampleUnitCircle{
		get{
			float twopi = 2.0f * Mathf.PI;
			if(random == null){
				random = new System.Random();
			}
			float angle = (float)random.NextDouble();
			angle *= twopi;
			float ux = Mathf.Cos(angle);
			float uy = Mathf.Sin(angle);
			return new Vector2(ux,uy);
		}
	}
	public float spawnRange;
	Vector2 SampleCircle{
		get{
			Vector2 direction = SampleUnitCircle;
			float maxMagnitude = Mathf.Sqrt(Mathf.Abs(spawnRange));
			if(random == null){
				random = new System.Random();
			}
			float magnitude = (float)random.NextDouble();
			magnitude *= maxMagnitude;
			magnitude *= magnitude;
			return direction * magnitude;
		}
	}
	float curtime;
	float remainingTime;
	bool hasFailed = false;
	public string p1SelectedConstellation;
	public string p2SelectedConstellation;
	PlayerLevelSelect p1ls;
	PlayerLevelSelect p2ls;

    public float epsilon = 0.000001f;
	public ConstellationViewManager constellationViewManager;

	public void SetMinimapView(int playernum, bool toSet){
		if(playernum == 1){
			isViewingMinimapP1 = toSet;
		}
		else if(playernum == 2){
			isViewingMinimapP2 = toSet;
		}
	}

	// Use this for initialization
	void Start () {
		p1ls = player1.GetComponent<PlayerLevelSelect>();
		p2ls = player2.GetComponent<PlayerLevelSelect>();
		winloss1.canvasRenderer.SetAlpha(0);
		winloss2.canvasRenderer.SetAlpha(0);
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(inTutorial){
			startingMenu = true;
			inTutorial = false;
			if(tutorialManager.hasFinishedTutorial){
				tutorialManager.hasFinishedTutorial = false;
				inTutorial = false;
				startingMenu = true;
				player1.transform.position = new Vector3(p2startloc.x,
				p2startloc.y,
				player1.transform.position.z);
				player2.transform.position = new Vector3(p2startloc.x,
				p2startloc.y,
				player2.transform.position.z);
			}
		}
		else if(startingMenu){ //Start menu
			p1Timer.canvasRenderer.SetAlpha(0);
			p2Timer.canvasRenderer.SetAlpha(0);
			p1Title.canvasRenderer.SetAlpha(1);
			p2Title.canvasRenderer.SetAlpha(1);
			p1SelectedConstellation = player1.GetComponent<PlayerLevelSelect>().SelectedConstellationName;
			p2SelectedConstellation = player2.GetComponent<PlayerLevelSelect>().SelectedConstellationName;
			if(p1ls.startGameSelected && p2ls.startGameSelected){ //Switching to game
				startingMenu = false;
				inPlay = true;
				//These are player starting locations in the game
				p1playloc = p1ls.SelectedConstellationLoc;
				p2playloc = p2ls.SelectedConstellationLoc;
				Vector2 sample = SampleCircle;
				player1.transform.position = new Vector3(
					p1playloc.x + sample.x,
					p1playloc.y + sample.y,
					player1.transform.position.z
				);
				sample = SampleCircle;
				player2.transform.position = new Vector3(
					p2playloc.x + sample.x,
					p2playloc.y + sample.y,
					player2.transform.position.z
				);
				constellationViewManager.ResetGame();
				//Debug.Log(p1ls.selectObject);
				//Debug.Log(p2ls.selectObject);
				constellationViewManager.SetupScreen(p1ls.CorrespondingCVMDifficulty,p2ls.CorrespondingCVMDifficulty,p1ls.SelectedConstellationName,p2ls.SelectedConstellationName);
				curtime = Time.fixedTime;
				p1Timer.canvasRenderer.SetAlpha(1);
				p2Timer.canvasRenderer.SetAlpha(1);
				p1Title.canvasRenderer.SetAlpha(0);
				p2Title.canvasRenderer.SetAlpha(0);
			}
		}
		else if(inPlay){//Inplay
			remainingTime = Time.fixedTime - curtime;
			//Debug.Log(remainingTime);
			remainingTime = SecondsTimeLimit - remainingTime;
			//Debug.Log(remainingTime);
			bool finishedYet = constellationViewManager.FinishedDrawing();
			//Debug.Log(finishedYet);
			if(Input.GetKey(KeyCode.T)){
				//Debug.Log("Got it");
				if(Input.GetKey(KeyCode.Y)){
					//Debug.Log("Got it");
					remainingTime = -1f;
				}
			}
			if(finishedYet){
				//Debug.Log("WTF");
				finalState = true;
				hasFailed = false;
				inPlay = false;
				winlossStartTime = Time.fixedTime;
				winloss1.canvasRenderer.SetAlpha(1);
				winloss2.canvasRenderer.SetAlpha(1);
			}
			//Check if constellations finished yet
			if(remainingTime < 0){ //Fail state
			//Debug.Log("HTH");
				finalState = true;
				hasFailed = true;
				inPlay = false;
				winlossStartTime = Time.fixedTime;
				winloss1.canvasRenderer.SetAlpha(1);
				winloss2.canvasRenderer.SetAlpha(1);
			}
			float minutes = remainingTime / 60f;
			remainingTime = minutes - (float)((int)minutes);
			int min = (int) (minutes - remainingTime);
			float seconds = remainingTime * 60f;
			remainingTime = seconds - (float)((int)seconds);
			int sec = (int) (seconds - remainingTime);
			int miliseconds = (int) (remainingTime * 1000);
			string secstring = (sec.ToString().Length > 1 ? "" : "0" ) + sec.ToString();
			p1Timer.text = "0" + min.ToString() + ":" + secstring + ":" + miliseconds.ToString();
			p2Timer.text = "0" + min.ToString() + ":" + secstring + ":" + miliseconds.ToString();

		}
		else if(finalState){
			if(Time.fixedTime - winlossStartTime < winlossDuration){
				p1Timer.canvasRenderer.SetAlpha(0);
				p2Timer.canvasRenderer.SetAlpha(0);
				if(hasFailed){
					p1pem.Gloomy();
					p2pem.Gloomy();
					audioArray.PlayAudio(lose);
					winloss1.text = "You Lost!";
					winloss2.text = "You Lost!";
					//YOU LOSE
				}
				else{
					constellationViewManager.UpdateSuccesses();
					p1pem.Exuberant();
					p2pem.Exuberant();
					audioArray.PlayAudio(win);
					winloss1.text = "You Won!";
					winloss2.text = "You Won!";
					//YOU WIN
				}
			}
			else{
				Debug.Log("Resetting");
				player1.transform.position = new Vector3(tutorialMoveLoc.x,
				tutorialMoveLoc.y,
				player1.transform.position.z);
				player2.transform.position = new Vector3(tutorialMoveLoc.x,
				tutorialMoveLoc.y,
				player2.transform.position.z);
				finalState = false;
				startingMenu = false;//false;
				inTutorial = true;//true;
				tutorialManager.hasFinishedTutorial = true;
				tutorial.GetComponent<ConstellationManager>().DespawnConstellation();
				p1ls.Deselect();
				p2ls.Deselect();
				TempTutorialManager ttm1 = player1.GetComponent<TempTutorialManager>();
				TempTutorialManager ttm2 = player2.GetComponent<TempTutorialManager>();
				ttm1.Restart();
				ttm2.Restart();
				winloss1.canvasRenderer.SetAlpha(0);
				winloss2.canvasRenderer.SetAlpha(0);
			}
		}
	}

	/// <summary>
	/// Callback to draw gizmos that are pickable and always drawn.
	/// </summary>
	void OnDrawGizmos()
	{
		if(showGizmos){
			Gizmos.DrawWireSphere(viewloc, skyRadius);
			Gizmos.DrawWireSphere(Vector2.zero, viewRadius);
			Gizmos.DrawWireSphere(patternloc, teleRadius);
			Gizmos.DrawWireCube(p2startloc, startingDim);
			Gizmos.DrawWireSphere(tutorialLoc,tutorialrad);
			Gizmos.DrawWireSphere(tutorialLoc,tutorialrad);
			Gizmos.DrawWireSphere(tutorialMoveLoc,tutorialMoverad);
			Gizmos.DrawWireSphere(tutorialHintLoc,tutorialHintrad);
		}
	}
}

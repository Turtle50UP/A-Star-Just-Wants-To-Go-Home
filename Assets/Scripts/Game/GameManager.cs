using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

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
	float curtime;
	float remainingTime;
	bool hasFailed = false;
	public string p1SelectedConstellation;
	public string p2SelectedConstellation;

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
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(startingMenu){ //Start menu
			p1Timer.canvasRenderer.SetAlpha(0);
			p2Timer.canvasRenderer.SetAlpha(0);
			p1Title.canvasRenderer.SetAlpha(1);
			p2Title.canvasRenderer.SetAlpha(1);
			PlayerLevelSelect p1ls = player1.GetComponent<PlayerLevelSelect>();
			PlayerLevelSelect p2ls = player2.GetComponent<PlayerLevelSelect>();
			p1SelectedConstellation = player1.GetComponent<PlayerLevelSelect>().SelectedConstellationName;
			p2SelectedConstellation = player2.GetComponent<PlayerLevelSelect>().SelectedConstellationName;
			if(p1ls.startGameSelected && p2ls.startGameSelected){ //Switching to game
				startingMenu = false;
				inPlay = true;
				player1.transform.position = new Vector3(
					p1playloc.x,
					p1playloc.y,
					player1.transform.position.z
				);
				player2.transform.position = new Vector3(
					p2playloc.x,
					p2playloc.y,
					player2.transform.position.z
				);
				constellationViewManager.ResetGame();
				Debug.Log(p1ls.selectObject);
				Debug.Log(p2ls.selectObject);
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
			Debug.Log(remainingTime);
			remainingTime = SecondsTimeLimit - remainingTime;
			Debug.Log(remainingTime);
			bool finishedYet = constellationViewManager.FinishedDrawing();
			Debug.Log(finishedYet);
			if(Input.GetKeyDown(KeyCode.T)){
				Debug.Log("Got it");
				if(Input.GetKeyDown(KeyCode.Y)){
					Debug.Log("Got it");
					remainingTime = -1f;
				}
			}
			if(finishedYet){
				Debug.Log("WTF");
				finalState = true;
				hasFailed = false;
				inPlay = false;
			}
			//Check if constellations finished yet
			if(remainingTime < 0){ //Fail state
			Debug.Log("HTH");
				finalState = true;
				hasFailed = true;
				inPlay = false;
			}
			float minutes = remainingTime / 60f;
			remainingTime = minutes - (float)((int)minutes);
			int min = (int) (minutes - remainingTime);
			float seconds = remainingTime * 60f;
			remainingTime = seconds - (float)((int)seconds);
			int sec = (int) (seconds - remainingTime);
			int miliseconds = (int) (remainingTime * 1000);
			p1Timer.text = min.ToString() + ":" + sec.ToString() + ":" + miliseconds.ToString();
			p2Timer.text = min.ToString() + ":" + sec.ToString() + ":" + miliseconds.ToString();

		}
		else if(finalState){
			p1Timer.canvasRenderer.SetAlpha(0);
			p2Timer.canvasRenderer.SetAlpha(0);
			if(hasFailed){
				p1pem.Gloomy();
				p2pem.Gloomy();
				audioArray.PlayAudio(lose);
				//YOU LOSE
			}
			else{
				constellationViewManager.UpdateSuccesses();
				p1pem.Exuberant();
				p2pem.Exuberant();
				audioArray.PlayAudio(win);
				//YOU WIN
			}
			player1.transform.position = new Vector3(p2startloc.x,
			p2startloc.y,
			player1.transform.position.z);
			player2.transform.position = new Vector3(p2startloc.x,
			p2startloc.y,
			player2.transform.position.z);
			finalState = false;
			startingMenu = true;
			tutorial.GetComponent<ConstellationManager>().DespawnConstellation();
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
			//Gizmos.DrawWireCube(Vector2.zero, new Vector2(1.0f,1.0f));
		}
	}
}

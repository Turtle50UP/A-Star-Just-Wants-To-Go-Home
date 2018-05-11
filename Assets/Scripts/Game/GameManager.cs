using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public Text winloss;
	public float winlossDuration;
	public float winlossStartTime;
	public bool inTutorial;
	public Image pTitle;
	public AudioArrayHandler audioArray;
	public AudioClip lose;
	public AudioClip win;
	public GameObject player;
	public GameObject tutorial;
	public Text pTimer;
	public bool showGizmos;
	public float teleRadius;
	public float viewRadius;
	public float skyRadius;
	public Vector2 viewloc;
	public Vector2 patternloc;
	public Vector2 tutorialLoc;
	public float tutorialrad;
	public Vector2 tutorialMoveLoc;
	public float tutorialMoverad;
	public Vector2 tutorialHintLoc;
	public float tutorialHintrad;
	public Vector2 startingDim;
	public Vector2 startloc;
	public bool startingMenu;
	public bool isViewingMinimap;
	public Vector2 playloc;
	public bool inPlay;
	public bool finalState;
	public float timeLimit;
	public float constellationScore;
	float curTimeScore;
	float currentScore;
	float highScore;
	public float timeScore;
	public float easyScore;
	public float mediumScore;
	public float hardScore;
	public float insaneScore;
	public Text curScoreText;
	public Text highScoreText;
	bool finishPlayed = false;
	public AllConstellationsManager acm;
	public float timeOffset;
	public float deltaTimeOffset;
	public Color victoryColor;
	public EasterEggManager eem;
	Color genericColor;
	float SecondsTimeLimit{
		get{
			return (timeLimit + timeOffset) * 60f;
		}
	}
	float OriginalTimeLimit{
		get{
			return (timeLimit) * 60f;
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
	public string selectedConstellation;

    public float epsilon = 0.000001f;
	public ConstellationViewManager constellationViewManager;
	public PlayerManager playerManager;
	bool startingMenuStarted = false;
	public Image telescopeFilter;
	public ConstellationImageManager constellationImageManager;

	public float GetDifficultyScore(string difficulty){
		switch(difficulty){
			case "Easy":
				return easyScore;
			case "Medium":
				return mediumScore;
			case "Hard":
				return hardScore;
			case "Insane":
				return insaneScore;
			default:
				return 0;
		}
	}

	public float GetTimerMultiplier(string difficulty){
		switch(difficulty){
			case "Easy":
				return 1.0f;
			case "Medium":
				return 1.5f;
			case "Hard":
				return 2.0f;
			case "Insane":
				return 2.5f;
			default:
				return 0;
		}
	}

	public void SetMinimapView(int playernum, bool toSet){
		if(playernum == 1){
			isViewingMinimap = toSet;
		}
	}

	// Use this for initialization
	void Start () {
		genericColor = curScoreText.color;
		curTimeScore = 0;
		constellationScore = 0;
		highScore = 0;
		highScoreText.text = ((int)highScore).ToString();
		currentScore = 0;
		curScoreText.text = ((int)currentScore).ToString();
		winloss.canvasRenderer.SetAlpha(0);		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (Input.GetKey(KeyCode.Delete))
            Application.Quit();
		if(Input.GetKeyDown(KeyCode.Escape)){
			timeOffset = -100;
		}
		if(inTutorial){
			inTutorial = false;
			startingMenu = true;
			player.transform.position = new Vector3(startloc.x,
			startloc.y,
			player.transform.position.z);
		}
		else if(startingMenu){ //Start menu
			if(!startingMenuStarted){
				curScoreText.text = "0";
				telescopeFilter.canvasRenderer.SetAlpha(0);
				pTimer.canvasRenderer.SetAlpha(0);
				pTitle.canvasRenderer.SetAlpha(1);
				startingMenuStarted = true;
			}
			selectedConstellation = playerManager.playerLevelSelect.SelectedConstellationName;
			if(playerManager.playerLevelSelect.startGameSelected){ //Switching to game
				playerManager.playerDrawLine.ResetEdge();
				startingMenuStarted = false;
				startingMenu = false;
				inPlay = true;
				playloc = playerManager.playerLevelSelect.SelectedConstellationLoc;
				Vector2 sample = SampleCircle;
				player.transform.position = new Vector3(
					playloc.x + sample.x,
					playloc.y + sample.y,
					player.transform.position.z
				);
				constellationViewManager.ResetGame();
				constellationViewManager.SetupScreen(playerManager.playerLevelSelect.CorrespondingCVMDifficulty,playerManager.playerLevelSelect.SelectedConstellationName);
				curtime = Time.fixedTime;
				pTimer.canvasRenderer.SetAlpha(1);
				pTitle.canvasRenderer.SetAlpha(0);
				telescopeFilter.canvasRenderer.SetAlpha(1);
			}
		}
		else if(inPlay){//Inplay
			if(playerManager.playerViewHints.viewingHints){
				telescopeFilter.canvasRenderer.SetAlpha(0);
			}
			else{
				if(playerManager.playerMove.viewingMinimap){
					telescopeFilter.canvasRenderer.SetAlpha(0);
				}
				else{
					telescopeFilter.canvasRenderer.SetAlpha(1);
				}
			}
			remainingTime = Time.fixedTime - curtime;
			remainingTime = SecondsTimeLimit - remainingTime;
			curTimeScore = (remainingTime / OriginalTimeLimit) * timeScore;
			//bool finishedYet = constellationViewManager.FinishedDrawing();
			bool finishedYet = acm.CheckForUncompleted() == null;
			constellationImageManager.ColorTheseConstellations();
			if(finishedYet){
				finalState = true;
				hasFailed = false;
				inPlay = false;
				winlossStartTime = Time.fixedTime;
				winloss.canvasRenderer.SetAlpha(1);
			}
			if(remainingTime < 0){ //Fail state
				finalState = true;
				hasFailed = currentScore < 1000000;
				inPlay = false;
				winlossStartTime = Time.fixedTime;
				winloss.canvasRenderer.SetAlpha(1);
			}
			float minutes = remainingTime / 60f;
			remainingTime = minutes - (float)((int)minutes);
			int min = (int) (minutes - remainingTime);
			float seconds = remainingTime * 60f;
			remainingTime = seconds - (float)((int)seconds);
			int sec = (int) (seconds - remainingTime);
			int miliseconds = (int) (remainingTime * 1000);
			string secstring = (sec.ToString().Length > 1 ? "" : "0" ) + sec.ToString();
			pTimer.text = "0" + min.ToString() + ":" + secstring + ":" + miliseconds.ToString();
			constellationScore = acm.GetAllConstellationScores();
			Debug.Log(constellationScore);
			currentScore = constellationScore + curTimeScore;
			curScoreText.text = ((int)currentScore).ToString();
			if(currentScore > 1000000){
				curScoreText.color = victoryColor;
			}
			else{
				curScoreText.color = genericColor;
			}

		}
		else if(finalState){
			if(currentScore > highScore){
				highScore = currentScore;
				highScoreText.text = ((int)highScore).ToString();
			}
			currentScore = 0;
			constellationScore = 0;
			curTimeScore = 0;
			telescopeFilter.canvasRenderer.SetAlpha(0);
			if(Time.fixedTime - winlossStartTime < winlossDuration){
				playerManager.playerDrawLine.ResetEdge();
				if(hasFailed){
					if(!finishPlayed){
						playerManager.playerExpressionManager.Gloomy();
						audioArray.PlayAudio(lose);
						winloss.text = "You Lost!";
						finishPlayed = true;
					}
				}
				else{
					if(!finishPlayed){
						bool finishedYet = acm.CheckForUncompleted() == null;
						constellationViewManager.UpdateSuccesses();
						//if(finishedYet){
						//	eem.PlayEasterEgg();
						//}
						//else{
							playerManager.playerExpressionManager.Exuberant();
						//}
						audioArray.PlayAudio(win);
						winloss.text = "You Won!";
						finishPlayed = true;
					}
				}
			}
			else{
				pTimer.canvasRenderer.SetAlpha(0);
				player.transform.position = new Vector3(tutorialMoveLoc.x,
				tutorialMoveLoc.y,
				player.transform.position.z);
				finalState = false;
				startingMenu = false;//false;
				inTutorial = true;//true;
				finishPlayed = false;
				timeOffset = 0;
				acm.ResetConstellations();
				tutorial.GetComponent<ConstellationManager>().DespawnConstellation();
				playerManager.playerLevelSelect.Deselect();
				TempTutorialManager ttm = player.GetComponent<TempTutorialManager>();
				ttm.Restart();
				winloss.canvasRenderer.SetAlpha(0);
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
			Gizmos.DrawWireCube(startloc, startingDim);
			Gizmos.DrawWireSphere(tutorialLoc,tutorialrad);
			Gizmos.DrawWireSphere(tutorialLoc,tutorialrad);
			Gizmos.DrawWireSphere(tutorialMoveLoc,tutorialMoverad);
			Gizmos.DrawWireSphere(tutorialHintLoc,tutorialHintrad);
		}
	}
}

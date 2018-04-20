using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TutorialManager : MonoBehaviour {
	public bool hasFinishedTutorial;
	public GameManager gameManager;
	public bool inMoveTutorial = true;
	public bool inPlayTutorial;
	public bool canAccessHints;
	public bool canHoldY;
	public bool canPressX; 
	public Text p1Dialogue;
	public Text p2Dialogue;
	string text1 = "Psst, hey, over here!";
	string text2 = "Welcome to Star Crossing!";
	string text3 = "I am the Space-Bananagator, your traveling companion!";
	string text4 = "While traveling the stars, I broke some constellations...";
	string text5 = "Help me put them back together again!";
	string text6 = "Use the directionals to move around, and press B to brake!";
	string text7 = "Good!  Now that you know how to move...";
	string text8 = "I need you to draw Canis Minor for me!";
	string text9 = "Hold Y to see all the stars (and move faster)!";
	string text10 = "Press A to enter the hint mode!";
	string text11 = "Switch between the hint views using LB and RB!";
	string text12 = "Press X to select a star!";
	string text13 = "Press X again on a different star to try to draw a line!";
	string text14 = "Let me make this easier for you.  Here's Monoceros!";
	string text15 = "You finished Canis Minor!";
	string text16 = "Now go, minion, help me finish the others!";

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

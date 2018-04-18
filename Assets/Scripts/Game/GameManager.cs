using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public InputManager im1;
	public InputManager im2;
    public PhysicsManager pm;
	public GameObject player1;
	public GameObject player2;

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

    public float epsilon = 0.000001f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
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
			Gizmos.DrawWireCube(p1startloc, startingDim);
			Gizmos.DrawWireCube(p2startloc, startingDim);
		}
	}
}

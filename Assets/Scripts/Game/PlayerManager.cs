using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

	public GameObject player;
	public Rigidbody2D body2D;
	public PlayerViewHints playerViewHints;
	public PlayerLevelSelect playerLevelSelect;
	public PlayerDrawLine playerDrawLine;
	public CollisionState collisionState;
	public PlayerMove playerMove;
	public GameObject playerRep;
	public PlayerRepMovement playerRepMovement;
	public GameObject playerCamera;
	public GameObject playerEmotions;
	public PlayerExpressionManager playerExpressionManager;

	// Use this for initialization
	void Start () {
		body2D = player.GetComponent<Rigidbody2D>();
		playerViewHints = player.GetComponent<PlayerViewHints>();
		playerLevelSelect = player.GetComponent<PlayerLevelSelect>();
		playerDrawLine = player.GetComponent<PlayerDrawLine>();
		collisionState = player.GetComponent<CollisionState>();
		playerMove = player.GetComponent<PlayerMove>();
		playerRepMovement = playerRep.GetComponent<PlayerRepMovement>();
		playerExpressionManager = playerEmotions.GetComponent<PlayerExpressionManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

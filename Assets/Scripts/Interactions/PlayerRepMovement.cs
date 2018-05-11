using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRepMovement : MonoBehaviour {

	public GameObject player;
	public GameManager gm;
	Vector2 minimapOffset;
	float gameMapScale;
	float minimapScale;
	Vector2 playerLoc;
	// Use this for initialization
	void Start () {
		gameMapScale = gm.viewRadius;
		minimapScale = gm.skyRadius;
		minimapOffset = gm.viewloc;
	}
	
	// Update is called once per frame
	void Update () {
		playerLoc = new Vector2(player.transform.position.x,
								player.transform.position.y);
		float playerMag = playerLoc.magnitude;
		playerMag /= gameMapScale;
		playerMag *= minimapScale;
		playerLoc.Normalize();
		playerLoc *= playerMag;
		playerLoc += minimapOffset;
		this.transform.position = playerLoc;

		Vector2 velocity = player.GetComponent<Rigidbody2D>().velocity;
		GameObject sprite = this.gameObject.transform.GetChild(0).gameObject;
		if(velocity.magnitude > gm.epsilon){

			float angle = Vector2.Angle(new Vector2(1.0f,0f),velocity) + (
						velocity.y > 0 ? - 90f : +90f);
			angle *= velocity.y > 0 ? 1 : -1;
			/*sprite.transform.eulerAngles = new Vector3(
				sprite.transform.eulerAngles.x,
				sprite.transform.eulerAngles.y,
				angle);*/
		}
	}
}

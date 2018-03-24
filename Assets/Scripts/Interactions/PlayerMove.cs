using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : AbstractBehavior {

    public /*static*/ /*const*/ float maxspeed = 10;
    public GameManager gm;

    float epsilon;

    float ShiftedSigmoid(float x, float maxval){
        float shift = -0.5f;
        return Sigmoid(x, maxval, shift);
    }

	// Use this for initialization
	void Start () {
        epsilon = 
            GameObject.Find("GameManager").GetComponent<GameManager>().epsilon;
	}
	
	// Update is called once per frame
    protected virtual void Update () {

        bool[] directionals = new bool[];

        bool isRight = inputState.GetButtonValue(inputButtons[0]);
		bool isLeft = inputState.GetButtonValue(inputButtons[1]);
		bool isUp = inputState.GetButtonValue(inputButtons[2]);
		bool isDown = inputState.GetButtonValue(inputButtons[3]);

        bool isRunning = inputState.GetButtonValue(inputButtons[2]);

        //If on the ground...
        if (true) //collisionState.colliderStatus[standing])
        {
            //Holding either left or right directional
            if (isRight || isLeft)
            {

                //Apply appropriate speed to character.
                float tempSpeed = speed;
                if (isRunning && runMultiplier > 0)
                {
                    tempSpeed *= runMultiplier;
                }
                //direction = (float)inputState.direction;
                float velx = tempSpeed * direction;

                body2d.velocity = new Vector2(velx, body2d.velocity.y);
            }

            //On ground, no directional
            else{

                //If standing,
                if(inputState.absVelX < epsilon){
                    
                }

                //Else calculate and apply drag
                //Quadratic drag: -kv^2
                else{
                    float velx = body2d.velocity.x;
                    velx = velx - ((1 / drag) * (velx * velx)*
                        (velx > 0 ? 1 : -1));
                    if ((int)Mathf.Sign(velx) != 
                        (int)Mathf.Sign(body2d.velocity.x)){
                        velx = 0;
                    }
                    body2d.velocity = new Vector2(velx, body2d.velocity.y);
                }
            }
        }

        //Else in the air...  Split this off into its own class`
	}
}

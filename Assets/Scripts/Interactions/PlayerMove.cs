/* Contains a class for utilizing recyclable game objects
 */

using UnityEngine;

public class PlayerMove : AbstractBehavior {

    public /*static*/ /*const*/ float maxspeed = 10f;
    public float maxaccel = 1f;
    public /*static*/ /*const*/ float boostMultiplier = 2.0f;
    public float sigmoidStretch = 1f;
    public float breakAccel = 2f;
    Vector2 movementTimes;

    public float heldUpperBound = 4;

    float epsilon;

	// Use this for initialization
	void Start () {
        epsilon = 
            GameObject.Find("GameManager").GetComponent<GameManager>().epsilon;
        movementTimes = Vector2.zero;
	}

    float UpdateMovementTime(int posIndex, int negIndex){
        bool posDir = inputState.GetButtonValue(inputButtons[posIndex]);
        bool negDir = inputState.GetButtonValue(inputButtons[negIndex]);

        float posHeld = inputState.GetButtonHoldTime(inputButtons[posIndex]);
        float negHeld = inputState.GetButtonHoldTime(inputButtons[negIndex]);

        float axisHolding;
        if(posDir ^ negDir){
            axisHolding = posDir ? 1f : -1f;
        }
        else{
            axisHolding = 0.0f;
            inputState.ResetHoldTime(inputButtons[posIndex]);
            inputState.ResetHoldTime(inputButtons[negIndex]);
        }

        float curmovementtime = (posHeld + negHeld) * axisHolding;
        return curmovementtime;
    }
	
	// Update is called once per frame
    protected virtual void FixedUpdate () {
		bool isBoosting = inputState.GetButtonValue(inputButtons[4]);
        bool isBreaking = inputState.GetButtonValue(inputButtons[5]);
        Vector2 velocity = body2d.velocity;
        float tempMaxSpeed = maxspeed * (isBoosting ? boostMultiplier : 1);
        float tempMaxAccel = maxaccel * (isBoosting ? boostMultiplier : 1);
        Vector2 accel = Vector2.zero;
        GameObject sprite = this.gameObject.transform.GetChild(0).gameObject;

        if(isBreaking){
            Debug.Log("breaking");
            Vector2 unit = velocity.normalized;
            unit *= -breakAccel;
            float velmag = velocity.magnitude;
            if(velmag < breakAccel){
                body2d.velocity = Vector2.zero;
            }
            else{
                body2d.velocity += unit;
            }
        }
        else{

            accel = new Vector2(UpdateMovementTime(0,1),
                                UpdateMovementTime(2,3));
            accel = new Vector2(fl.TwoWaySigmoid(accel.x,sigmoidStretch),
                                fl.TwoWaySigmoid(accel.y,sigmoidStretch));
            accel *= tempMaxAccel;
            velocity += accel * Time.fixedDeltaTime;
            if(velocity.magnitude > tempMaxSpeed){
                velocity = velocity.normalized * tempMaxSpeed;
            }
            body2d.velocity = velocity;
        }

        if(body2d.velocity.magnitude > epsilon){
            float angle = Vector2.Angle(new Vector2(1.0f,0f),velocity) + (
                    velocity.y > 0 ? - 90f : +90f);
            angle *= velocity.y > 0 ? 1 : -1;
            sprite.transform.eulerAngles = new Vector3(
                sprite.transform.eulerAngles.x,
                sprite.transform.eulerAngles.y,
                angle);
        }
    }
}

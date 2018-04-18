/* Contains a class for utilizing recyclable game objects
 */

using UnityEngine;

public class PlayerMove : AbstractBehavior {

    public /*static*/ /*const*/ float maxspeed = 10f;
    public float maxaccel = 1f;
    public /*static*/ /*const*/ float boostMultiplier = 2.0f;
    public float sigmoidStretch = 1f;
    public float breakAccel = 2f;
    public float breakFactor = 2.0f;
    Vector2 movementTimes;

    public float heldUpperBound = 4;
    public int playernum;

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

    float ThreeSign(float input){
        if(input * input < epsilon){
            return 0.0f;
        }
        return Mathf.Sign(input);
    }
	
	// Update is called once per frame
    protected virtual void FixedUpdate () {
		bool isBoosting = inputState.GetButtonValue(inputButtons[4]);
        gm.SetMinimapView(playernum,isBoosting);
        bool isBreaking = inputState.GetButtonValue(inputButtons[5]);
        Vector2 velocity = body2d.velocity;
        float tempMaxSpeed = maxspeed * (isBoosting ? boostMultiplier : 1);
        float tempMaxAccel = maxaccel * (isBoosting ? boostMultiplier : 1);
        Vector2 accel = Vector2.zero;
        GameObject sprite = this.gameObject.transform.GetChild(0).gameObject;

        accel = new Vector2(UpdateMovementTime(0,1),
                            UpdateMovementTime(2,3));
        accel = new Vector2(fl.TwoWaySigmoid(accel.x,sigmoidStretch),
                            fl.TwoWaySigmoid(accel.y,sigmoidStretch));
        float amag = accel.magnitude;

        if(amag < epsilon){
            Debug.Log("breaking");
            Vector2 unit = velocity.normalized;
            unit *= -breakAccel * (isBreaking ? breakFactor : 1.0f);
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
            accel = new Vector2(ThreeSign(accel.x),ThreeSign(accel.y));
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
        if(!gm.startingMenu){
            float mag = this.transform.position.magnitude;
            if(mag > gm.viewRadius){
                Vector2 temp = new Vector2(this.transform.position.x,
                                            this.transform.position.y);
                temp.Normalize();
                temp *= gm.viewRadius;
                this.transform.position = temp;
            }
        }
    }
}

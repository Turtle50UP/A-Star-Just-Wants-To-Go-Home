using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* ButtonState:
 * Handles the state of a button, both the boolean of the button press
 * and the duration of holding of the button.
 */
public class ButtonState{
    public bool val;
    public float holdTime = 0f;
}

/* Directions:
 * Ints to represent anything directional.
 */
public enum Directions
{
    Right = 1,
    Left = -1,
}

/* InputState:
 */
public class InputState : MonoBehaviour {
    
	public Directions direction = Directions.Right;
	private Dictionary<Buttons, ButtonState> buttonStates =
        new Dictionary<Buttons, ButtonState>();

    /*
     * For button key, sets true if pressed, false if not
     */
    public void SetButtonValue(Buttons key, bool val){

        if (!buttonStates.ContainsKey(key))
        {
            buttonStates.Add(key, new ButtonState());
        }

        ButtonState state = buttonStates[key];

        if(state.val && !val)
        {
            state.holdTime = 0;
        }

        else if(state.val && val)
        {
            state.holdTime += Time.deltaTime;
		}

        state.val = val;
    }

    /*
     * Gets bool for pressed
     */
    public bool GetButtonValue(Buttons key){
        
        if (buttonStates.ContainsKey(key))
        {
            return buttonStates[key].val;
        }

        else
        {
            return false;
        }
    }

    /*
     * Gets length of button pressed
     */
    public float GetButtonHoldTime(Buttons key)
	{
        if (buttonStates.ContainsKey(key))
        {
            return buttonStates[key].holdTime;
        }

        else 
        { 
            return 0;
        }
	}
    
    public void ResetHoldTime(Buttons key)
    {
        if(buttonStates.ContainsKey(key))
        {
            buttonStates[key].holdTime = 0f;
        }
    }

    public void SetHoldTime(Buttons key,float newHoldTime)
    {
        if(buttonStates.ContainsKey(key) && newHoldTime > 0)
        {
            buttonStates[key].holdTime = newHoldTime;
        }
    }
}

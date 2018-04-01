using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Buttons{
    Right,
    Left,
    Up,
    Down,
    A,
    B,
    X,
    Y,
    L,
    R,
    Select,
    Start
}

public enum Cond
{
    Greater,
    Less,
    Equals
}

/*
 * InputAxisState: Class for containing information for a particular
 * button axis.
 */
[System.Serializable]
public class InputAxisState{
    public string axisName;
    public float offValue;
    public Buttons button;
    public Cond condition;

    public bool value{
        get{
            float axisVal = Input.GetAxis(axisName);  //Issue with input, release does not go right to zero
            bool leftpressed = Input.GetKey(KeyCode.A);
            bool rightpressed = Input.GetKey(KeyCode.D);
            switch (condition)
            {
                case Cond.Greater:
                    return axisVal > offValue;
                case Cond.Less:
                    return axisVal < offValue;
                case Cond.Equals:
                    return axisVal == offValue;
            }
            return false;
        }
    }
}

public class InputManager : MonoBehaviour {

    public InputAxisState[] inputs;
    public InputState inputState;

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void FixedUpdate()
    {
        foreach (InputAxisState input in inputs)
        {
            inputState.SetButtonValue(input.button, input.value);
        }
    }
}

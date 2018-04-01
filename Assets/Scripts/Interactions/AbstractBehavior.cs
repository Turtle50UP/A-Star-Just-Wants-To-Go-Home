using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractBehavior : MonoBehaviour {

    public Buttons[] inputButtons;
    protected InputState inputState;
    protected Rigidbody2D body2d;
    protected CollisionState collisionState;
    protected PhysicsManager pm;
    protected GameManager gm;
    protected FunctionLib fl;

    protected virtual void Awake()
    {
        inputState = GetComponent<InputState>();
        body2d = GetComponent<Rigidbody2D>();
		collisionState = GetComponent<CollisionState>();
        pm = GetComponent<PhysicsManager>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        fl = new FunctionLib();
	}

    protected float GetAbsX()
    {
        return Mathf.Abs(body2d.velocity.x);
    }

	protected float GetAbsY()
	{
		return Mathf.Abs(body2d.velocity.y);
	}
}

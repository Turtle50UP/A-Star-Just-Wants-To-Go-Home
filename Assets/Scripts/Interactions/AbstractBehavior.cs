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

    protected GameObject GetClosestCollidingMember(Collider2D[] collmems){
		float[] dists = new float[collmems.Length];
		for(int i = 0; i < collmems.Length; i++){
			Vector2 collloc = collmems[i].gameObject.transform.position;
			collloc -= (Vector2)this.transform.position;
			dists[i] = collloc.magnitude;
		}
		int mindex = 0;
		float min = float.PositiveInfinity;
		for(int i = 0; i < dists.Length; i++){
			if(min > dists[i]){
				min = dists[i];
				mindex = i;
			}
		}
		return collmems[mindex].gameObject;
	}

	protected float GetAbsY()
	{
		return Mathf.Abs(body2d.velocity.y);
	}
}

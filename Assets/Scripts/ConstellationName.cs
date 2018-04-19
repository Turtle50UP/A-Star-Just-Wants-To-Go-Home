using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstellationName : MonoBehaviour {

	public string Name
	{
		get
		{
			return this.gameObject.name.Remove(this.gameObject.name.Length - 6);
		}
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

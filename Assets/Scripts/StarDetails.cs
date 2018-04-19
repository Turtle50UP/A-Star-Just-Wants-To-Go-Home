using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarDetails : MonoBehaviour {

	public string groupName;
	public bool alreadySelected = false;
	public int index;
	public GraphVertex thisgv;
	public ConstellationManager cm;


	public void SetIndex(int index){
		this.index = index;
	}

	public void SetGV(GraphVertex gv){
		thisgv = gv;
	}

	public void SetCM(ConstellationManager constellationManager){
		cm = constellationManager;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleSpawner : AbstractSpawner {

	// Use this for initialization
	void Start () {
		
	}

	public GameObject Spawn(){
		if(spawnedList.Count == 0){
			return Spawn(prefabs[0].name);
		}
		else{
			return null;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

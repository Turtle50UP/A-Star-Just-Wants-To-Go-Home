using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PrefabContainer{
	public string name;
	public GameObject prefab;
}

public class GameObjectContainer{
	public string name;
	public GameObject gameObject;
	public GameObjectContainer(string name, GameObject go){
		this.name = name;
		this.gameObject = go;
	}
}

public class AbstractSpawner : MonoBehaviour {
	protected List<GameObjectContainer> spawnedList;
	public PrefabContainer[] prefabs;

	// Use this for initialization
	protected virtual void Awake () {
		if(spawnedList == null){
			spawnedList = new List<GameObjectContainer>();
		}

	}

	public GameObject Spawn(string name, Vector3 position){
		GameObject toSpawn = null;
		foreach(PrefabContainer prefab in prefabs){
			if(string.Compare(name, prefab.name) == 0){
				toSpawn = prefab.prefab;
				//Debug.Log("Spawned!");
			}
		}
		if(toSpawn != null){
			GameObject go = GameObjUtil.Instantiate(toSpawn, position);
			GameObjectContainer goc = new GameObjectContainer(name,go);
        	spawnedList.Add(goc);
			return go;
		}   
		//Debug.Log("Not Spawned!");
		return null;
	}

	public GameObject Spawn(string name){
        return Spawn(name, this.transform.position);
    }

	public void Despawn(){
		while(spawnedList.Count > 0){
			GameObjectContainer toDespawn = spawnedList[0];
			spawnedList.RemoveAt(0);
			GameObjUtil.Destroy(toDespawn.gameObject);
		}
	}

	public void Despawn(string name){
		for(int i = 0; i < spawnedList.Count; i++){
			GameObjectContainer goc = spawnedList[i];
			if(string.Compare(goc.name,name) == 0){
				spawnedList.RemoveAt(i);
				GameObjUtil.Destroy(goc.gameObject);
				i--;
			}
		}
	}

	public void Despawn(GameObject go){
		int removeIndex = -1;
		for(int i = 0; i < spawnedList.Count; i++){
			GameObjectContainer goc = spawnedList[i];
			if(go == goc.gameObject){
				removeIndex = i;
			}
		}
		if(removeIndex >= 0){
			GameObjectContainer toRemove = spawnedList[removeIndex];
			spawnedList.RemoveAt(removeIndex);
			GameObjUtil.Destroy(toRemove.gameObject);
		}
	}
}

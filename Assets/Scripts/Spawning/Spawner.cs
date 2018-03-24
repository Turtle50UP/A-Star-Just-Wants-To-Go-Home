/* Contains the class required for spawning and despawning instances.
 * 
 * Can be used as a universal spawner, but should be used separately for
 * different groups of prefabs.
 */

using UnityEngine;
using System.Collections.Generic;

/* Spawner: Class that contains methods for spawning individual and n many
 * instances of that object, either at the spawner location or 
 * a different location specified, and despawns all instances of a given
 * prefab (or all prefabs).
 * Contains a public array of prefabs and a private array of lists
 * of gameobjects containing all instances of the spawned object.
 */
public class Spawner : MonoBehaviour {
	public GameObject[] prefabs;
    private List<GameObject>[] spawnedList;

    /* Start: Initializes structures used in later spawn/despawn methods
     */
    private void Start()
    {
        spawnedList = new List<GameObject>[prefabs.Length];
        for (int i = 0; i < spawnedList.Length; i++){
            spawnedList[i] = new List<GameObject>();
        }
    }

    /* SpawnAtLoc: Spawns an object at the requested location
     */
    public void SpawnAtLoc(int i, Vector3 position){
        GameObject go = GameObjUtil.Instantiate(prefabs[i], position);
        spawnedList[i].Add(go);
	}

    /* Spawn: Spawns at the spawner location
     */
    public void Spawn(int i){
        SpawnAtLoc(i,this.transform.position);
    }

    /* SpawnN: Spawns n at the spawner location
     */
	public void SpawnN(int i, int n)
	{
		for (int j = 0; j < n; j++)
		{
			Spawn(i);
		}
	}

    /* SpawnNAtLoc: Spawns n at the requested location
     */
    public void SpawnNAtLoc(int i, int n, Vector3 position){
        for (int j = 0; j < n; j++){
            SpawnAtLoc(i,position);
        }
    }

    /* Despawn: Despawns all of a particular prefab
     */
	public void Despawn(int i){
        foreach(GameObject go in spawnedList[i]){
            GameObjUtil.Destroy(go);
        }
	}

    /* DespawnAll: Despawns all prefabs controlled by this spawner.
     */
	public void DespawnAll(){
        for (int i = 0; i < spawnedList.Length; i++){
            Despawn(i);
        }
	}
}

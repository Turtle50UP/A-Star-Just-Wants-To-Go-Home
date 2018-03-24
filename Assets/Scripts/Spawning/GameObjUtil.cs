/* Contains a class for utilizing recyclable game objects
 */

using UnityEngine;
using System.Collections.Generic;

/* GameObjUtil: Adjusts the Destroy and Instantiate methods for creating
 * GameObjects.  Specifically it checks for the object having
 * a recyclable game object script and adjusting Destroy and Instantiate
 * for the requested object.
 */
public class GameObjUtil {

	private static Dictionary<RecycleGameObj, ObjPool> pools = new Dictionary<RecycleGameObj,ObjPool> ();

    /* Instantiate: If it detects a recycle script on the object requested,
     * it finds the correct object pool for that requested prefab
     * and requests a new game object from the object pool (regardless of status
     * of the status of the object pool).
     * Otherwise it just instantiates like in GameObject.
     */
	public static GameObject Instantiate(GameObject prefab, Vector3 pos){
		GameObject instance = null;
		RecycleGameObj recycledScript = prefab.GetComponent<RecycleGameObj> ();
		if (recycledScript != null) {
			ObjPool pool = GetObjPool (recycledScript);
			instance = pool.NextObject (pos).gameObject;
		} else {
			instance = GameObject.Instantiate (prefab);
			instance.transform.position = pos;
		}
		return instance;
	}

    /* Destroy: Similarly to Instantiate, it either shuts down the object
     * if the object contains a RecycleGameObject script, else destroys
     * the object like in GameObject.
     */
	public static void Destroy(GameObject gameObject){
		if(gameObject == null)
			return;
		RecycleGameObj recycleGameObject = gameObject.GetComponent<RecycleGameObj> ();
		if (recycleGameObject != null) {
			recycleGameObject.Shutdown ();
		} else {
			GameObject.Destroy (gameObject);
		}
	}

    /* GetObjPool: Gets object pool for particular prefab.  Either gets
     * existing object pool or creates a new object pool for that prefab.
     */
	private static ObjPool GetObjPool(RecycleGameObj reference){
		ObjPool pool = null;
		if (pools.ContainsKey (reference)) {
			pool = pools [reference];
		} else{
			GameObject poolContainer = new GameObject (reference.gameObject.name+"ObjectPool");
			pool = poolContainer.AddComponent<ObjPool> ();
			pool.prefab = reference;
			pools.Add (reference,pool);
		}

		return pool;
	}
}

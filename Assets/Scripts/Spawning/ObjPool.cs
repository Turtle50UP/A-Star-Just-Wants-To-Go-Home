/* A class for the creation and manipulation of an object pool.
 */

using UnityEngine;
using System.Collections.Generic;

/* ObjPool: Creates and handles clones of a prefab that is stored in a convenient
 * list structure called an Object Pool.
 */
public class ObjPool : MonoBehaviour {
	public RecycleGameObj prefab;
	private List<RecycleGameObj> poolInstances = new List<RecycleGameObj> ();

    /* CreateInstance: Instantiates a prefab of the object at the particular
     * location provided.  This specficially creates a prefab clone if the prefab has
     * the recycle script attached.
     */
	private RecycleGameObj CreateInstance(Vector3 pos){
		RecycleGameObj clone = GameObject.Instantiate (prefab);
		clone.transform.position = pos;
		clone.transform.parent = transform;
		poolInstances.Add (clone);
		return clone;
	}

    /* NextObject: Searches object pool for an instance that has been
     * turned off.  If any are, then it restarts the instance and
     * returns that object.  Otherwise it calls the creation of a new
     * instance.
     */
	public RecycleGameObj NextObject(Vector3 pos){
		RecycleGameObj instance = null;
		foreach (RecycleGameObj go in poolInstances) {
			if (!(go.gameObject.activeSelf)) {
				instance = go;
				instance.transform.position = pos;
			}
		}
        if (instance == null)
        {
            instance = CreateInstance(pos);
        }
		instance.Restart ();
		return instance;
	}
}

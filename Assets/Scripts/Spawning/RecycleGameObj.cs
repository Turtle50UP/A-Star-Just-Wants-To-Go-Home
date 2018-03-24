/* Contains class and interface required for making recyclable game
 * objects
 */

using UnityEngine;
using System.Collections.Generic;

public interface IRecycle{
	void Restart();
	void Shutdown();
}

/* RecycleGameObj:
 * A class that allows for recyclable prefabs.
 * Only use on a prefab/something that will be Instantiated and 
 * Destroyed over and over again.  Saves CPU from having to initialize
 * and destroy objects and instead just silences and awakens the object
 */
public class RecycleGameObj : MonoBehaviour {
	private List<IRecycle> recycleComponents;

    /* Initialization of a recyclable object
     */
	void Awake(){
		MonoBehaviour[] components = GetComponents<MonoBehaviour> ();
		recycleComponents = new List<IRecycle> ();
		foreach (MonoBehaviour component in components) {
			if (component is IRecycle)
				recycleComponents.Add (component as IRecycle);
		}
	}

    /* Restart: a function to turn an inactive game object into an
     * active game object
     */
	public void Restart(){
		gameObject.SetActive (true);
		foreach (IRecycle component in recycleComponents)
			component.Restart ();
	}

    /* Shutdown: a function to turn an active game object into an
     * inactive game object
     */
	public void Shutdown(){
		gameObject.SetActive (false);
		foreach (IRecycle component in recycleComponents)
			component.Restart ();
	}
}

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

public class ObjectTracker{
    public float x;
    public float y;
    public string groupName;
    public string prefabName;
    public bool isOn;

    public ObjectTracker(float x, float y, string gn, string pn){
        this.x = x;
        this.y = y;
        this.groupName = gn;
        this.prefabName = pn;
        this.isOn = false;
    }

    public ObjectTracker(string[] stringData){
        if(stringData.Length == 4){
            this.x = float.Parse(stringData[0]);
            this.y = float.Parse(stringData[1]);
            this.groupName = stringData[2];
            this.prefabName = stringData[3];
        }
        else{
            this.x = 0f;
            this.y = 0f;
            this.groupName = "null";
            this.prefabName = "null";
        }
        this.isOn = false;
    }
}

public class Spawner : AbstractSpawner {
    public bool is2D = true;
    public TextAsset coordinates;
    private string coordString;
    private List<ObjectTracker> objects;

    private void ParseCoordinates(){
        string[] coordStrings = coordString.Split('\n');
        foreach(string coordData in coordStrings){
            string[] cData = coordData.Split(',');
            objects.Add(new ObjectTracker(cData));
        }
    }

    /* Start: Initializes structures used in later spawn/despawn methods
     */
    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>

    protected override void Awake(){
        base.Awake();
        objects = new List<ObjectTracker>();
        if(coordinates != null){
            coordString = coordinates.ToString();
        }
        ParseCoordinates();
        Spawn();
    }

    public void Spawn(){
        foreach(ObjectTracker obj in objects){
            Vector3 position = new Vector3(
                obj.x,obj.y,this.transform.position.z);
            Spawn(obj.prefabName,position);
        }
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.T)){
            Spawn();
        }
        if(Input.GetKeyDown(KeyCode.U)){
            Despawn();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CollisionShape
{
    Box,
    Circle
}

public class CollisionState : MonoBehaviour {

    /*
     * ColliderInput: Class, creates a rectangular colliding box for detecting
     * collision/contact with other object
     */
    [System.Serializable]
    public class ColliderInput
    {
        public string name; //Name of this collider
        public CollisionShape shape; //Shape of collider
        public Vector2 dimension; //Dimensions of collider
        private float radius; //
        public Vector2 location; //Center of collider (from world center)
        public float angle = 0f;
        public LayerMask layer; //What coll layer to detect collision
        public string tag = "untagged"; //Tag that collider interacts with
        public bool isColliding; //if isColliding, is colliding and vice versa
        public bool gizmoIsVisible;
        public float timeColliding = -1f; //stores how long something has been colliding for.
        public float timeCollided = 0f; //time of collision
        public Collider2D[] collidingWith = null;
        public Color debugCollisionColor = Color.red; //Gizmo color

        /*
         * IsColliding: Given collider parent location, updates isColliding
         * to reflect if collider is colliding or not.
         */
        public void IsColliding(Vector3 objPosition){
            Vector2 pos = location;
            pos.x += objPosition.x;
            pos.y += objPosition.y;
            switch (shape)
            {
                case CollisionShape.Box:
                    isColliding = Physics2D.OverlapBox(pos, dimension, angle, layer);
                    collidingWith = Physics2D.OverlapBoxAll(pos, dimension, angle, layer);
                    break;
                case CollisionShape.Circle:
                    radius = dimension.magnitude / 2.0f;
                    isColliding = Physics2D.OverlapCircle(pos, radius, layer);
                    collidingWith = Physics2D.OverlapCircleAll(pos, radius, layer);
                    break;
            }

            //Special Tag Collision check
            
            if (string.Compare(tag, "untagged") != 0)
            {
                int temp = 1;
                foreach (Collider2D coll in collidingWith)
                {
                    temp *= string.Compare(coll.gameObject.tag, tag);
                }
                isColliding = temp == 0;
            }
            if(isColliding){
                Debug.Log("IS COLLIDING");
                if (timeColliding < 0f){
                    timeCollided = Time.time;
                }
                timeColliding = Time.time - timeCollided;
            }
            else{
                collidingWith = null;
                timeColliding = -1f;
                timeCollided = -1f;
            }
        }

        //Flips collider across y axis
        public void FlipCollider(float sign)
        {
            location = new Vector2(sign * Mathf.Abs(location.x), location.y);
        }

        /* SetCollider: updates collider location/dimension/layer info
         * from a separate collider
         */
        public void SetCollider(ColliderInput ci)
        {
            dimension = ci.dimension;
            location = ci.location;
            layer = ci.layer;
        }

        /*
         * DrawGizmo: Draws gizmo for collider for parent object at objPosition
         */
        public void DrawGizmo(Vector3 objPosition){
            Vector2 pos = location;
            Gizmos.color = debugCollisionColor;
            pos.x += objPosition.x;
            pos.y += objPosition.y;
            if (gizmoIsVisible)
            {
                switch (shape)
                {
                    case CollisionShape.Box:
                        Gizmos.DrawWireCube(pos, dimension);
                        break;
                    case CollisionShape.Circle:
                        radius = dimension.magnitude / 2.0f;
                        Gizmos.DrawWireSphere(pos, radius);
                        break;
                }
            }
        }

        public void SetRadius(){
            radius = dimension.magnitude;
        }
    }

    //Collider State Dictionary (name to status mapping)
    public Dictionary<string, bool> colliderStatus = new Dictionary<string, bool>();

    public Dictionary<string, Collider2D[]> collidingMembers = new Dictionary<string, Collider2D[]>();

    public Dictionary<string, float> collidingDuration = new Dictionary<string, float>();

    //Collider list (input from inspector)
    public ColliderInput[] colliderInputs;

    //Add collider list into collider state dictionary
    void Start () {
        foreach (ColliderInput input in colliderInputs)
        {
            if (!colliderStatus.ContainsKey(input.name)){
                colliderStatus.Add(input.name, false);
                collidingMembers.Add(input.name, null);
                collidingDuration.Add(input.name, 0f);
            }

        }
    }

    /* GetCollider: Gets collider info for a named collider
     */
    private ColliderInput GetCollider(string collName){
        if (colliderStatus.ContainsKey(collName))
        {
            foreach (ColliderInput input in colliderInputs)
            {
                if (string.Compare(input.name,collName) == 0){
                    return input;
                }
            }
            Debug.Log("THIS IS AN ERROR, COLLIDER EXISTS IN DICTIONARY ONLY");
            return null;
        }
        else{
            return null;
        }
    }

    /* SetCollider: Update existing collider with an existing collider name
     */
    private void SetCollider(ColliderInput ci){
        if (colliderStatus.ContainsKey(ci.name)){
            foreach (ColliderInput input in colliderInputs){
                if(string.Compare(input.name,ci.name) == 0){
                    input.SetCollider(ci);
                }
            }
        }
    }

    /* UpdateCollider: Updates existing collider with input updates
     */
    public void UpdateCollider(string name, Vector2 newDimension, Vector2 newLocation, LayerMask newLayer){
        ColliderInput ci = GetCollider(name);
        ci.dimension = newDimension;
        ci.location = newLocation;
        ci.layer = newLayer;
        SetCollider(ci);
    }

    private void FixedUpdate()
    {
        foreach (ColliderInput input in colliderInputs){
            input.IsColliding(this.transform.position);
            input.FlipCollider(transform.localScale.x);
            colliderStatus[input.name] = input.isColliding;
            collidingMembers[input.name] = input.collidingWith;
            collidingDuration[input.name] = input.timeColliding;
        }
    }

    private void OnDrawGizmos()
    {
        if(colliderInputs == null){
            colliderInputs = new ColliderInput[1];
            colliderInputs[0].SetRadius();
        }
        if (colliderInputs != null)
        {
            foreach (ColliderInput input in colliderInputs)
            {
                input.DrawGizmo(this.transform.position);
            }
        }
    }
}

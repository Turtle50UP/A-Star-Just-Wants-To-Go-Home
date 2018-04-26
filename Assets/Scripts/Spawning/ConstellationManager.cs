using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GraphVertex{
	public GameObject vertex;
	public int vertexnum;
}

[System.Serializable]
public class GraphEdge{
	GameObject edge = null;
	public Vector2 endpoints;

    public GameObject Edge
    {
        get
        {
            return edge;
        }

        set
        {
            edge = value;
        }
    }

	public bool Occupied
	{
		get
		{
			return edge != null;
		}
	}

    public bool IsEdge(int v1, int v2){
		int ev1 = (int)endpoints.x;
		int ev2 = (int)endpoints.y;
		//Debug.Log(ev1);
		//Debug.Log(ev2);
		//Debug.Log(v1);
		//Debug.Log(v2);
		return (ev1 == v1 && ev2 == v2) || (ev1 == v2 && ev2 == v1);
	}

	public bool HasDrawnEdge(){
		return Edge != null;
	}
}

[System.Serializable]
public class Constellation{
	public GameObject constellationImage;
	public GameObject[] vertexgos;
	public GraphVertex[] vertices;
	public GraphEdge[] edges;
	int numEdges;
	int numVertices;
	public string difficulty;
	float constant = 10f;

    public int NumEdges
    {
        get
        {
			numEdges = edges.Length;
        	return numEdges;
        }
    }

    public int NumVertices
    {
        get
        {
			numVertices = vertices.Length;
            return numVertices;
        }
    }

    public bool ContainsEdge(int v1, int v2){
		bool res = false;
		foreach(GraphEdge edge in edges){
			res = res || edge.IsEdge(v1,v2);
		}
		return res;
	}

	public bool ContainsVertex(int v){
		bool res = false;
		foreach(GraphVertex vertex in vertices){
			res = res || (vertex.vertexnum == v);
		}
		return res;
	}

	public GraphVertex GetVertex(int v){
		if(ContainsVertex(v)){
			foreach(GraphVertex vertex in vertices){
				if(vertex.vertexnum == v){
					return vertex;
				}
			}
		}
		return null;
	}

	public GraphEdge GetEdge(int v1, int v2){
		if(ContainsEdge(v1,v2)){
			foreach(GraphEdge edge in edges){
				if(edge.IsEdge(v1,v2)){
					return edge;
				}
			}
		}
		return null;
	}

	public int GetEdgeIndex(int v1, int v2){
		if(ContainsEdge(v1,v2)){
			for(int i = 0; i < edges.Length; i++){
				if(edges[i].IsEdge(v1,v2)){
					return i;
				}
			}
		}
		return -1;
	}

	public bool DrawEdge(GameObject edge, int v1, int v2){
		if(ContainsEdge(v1,v2)){
			if(edges[GetEdgeIndex(v1,v2)].Occupied){
				return false;
			}
			GraphVertex ve1 = GetVertex(v1);
			GraphVertex ve2 = GetVertex(v2);
			Vector2 vve1 = new Vector2(ve1.vertex.transform.position.x,
										ve1.vertex.transform.position.y);
			Vector2 vve2 = new Vector2(ve2.vertex.transform.position.x,
										ve2.vertex.transform.position.y);
			if(vve1.y < vve2.y){
				Vector2 temp = vve1;
				vve1 = vve2;
				vve2 = temp;
			}
			Vector2 vdesc = vve1 - vve2;
			float linelen = vdesc.magnitude;
			Vector2 midpoint = (vve1 + vve2) / 2.0f;
			float angle = Vector2.Angle(new Vector2(1.0f,0f),vdesc) + (
                    vdesc.y > 0 ? - 90f : +90f);
			edge.transform.eulerAngles = new Vector3(
				edge.transform.eulerAngles.x,
				edge.transform.eulerAngles.y,
				angle);
			edge.transform.localScale = new Vector3(
				edge.transform.localScale.x,
				linelen,
				edge.transform.localScale.z);
			edge.transform.position = midpoint;

			edges[GetEdgeIndex(v1,v2)].Edge = edge;
			return true;
		}
		return false;
	}

	public bool CheckCompleted(){
		bool res = true;
		foreach(GraphEdge edge in edges){
			//Debug.Log(edge.Edge);
			res = res && edge.Edge != null;
		}
		//Debug.Log(res);
		if(res){
			Debug.Log("Hatred");
			if(!constellationImage.GetComponent<Renderer>().enabled){
				constellationImage.GetComponent<Renderer>().enabled = true;
			}
		}
		else{
			if(constellationImage.GetComponent<Renderer>().enabled){
				constellationImage.GetComponent<Renderer>().enabled = false;
			}
		}
		return res;
	}
	public void TurnOffGraphic(){
		if(constellationImage.GetComponent<Renderer>().enabled){
			constellationImage.GetComponent<Renderer>().enabled = false;
		}
	}

	public GameObject RemoveEdge(int v1, int v2){
		if(ContainsEdge(v1,v2)){
				GameObject res = edges[GetEdgeIndex(v1,v2)].Edge;
				edges[GetEdgeIndex(v1,v2)].Edge = null;
				return res;
		}
		return null;
	}
}

public class ConstellationManager : AbstractSpawner {

	public Constellation constellation;
	public ImageSwitch imageSwitch;
	public bool isEasterEgg = false;
	string edgename = "edge";
	public GameManager gameManager;
	public PlayerExpressionManager p1express;
	public PlayerExpressionManager p2express;
	bool easterEggPlayed = false;
	public EasterEggManager eem;
	public Vector3 ConstellationPosition{
		get{
			return constellation.constellationImage.transform.position;
		}
	}
	public bool FinishedDrawing{
		get{
			bool res = constellation.CheckCompleted();
			if(imageSwitch != null){
				if(isEasterEgg && res && !easterEggPlayed && imageSwitch.isEasterEggMode){
					eem.PlayEasterEgg();
					easterEggPlayed = true;
					imageSwitch.SetTrophiesOff();
				}
			}
			return res;
		}
	}
	public string name;

	public bool DrawEdge(int v1, int v2){
		if(constellation.ContainsEdge(v1,v2)){
			Debug.Log("Found Edge");
			GameObject edge = Spawn(edgename);
			if(!constellation.DrawEdge(edge,v1,v2)){
				Despawn(edge);
				return false;
			}
			if(FinishedDrawing){
				Debug.Log("Finished");
			}
			return true;
		}
		return false;
	}

	public void RemoveEdge(int v1, int v2){
		if(constellation.ContainsEdge(v1,v2)){
			//Debug.Log("Found Edge");
			GameObject edge = constellation.RemoveEdge(v1,v2);
			Despawn(edge);
		}
	}

	public void DrawAllEdges(){
		//Debug.Log(constellation.NumVertices);
		for(int i = 0; i < constellation.NumVertices; i++){
			for(int j = i + 1; j < constellation.NumVertices; j++){
				DrawEdge(i,j);
			}
		}
		bool test = FinishedDrawing;
	}

	public void AssignAllIndices(){
		for(int i = 0; i < constellation.vertexgos.Length; i++){
			constellation.vertices[i].vertexnum = i;
			constellation.vertices[i].vertex = constellation.vertexgos[i];
		}
		foreach(GraphVertex gv in constellation.vertices){
			gv.vertex.GetComponent<StarDetails>().SetIndex(gv.vertexnum);
			gv.vertex.GetComponent<StarDetails>().SetGV(gv);
			gv.vertex.GetComponent<StarDetails>().SetCM(this);
		}
	}

	public void DespawnConstellation(){
		for(int i = 0; i < constellation.NumVertices; i++){
			for(int j = i + 1; j < constellation.NumVertices; j++){
				RemoveEdge(i,j);
			}
		}
		bool test = FinishedDrawing;
		easterEggPlayed = false;
	}

	//public bool DrawEdge(GraphVertex graphVertex){}

	// Use this for initialization
	void Start () {
		AssignAllIndices();
	}
	
	// Update is called once per frame
	void Update () {

	}
	
}

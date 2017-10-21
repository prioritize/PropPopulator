using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomProp : MonoBehaviour {

	// Use this for initialization
	public MeshFilter meshFilter;
	public Mesh mesh;
	public GameObject rayCastTarget;
	private Collider rayCastCollider;
	private bool needVertexes = true;
	private bool needTrees = true;
	private bool needRocks= true;
	private Vector3 oldPos;
	private List<Vector3> propPosits = new List<Vector3>();
	public int numberTrees;
	public int numberRocks;
	public GameObject[] treeProps;
	public GameObject[] rockProps;
	void Awake() {
		rayCastCollider = rayCastTarget.GetComponent<Collider>();
		mesh = meshFilter.sharedMesh;
	}
	
	// Update is called once per frame
	void Update () {
		if(needVertexes){
			propPosits = GetVertex(mesh, rayCastTarget);
			Vector3[] verts = new Vector3[propPosits.Count];
			verts = propPosits.ToArray();
			Debug.Log(propPosits);
			needVertexes = false;
			 
			foreach(Vector3 item in propPosits){
				Debug.Log(item);
			}
			foreach(Vector3 item in verts){
				Debug.Log(item);
			}
			
			if(needTrees){
				propPosits = PlaceProps(numberTrees, propPosits, treeProps);
				needTrees = false;
			}
			if(needRocks){
				propPosits = PlaceProps(numberRocks, propPosits, rockProps);
			}
		}

	}
	/// <summary>
	/// Callback to draw gizmos that are pickable and always drawn.
	/// </summary>
	void OnDrawGizmos(){
		/* 
		mesh = meshFilter.sharedMesh;
		int v = 0;
		bool rayCastOut;
		if(transform.position != oldPos){
			needVertexes = true;
		}
		RaycastHit hit;
		if(needVertexes){
			oldPos = transform.position;
			while(v < mesh.vertices.Length){
				//Debug.Log("Drew a Gizmo");
				//Raycast to object
				rayCastOut = Physics.Raycast(rayCastTarget.transform.position, (transform.position + mesh.vertices[v])-rayCastTarget.transform.position, out hit);


				if(hit.point == (transform.position + mesh.vertices[v])){
					Gizmos.color = Color.green;
					Gizmos.DrawSphere(transform.position + mesh.vertices[v], .15f);
					propPosits.Add(transform.position + mesh.vertices[v]);
				}
				else{
					Gizmos.color = Color.red;
					Gizmos.DrawSphere(transform.position + mesh.vertices[v], .15f);
				}
				//Debug.DrawRay(mesh.vertices[v], rayCastTarget.transform.position-mesh.vertices[v], Color.red, 1.0f);
			v++;
			}
			Debug.Log(propPosits.Count);
				foreach(Vector3 item in propPosits){
					Debug.DrawRay(rayCastTarget.transform.position, item - rayCastTarget.transform.position, Color.blue, 1f);
				}
				needVertexes = false;
		}
		*/
	}
	List<Vector3> GetVertex(Mesh mesh, GameObject rayCastOrigin){
		int v = 0;
		RaycastHit hit;
		List<Vector3> validVertex = new List<Vector3>();
		bool rayCastBool;
		
		while(v < mesh.vertices.Length){
			rayCastBool = Physics.Raycast(rayCastTarget.transform.position, (transform.position + mesh.vertices[v])-rayCastTarget.transform.position, out hit);
			if(rayCastBool){
				if(hit.point == (transform.position + mesh.vertices[v])){
					validVertex.Add((transform.position+mesh.vertices[v]));
				}
			
			}
			v++;
		}
		return validVertex;
	}
	List<Vector3> PlaceProps(int numberProps, List<Vector3> vertList, GameObject[] propArray){
		int propCounter = 0;
		System.Random rnd = new System.Random();
		while(propCounter < numberProps){
			int vert = rnd.Next(0, propPosits.Count);
			int prop = rnd.Next(0, treeProps.Length);
			int angle = rnd.Next(0,360);
			float scale = rnd.Next(1,3);
			GameObject currentProp = Instantiate(propArray[prop], vertList[vert], Quaternion.identity);
			currentProp.transform.rotation = Quaternion.AngleAxis(angle, Vector3.up);
			currentProp.transform.parent = this.transform;
			currentProp.transform.localScale = scale*Vector3.one;
			vertList.RemoveAt(vert);
			propCounter++;
		}
		return vertList;
	}
}

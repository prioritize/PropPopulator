using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
//[ExecuteInEditMode]
public class PropPopulator : MonoBehaviour {

	// Use this for initialization
	public Mesh mesh; 
	private RaycastHit hit;
	public GameObject rayCastTarget;
	private Collider rayCastHitCollider;
	void Awake () {
		Debug.Log("Awake Ran");
		rayCastHitCollider = rayCastTarget.GetComponent<Collider>();
		mesh = GetComponent<MeshFilter>().mesh;
	}
	
	// Update is called once per frame
	void Update () {
	}
	void OnDrawGizmos(){
		int i = 0;
		while(i < mesh.vertices.Length){
			Physics.Raycast(rayCastTarget.transform.position, (transform.position + mesh.vertices[i]) - rayCastTarget.transform.position, out hit);
			if(hit.point == transform.position + mesh.vertices[i]){
				Gizmos.color = Color.green;
				Gizmos.DrawSphere(transform.position + mesh.vertices[i], .15f);
				Debug.DrawRay(rayCastTarget.transform.position, (transform.position + mesh.vertices[i]) - rayCastTarget.transform.position, Color.red, 0.01f);
			}
			else{
				Gizmos.color = Color.red;
				Gizmos.DrawSphere(transform.position + mesh.vertices[i], .15f);
			}
			i++;
		}
		
	}
}

using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Level))]
public class LevelInspector : Editor {
	public Mesh mesh;
	public int i = 0;
	// Use this for initialization
	[DrawGizmo(GizmoType.InSelectionHierarchy | GizmoType.NotInSelectionHierarchy)]
	private void OnSceneGUI(){
		if(mesh == null){//Component.GetComponent<MeshFilter>().mesh;
			GameObject selectedObject = Selection.activeGameObject;
			mesh = selectedObject.GetComponent<MeshFilter>().mesh;
		    Debug.Log(mesh.vertices.Length);
		}
		Level level = target as Level;
		Handles.color = Color.black;
		//Handles.color = Color.white;
		//Handles.DrawLine(level.p0, level.p1);
	}
	private void OnDrawGizmos(){
		while(i < mesh.vertices.Length){
			Gizmos.DrawSphere(mesh.vertices[i], 0.1f);
			i++;
		}
	}
}



using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshFilter))]

public class Frustum : MonoBehaviour {

    public Rect near;
    public Rect far;

    MeshFilter meshFilter;
    Mesh newMesh;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

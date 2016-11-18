using UnityEngine;
using System.Collections;

public class RotateAroundObject : MonoBehaviour {

    public Transform targetObject;
    public Vector3 direction;
    public float speed;
    
	
	// Update is called once per frame
	void Update () {
	    transform.RotateAround(targetObject.position, direction, speed * Time.deltaTime);
	}
}

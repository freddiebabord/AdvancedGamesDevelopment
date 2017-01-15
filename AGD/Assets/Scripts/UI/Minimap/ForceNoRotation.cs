using UnityEngine;
using System.Collections;

public class ForceNoRotation : MonoBehaviour {

	Quaternion startRotation;
	Vector3 startPosition;

	// Use this for initialization
	void Start () {
		startRotation = transform.localRotation;
		startPosition = transform.localPosition;
	}
	
	// Update is called once per frame
	void Update () {
		transform.localRotation = startRotation;
		transform.localPosition = startPosition;
	}
}

using UnityEngine;
using System.Collections;

public class LockRotation : MonoBehaviour
{

    public Vector3 customRotation = Vector3.zero;

	
	// Update is called once per frame
	void Update () {
	    transform.rotation = Quaternion.Euler(customRotation);
	}
}

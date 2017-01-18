using UnityEngine;
using System.Collections;

public class FrustumCollider : MonoBehaviour {

	Frustum frustum;

	void Start()
	{
		frustum = GetComponentInParent<Frustum> ();
	}

    void OnTriggerEnter(Collider other)
    {
        frustum.OnTriggerEnter(other);
    }
}

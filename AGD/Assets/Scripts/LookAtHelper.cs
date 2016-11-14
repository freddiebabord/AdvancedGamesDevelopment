using UnityEngine;
using System.Collections;

public enum LookAtHelperMode
{
    TargetTransform,
    TargetPosition
}

public class LookAtHelper : MonoBehaviour
{

    public Vector3 position;
    public Transform targetTransform;
    public LookAtHelperMode mode;
    
	// Update is called once per frame
	void Update () {
	    switch (mode)
	    {
	        case LookAtHelperMode.TargetPosition:
                transform.LookAt(targetTransform);
                break;
            case LookAtHelperMode.TargetTransform:
                transform.LookAt(position);
                break;
	    }
	}
}

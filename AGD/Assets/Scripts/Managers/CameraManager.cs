using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour
{

    private Vector3 startPosition;
    private Quaternion startRotation;
    private bool withinPlayer, moveCamera = false, resetCamera = false;
    [SerializeField]private Transform headTransform;
    private float colliderRadius;
    private Vector3 targetPosition;

	// Use this for initialization
	void Start ()
	{
	    startPosition = transform.localPosition;
	    startRotation = transform.rotation;
	    colliderRadius = GetComponent<SphereCollider>().radius + 0.01f;

	}
	
	// Update is called once per frame
	void Update () {
	    if (resetCamera)
	    {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, startPosition, 1*Time.deltaTime);
            Debug.Log(Vector3.Distance(transform.localPosition, startPosition));
	        if (Vector3.Distance(transform.localPosition, startPosition) < 0.15f)
	            resetCamera = false;
	    }
        if (moveCamera)
        {
            Vector3 direction = headTransform.position - transform.position;
            direction.Normalize();
            transform.position += direction * 0.01f;

            if (Vector3.Distance(transform.position, headTransform.position) < 0.15f)
            {
                moveCamera = false;
                resetCamera = true;
            }
        }        
    }

    void FixedUpdate()
    {
        Ray ray = new Ray(transform.position + transform.forward * colliderRadius, headTransform.position - transform.position);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 30))
        {
            if (!hit.transform.CompareTag("Player"))
            {
                moveCamera = true;
            }
        }        
        Debug.DrawRay(transform.position + transform.forward * colliderRadius, headTransform.position - transform.position, Color.blue);
        
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.transform.tag);
        if (other.transform.CompareTag("Player"))
        {
            moveCamera = false;
            resetCamera = true;
        }
    }

    //void OnTriggerStay(Collider other)
    //{
    //    Debug.Log(other.transform.tag);
    //}

    //void OnTriggerExit(Collider other)
    //{
    //    if (other.transform.CompareTag("Player"))
    //    {
    //        moveCamera = false;
    //        resetCamera = true;
    //    }
    //}
}

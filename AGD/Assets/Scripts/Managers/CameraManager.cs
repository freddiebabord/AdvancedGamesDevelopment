using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour
{
    public Camera targetCamera;
    public Transform characterHead, headOffsetTargetTransform;
    private Vector3 startLocalPosition;
    public LayerMask targetMask;
    private float maxDistance;
    public float speed = 5.0f;

    void Start()
    {
        if (!targetCamera)
            targetCamera = Camera.main;
        startLocalPosition = targetCamera.transform.localPosition;
        maxDistance = Vector3.Distance(targetCamera.transform.position, characterHead.position);
    }

    void FixedUpdate()
    {
        
        Ray invertedRay = new Ray(characterHead.position,
            (targetCamera.transform.TransformPoint(startLocalPosition) - characterHead.position).normalized);
        Debug.DrawRay(characterHead.position, (targetCamera.transform.TransformPoint(startLocalPosition) - characterHead.position).normalized,
            Color.red);
        RaycastHit hit;
        if (Physics.Raycast(invertedRay, out hit, maxDistance, targetMask))
        {
            Debug.DrawLine(invertedRay.origin, hit.point, Color.blue);
            // targetCamera.transform.InverseTransformPoint(hit.point + (targetCamera.transform.position - characterHead.position).normalized * 0.1f);
            if (!lerpToHead)
            {
                lerpToHead = true;
                currentLerpTime = 0.0f;
            }
            LerpToHead();
        }
        else
        {
            if (lerpToHead)
            {
                lerpToHead = false;
                currentLerpTime = 0.0f;
            }
            LerpToStart();
        }


        // targetCamera.transform.localPosition = Vector3.Slerp(targetCamera.transform.localPosition, targetLocalPosition, Time.deltaTime * speed);
        targetCamera.transform.localPosition = new Vector3(targetCamera.transform.localPosition.x, startLocalPosition.y, targetCamera.transform.localPosition.z);
    }

    private bool lerpToHead = false;
    private float currentLerpTime = 0f;
    public float lerpDuration = 5f;

    void LerpToHead()
    {
		targetCamera.transform.localPosition = Vector3.Lerp(targetCamera.transform.localPosition, targetCamera.transform.InverseTransformPoint(headOffsetTargetTransform.transform.position), Mathf.Clamp01(currentLerpTime / lerpDuration));
        currentLerpTime+=Time.deltaTime;
    }

    void LerpToStart()
    {
        targetCamera.transform.localPosition = Vector3.Lerp(targetCamera.transform.localPosition, startLocalPosition, Mathf.Clamp01(currentLerpTime / lerpDuration));
        currentLerpTime += Time.deltaTime;
    }
}

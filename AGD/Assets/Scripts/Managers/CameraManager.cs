using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour
{
    public Camera camera;
    private Vector3 cameraOriginalStartPosition;

    void Start()
    {
        cameraOriginalStartPosition = camera.transform.localPosition;
    }

    void Update()
    {
        Vector3 bottomLeft = camera.transform.InverseTransformPoint(camera.ScreenToWorldPoint(new Vector3(0, 0, 0)));
        Vector3 topLeft = camera.transform.InverseTransformPoint(camera.ScreenToWorldPoint(new Vector3(0, 1, 0)));
        Vector3 topRight = camera.transform.InverseTransformPoint(camera.ScreenToWorldPoint(new Vector3(1, 1, 0)));
        Vector3 bottomRight = camera.transform.InverseTransformPoint(camera.ScreenToWorldPoint(new Vector3(1, 0, 0)));
        camera.transform.localPosition = HandleCollisionZoom(cameraOriginalStartPosition, transform.localPosition, 1.0f, new [] {bottomLeft, bottomRight, topLeft, topRight});
        
    }

    // returns a new camera position
    Vector3 HandleCollisionZoom(Vector3 camPos, Vector3 targetPos, float minOffsetDist, Vector3[] frustumNearCorners)
    {
        float offsetDist = (targetPos - camPos).magnitude;
        float raycastLength = offsetDist - minOffsetDist;
        if (raycastLength < 0.0f)
        {
            // camera is already too near the lookat target
            return camPos;
        }

    Vector3 camOut = (targetPos - camPos).normalized;
    Vector3 nearestCamPos = targetPos - camOut * minOffsetDist;
    float minHitFraction = 1.0f;

        for (int i = 0; i< frustumNearCorners.Length; i++)
        {
            Vector3 corner = frustumNearCorners[i];
            Vector3 offsetToCorner = corner - camPos;
            Vector3 rayStart = nearestCamPos + offsetToCorner;
            Vector3 rayEnd = corner;
            Debug.DrawLine(rayStart, rayEnd, Color.green);
            // a result between 0 and 1 indicates a hit along the ray segment
            RaycastHit hit;
            if(Physics.Linecast(rayStart, rayEnd, out hit))
            {
                float hitFraction = (rayStart - hit.point).magnitude;
                minHitFraction = Mathf.Min(hitFraction, minHitFraction);
            }
            
        }        

        if (minHitFraction< 1.0f)
        {
            return nearestCamPos - camOut* (raycastLength* minHitFraction);
        }
        else
        {
            return camPos;
        }
    }
}

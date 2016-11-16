using UnityEngine;
using System.Collections;

public class GhostBodyAdjustments : MonoBehaviour {

    public float yThreshold = 0.5f;
    public float minHover = 1.75f;

    Transform parentPos;
    NavMeshAgent parentAgent;
    Vector3 startPos;
    Vector3 targetPos;
    float startTime;
    float posY;
    float oldPosY;
    bool isDropping;
    float floatTime;

    // Use this for initialization
    void Start ()
    {
        parentAgent = GetComponentInParent<NavMeshAgent>();
        parentPos = transform.parent;
        posY = parentPos.position.y;
        oldPosY = posY;
	}
	
	// Update is called once per frame
	void Update ()
    {
        posY = parentPos.position.y;
        print("<color=yellow>Old: " + oldPosY + "</color> <color=green>Current: " + posY + "</color>");
        if (isDropping)
        {
            float fracComplete = (Time.time - startTime) / floatTime;
            float newY = Vector3.Slerp(startPos, targetPos, fracComplete).y;
            transform.localPosition = new Vector3(transform.localPosition.x, newY, transform.localPosition.z);
            print(newY);
            if (newY == targetPos.y || newY <= minHover)
            {
                print("<color=cyan>Reset!</color>");
                isDropping = false;
            }
	    }
    }

    void LateUpdate()
    {
        if (oldPosY - posY > yThreshold && !isDropping)
        {
            isDropping = true;
            startTime = Time.time;
            targetPos = parentAgent.path.corners[0];
            startPos = new Vector3(transform.localPosition.x, (oldPosY - transform.localPosition.y), transform.localPosition.z);
            transform.localPosition = startPos;
            floatTime = Vector3.Distance(startPos, targetPos) / parentAgent.speed;
        }
        oldPosY = posY;
    }
}

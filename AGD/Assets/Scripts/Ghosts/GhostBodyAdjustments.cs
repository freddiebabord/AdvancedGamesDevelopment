using UnityEngine;
using System.Collections;

public class GhostBodyAdjustments : MonoBehaviour {

    public float yThreshold = 0.5f;

    Transform parentPos;
    float posY;
    float oldPosY;

    // Use this for initialization
    void Start ()
    {
        parentPos = transform.parent;
        posY = parentPos.position.y;
        oldPosY = posY;
	}
	
	// Update is called once per frame
	void Update ()
    {
        posY = parentPos.position.y;
        print(oldPosY - posY);
        if (oldPosY - posY > yThreshold)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + oldPosY, transform.position.z);
        }
        oldPosY = posY;
	}
}

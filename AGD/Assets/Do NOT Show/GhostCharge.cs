using UnityEngine;
using System.Collections;

public class GhostCharge : MonoBehaviour {

    public float chargeSpeed = 10f;

    bool charging = false;
    Vector3 startPosition;

	// Use this for initialization
	void Start ()
    {
        startPosition = transform.position;
	}
	
	// Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            charging = true;
        }
        if (charging)
        {
            float step = chargeSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, startPosition + Vector3.back*10 + Vector3.right*10, step);
        }
    }
}

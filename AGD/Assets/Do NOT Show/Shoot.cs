using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour {

    public float fireRate = 1f;
    public float fireForce = 10f;
    public Rigidbody bullet;

    bool isShooting = false;
    float startfireRateTimer;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if (Input.GetKeyDown(KeyCode.Space))
        {
            isShooting = true;
            startfireRateTimer = Time.time;
        }

        if (isShooting)
        {
            if (Time.time - startfireRateTimer > fireRate)
            {
                Rigidbody newInstance = Instantiate(bullet, transform.position + Vector3.back + Vector3.left, transform.rotation) as Rigidbody;
                Vector3 targetFwd = (transform.position + Vector3.back + Vector3.left) - transform.position;
                newInstance.AddForce(targetFwd * fireForce);
                startfireRateTimer = Time.time;
            }
        }
	}
}

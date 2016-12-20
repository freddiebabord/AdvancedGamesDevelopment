using UnityEngine;
using System.Collections;

public class GhostBodyCollision : MonoBehaviour {

    GameObject root;
    Charge charge;
    //Teleportation teleport;

    void Start()
    {
		root = transform.root.gameObject;
        charge = root.GetComponent<Charge>();
        //teleport = root.GetComponent<Teleportation>();
    }

    void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player")
        {
            if (charge)
            {
                charge.StealPoints();
            }
        }
    }
}

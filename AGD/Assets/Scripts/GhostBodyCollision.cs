using UnityEngine;
using System.Collections;

public class GhostBodyCollision : MonoBehaviour {

    GameObject root;
    LocalFrustum childFrustum;
    Charge charge;
    Teleportation teleport;

    void Start()
    {
        root = transform.parent.parent.gameObject;
        childFrustum = GetComponentInChildren<LocalFrustum>();
        charge = root.GetComponent<Charge>();
        teleport = root.GetComponent<Teleportation>();
    }

    void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player")
        {
            if (charge)
            {
                root.GetComponent<Charge>().StealPoints();
            }
        }
    }
}

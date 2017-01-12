using UnityEngine;
using System.Collections;

public class GhostBodyCollision : MonoBehaviour {

    GameObject root;
    LocalFrustum childFrustum;
    GhostCharge charge;
    GhostTeleport teleport;

    void Start()
    {
        root = transform.parent.parent.gameObject;
        childFrustum = GetComponentInChildren<LocalFrustum>();
        charge = root.GetComponent<GhostCharge>();
        teleport = root.GetComponent<GhostTeleport>();
    }

    void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player")
        {
            if (charge)
            {
                root.GetComponent<GhostCharge>().StealPoints();
            }
        }
    }
}

using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Networking;
using System.Collections;

public class DevShortcuts : MonoBehaviour {

    [SerializeField]List<GhostBehaviour> ghosts = new List<GhostBehaviour>();

	// Use this for initialization
	void Start ()
    {
        ghosts = FindObjectsOfType<GhostBehaviour>().ToList();
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if(Input.GetKeyDown(KeyCode.F1))
        {
            ghosts = FindObjectsOfType<GhostBehaviour>().ToList();
            print("Hello");
            foreach (GhostBehaviour ghost in ghosts)
            {
                ghost.TakeDamage(GetComponent<NetworkIdentity>().connectionToClient.connectionId);
            }
        }
	}
}

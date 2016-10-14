using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Networking;
using System.Collections;

public class DevShortcuts : NetworkBehaviour {

    [SerializeField]List<GhostBehaviour> ghosts = new List<GhostBehaviour>();

	// Use this for initialization
	void Start ()
    {
        ghosts = FindObjectsOfType<GhostBehaviour>().ToList();
	}
	
	// Update is called once per frame
	void Update ()
	{
        
        
	    if (!isLocalPlayer)
	        return;

	    if(Input.GetKeyDown(KeyCode.F2))
        {
            Cmd_SendTakeTamage();
        }
	}
    

	[Command]
	public void Cmd_SendTakeTamage()
	{
		ghosts = FindObjectsOfType<GhostBehaviour>().ToList();
            print("Hello");
            foreach (GhostBehaviour ghost in ghosts)
            {
                if (isServer)
                    ghost.TakeDamage(GetComponent<NetworkIdentity>().connectionToClient.connectionId, 5f);
                else
                    ghost.TakeDamage(GetComponent<NetworkIdentity>().connectionToServer.connectionId, 5f);
            }
	}
}

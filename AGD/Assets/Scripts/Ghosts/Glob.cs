using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Glob : NetworkBehaviour {

    [SyncVar]
    public NetworkInstanceId parentNetID;

    NetworkIdentity rootNetID;
    float fearAffect;

    public override void OnStartClient()
    {
        GameObject parentObj = ClientScene.FindLocalObject(parentNetID);
        transform.SetParent(parentObj.transform.GetChild(0));
    }

    public void PostStart()
    {

    }
	
	// Update is called once per frame
	void Update () {
	
	}
}

using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class GhostThrow : NetworkBehaviour {

    public GameObject globPrefab;
    public GameObject handParent;

    GameObject m_networkGlob;
	
    // Use this for initialization
	void Start ()
    {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SpawnGlob()
    {
        m_networkGlob = Instantiate(globPrefab, transform.GetChild(0).position, transform.GetChild(0).rotation) as GameObject;
        m_networkGlob.GetComponent<Glob>().parentNetID = netId;
        NetworkServer.Spawn(m_networkGlob);
        m_networkGlob.GetComponent<Glob>().PostStart();
    }

    public void ThrowGlob()
    {

    }
}

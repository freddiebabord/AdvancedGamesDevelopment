using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class GhostThrow : NetworkBehaviour {

	Animator animator;
    public GameObject globPrefab;
    public GameObject handParent;
	public float fearImpact = 10f;
	public float sizeOffset = 0.1f;
	public float growSpeed = 1f;
	public float shrinkSpeed = 1f;
	public float throwForce = 10f;

    GameObject m_networkGlob;
	Rigidbody globRigidbody;
	
    // Use this for initialization
	void Start ()
    {
		animator = GetComponentInChildren<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SpawnGlob()
    {
        m_networkGlob = Instantiate(globPrefab, Vector3.zero, handParent.transform.rotation) as GameObject;
		Glob g = m_networkGlob.GetComponent<Glob> ();
		g.parentNetID = netId;
		g.parentObj = gameObject;
		g.transform.SetParent(handParent.transform);
        NetworkServer.Spawn(m_networkGlob);
		m_networkGlob.GetComponent<Glob>().PostStart(fearImpact, growSpeed, sizeOffset);
		globRigidbody = m_networkGlob.GetComponent<Rigidbody> ();
		//Debug.Break ();
    }

    public void ThrowGlob()
    {
		m_networkGlob.transform.parent = null;
		globRigidbody.AddRelativeForce(transform.forward * throwForce, ForceMode.Impulse);
		m_networkGlob.GetComponent<Glob> ().ThrowGlob (shrinkSpeed);
    }

	public void Triggered()
	{
		animator.SetBool ("Throwing", true);
	}

	public void Reset()
	{
		animator.SetBool ("Throwing", false);
	}
}

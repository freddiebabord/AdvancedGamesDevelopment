using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Glob : NetworkBehaviour {

    [SyncVar]
    public NetworkInstanceId parentNetID;

    NetworkIdentity rootNetID;
    float fearEffect;
	float smoothSpeed;
	float targetOffset;
	Vector3 targetScale;
	Vector3 velocity;
	bool isGrowing;
	public GameObject parentObj;

    public override void OnStartClient()
    {
        parentObj = ClientScene.FindLocalObject(parentNetID);
		//transform.SetParent(GameObject.Find("Hand_R").transform);
    }


	public void PostStart(float fear, float grow, float offset)
    {
		fearEffect = fear;
		smoothSpeed = grow;
		targetOffset = offset;
		targetScale = transform.localScale;
		transform.localScale = Vector3.zero;
		transform.localPosition = Vector3.zero;
    }
	
	// Update is called once per frame
	void Update ()
	{
		transform.localScale = Vector3.SmoothDamp (transform.localScale, targetScale, ref velocity, smoothSpeed);
		if (transform.parent != null)
			transform.localPosition = new Vector3(0f,-0.08f,0.13f);
		if (targetScale == Vector3.zero && Vector3.Distance (transform.localScale, targetScale) < targetOffset)
		{
			Destroy (gameObject);
		}
	}

	public void ThrowGlob(float shrink)
	{
		smoothSpeed = shrink;
		targetScale = Vector3.zero;
	}

	void OnCollisionEnter(Collision other)
	{
		if (other.transform.tag == "Player")
		{
			//other.transform.GetComponent<NetworkedThirdPersonCharacter> ().playerID;
			print ("Player Hit!!!");
		}
	}
}

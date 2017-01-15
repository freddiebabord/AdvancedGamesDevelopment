using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Room : MonoBehaviour {

	private List<Collider> colliders = new List<Collider>();
	private List<GhostBehaviour> enemies = new List<GhostBehaviour>();
	private List<NetworkedFirstPersonController> players = new List<NetworkedFirstPersonController>();
	public int enemyCount { get{ return enemies.Count; } }
	public int playerCount { get{ return players.Count; } }
	private List<Collider> doors = new List<Collider>();
    public List<Collider> Doors { get { return doors; } }
	private List<ReflectionProbe> roomReflectionProbes = new List<ReflectionProbe> ();

	// Use this for initialization
	void Start () {
		colliders = GetComponentsInChildren<Collider>().ToList();
		doors = colliders.FindAll(x => x.gameObject.tag == "Door").ToList();
		roomReflectionProbes = GetComponentsInChildren<ReflectionProbe> ().ToList ();
	}	

	public void OnTriggerEnter(Collider other)
	{
		if(other.GetComponent<GhostBehaviour>())
		{
			if(!enemies.Contains(other.GetComponent<GhostBehaviour>()))
				enemies.Add(other.GetComponent<GhostBehaviour>());
		}
		else if(other.GetComponent<NetworkedFirstPersonController>())
		{
			if(!players.Contains(other.GetComponent<NetworkedFirstPersonController>()))
				players.Add(other.GetComponent<NetworkedFirstPersonController>());
		}
	}

	public void OnTriggerStay(Collider other)
	{
		if(other.gameObject.CompareTag("Player") && !updatingProbes)
		{
			StartCoroutine (UpdatingReflectionProbe ());
		}
	}

	public void OnTriggerExit(Collider other)
	{
		if(other.GetComponent<GhostBehaviour>())
		{
			enemies.Remove(other.GetComponent<GhostBehaviour>());
			enemies.TrimExcess();
		}
		else if(other.GetComponent<NetworkedFirstPersonController>())
		{
			players.Remove(other.GetComponent<NetworkedFirstPersonController>());
		}
	}

	private bool updatingProbes = false;
	private IEnumerator UpdatingReflectionProbe()
	{
		updatingProbes = true;
		yield return null;
		for (int i = 0; i < roomReflectionProbes.Count; ++i) {
			roomReflectionProbes [i].RenderProbe ();		
		}
		updatingProbes = false;
	}
}

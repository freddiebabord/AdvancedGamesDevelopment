using UnityEngine;
using System.Collections;

public class RoomCollider : MonoBehaviour {

	private Room room;
	public Room GetRoom { get { return room; } }

	
	// Use this for initialization
	void Start () {
		room = GetComponentInParent<Room>();
	}

	void OnTriggerEnter(Collider other)
	{
		if(room)
			room.OnTriggerEnter(other);
	}

	void OnTriggerExit(Collider other)
	{
		if(room)
			room.OnTriggerExit(other);
	}

}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoomCollection : MonoBehaviour {

    List<BoxCollider> colliders = new List<BoxCollider>();

	// Use this for initialization
	void Start ()
    {
        RoomCollider[] roomColliders = FindObjectsOfType(typeof(RoomCollider)) as RoomCollider[];
        foreach (RoomCollider room in roomColliders)
        {
            colliders.Add(room.GetComponent<BoxCollider>());
        }
	}

    public Vector3 GetRandomPositionInRoom()
    {
        int roomSelection = Random.Range(0, colliders.Count - 1);

        Vector3 min = colliders[roomSelection].GetComponent<BoxCollider>().bounds.min;
        Vector3 max = colliders[roomSelection].GetComponent<BoxCollider>().bounds.max;

        float x = Random.Range(min.x, max.x);
        float y = Random.Range(min.y, max.y);
        float z = Random.Range(min.z, max.z);

        return new Vector3(x, y, z);
    }
}

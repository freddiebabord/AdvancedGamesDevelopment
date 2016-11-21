using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPool : MonoBehaviour {

	private List<GameObject> pooledObjects = new List<GameObject>();
	public GameObject prefabToPool;
	public int poolSize;
	private int currentPoolSize;

	// Use this for initialization
	void Start () {
		for(int i = 0; i < poolSize; ++i)
		{
			pooledObjects.Add((GameObject)Instantiate(prefabToPool));
			pooledObjects[i].SetActive(false);
		}
		currentPoolSize = poolSize;
	}
	
	public GameObject Spawn(Vector3 position, Quaternion rotation)
	{
		for(int i = 0; i < currentPoolSize; ++i)
		{
			if(!pooledObjects[i].activeInHierarchy)
			{
				pooledObjects[i].transform.position = position;
				pooledObjects[i].transform.rotation = rotation;
				pooledObjects[i].SetActive(true);
				return pooledObjects[i];
			}
		}

		GameObject extendingObject = Instantiate(prefabToPool, position, rotation) as GameObject;
		currentPoolSize++;
		pooledObjects.Add(extendingObject);
		return extendingObject;
	}

	public void Despawn(GameObject targetObject)
	{
		GameObject go = pooledObjects.Find(x => x == targetObject);
		go.SetActive(false);
	}

	public void ClearPool()
	{
		for(int i = 0; i < currentPoolSize; ++i)
			Destroy(pooledObjects[i]);
		pooledObjects.Clear();
		currentPoolSize = 0;
	}
}

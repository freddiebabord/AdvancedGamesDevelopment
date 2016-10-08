using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;

public class EnemyPoolSpawner : NetworkBehaviour {

	public GameObject enemyPrefab;
	
	private List<GameObject> spawnedEnemies = new List<GameObject>();

	public int SpawnedEnemies { get{ spawnedEnemies.TrimExcess(); return spawnedEnemies.Count; } }

	void Start()
	{
		if(!isServer)
			return;
			
		Spawn(5);
	}

	public void Spawn(int amount)
	{
		for(int i = 0; i < amount; ++i)
		{
			GameObject newEnemy = (GameObject)Instantiate(enemyPrefab);
			spawnedEnemies.Add(newEnemy);
			NetworkServer.Spawn(newEnemy);
		}
	}
}

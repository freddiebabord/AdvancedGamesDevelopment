using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;
using System.Linq;

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
			var spawnLocations = GameObject.FindObjectsOfType<NetworkStartPosition>().ToList();
			var enemySpawns = spawnLocations.FindAll(x => x.gameObject.CompareTag("enemySpawn")).ToList();
			var enemySpawn = enemySpawns[Random.Range(0, enemySpawns.Count)].transform;
			GameObject newEnemy = (GameObject)Instantiate(enemyPrefab, enemySpawn.position, enemySpawn.rotation);
			spawnedEnemies.Add(newEnemy);
			NetworkServer.Spawn(newEnemy);
		}
	}
}

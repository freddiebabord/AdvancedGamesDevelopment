using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using System.Linq;

public enum	EnemyType
{
	Type1 = 0,
	Type2,
	Type3,
	Type4,
	Type5,
	Type6,
	Type7
}

[System.Serializable]
public sealed class EnemyDefinition
{
	public EnemyType enemySpawner;
	public int quantity;
};

[System.Serializable]
public sealed class WaveDefinition
{
	public List<EnemyDefinition> enemyDef = new List<EnemyDefinition>();
};

public class GameManager : NetworkBehaviour {

	public static GameManager instance;

	[HideInInspector]public List<WaveDefinition> waves = new List<WaveDefinition>();
	[SyncVar]public int currentWave = 0;
	WaveDefinition currentWaveDefinition;
	int enemyCount = 0;
	public int waveSleepTimer;
	private bool waveComplete = false;
	public bool WaveComplete { get { return waveComplete; } }
	public List<GameObject> enemyPrefabs = new List<GameObject>();

	void Awake()
	{
		DontDestroyOnLoad(this);
		instance = this;
	}

	// Use this for initialization
	void Start () {
		enabled = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(!isServer)
			return;
		
		if(enemyCount <= 0 && !waveComplete)
			StartCoroutine(WaveWaitTimer());
	}

	public void DestroyEnemy()
	{
		enemyCount--;
	}

	IEnumerator WaveWaitTimer()
	{
		waveComplete = true;
		yield return new WaitForSeconds(waveSleepTimer);
		currentWave++;
		if(currentWave < waves.Count)
			SpawnEnemies();
		waveComplete = false;
	}

	public void SpawnEnemies()
	{
		var spawnLocations = GameObject.FindObjectsOfType<NetworkStartPosition>().ToList();
		var enemySpawns = spawnLocations.FindAll(x => x.gameObject.CompareTag("enemySpawn")).ToList();
		for(int i = 0; i < waves[currentWave].enemyDef.Count; ++i)
		{
			EnemyType etype = waves[currentWave].enemyDef[i].enemySpawner;
			GameObject prefab;
			for(int j = 0; j < waves[currentWave].enemyDef[i].quantity; ++j)
			{
				prefab = enemyPrefabs[(int)etype];
				var enemySpawn = enemySpawns[Random.Range(0, enemySpawns.Count)].transform;
				GameObject newEnemy = (GameObject)Instantiate(prefab, enemySpawn.position, enemySpawn.rotation);
				NetworkServer.Spawn(newEnemy);
				enemyCount++;
			}
		}
		
		
	}
}

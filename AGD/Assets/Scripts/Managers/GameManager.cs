using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using System.Linq;
using UnityEngine.UI;

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
    public List<Text>enemiesRemainigText = new List<Text>();
    public bool playerOneAssigned = false;

	void Awake()
	{
		DontDestroyOnLoad(this);
        if(instance != null)
            Destroy(this.gameObject);
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
	    if (enemyCount > 0)
	    {
            foreach (Text text in enemiesRemainigText)
                text.text = enemyCount.ToString();
        }
	    else
	    {
            foreach (Text text in enemiesRemainigText)
                text.text = "Loading next wave";
        }
	}

	IEnumerator WaveWaitTimer()
	{
		waveComplete = true;
		yield return new WaitForSeconds(waveSleepTimer);
		currentWave++;
		if(currentWave < waves.Count)
			SpawnEnemies();
		else
		{
		    foreach (Text text in enemiesRemainigText)
                text.text = "GAME OVER!";
		}
	    waveComplete = false;
	}
    public IEnumerator SpawnFirstWaveWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SpawnEnemies();
    }

    public void SpawnEnemies()
	{
	    if (!isServer)
	        return;

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

        foreach (Text text in enemiesRemainigText)
        {
            if (text == null)
            {
                enemiesRemainigText.TrimExcess();
                continue;
            }
            text.text = enemyCount.ToString();
        }

    }

    
}

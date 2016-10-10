using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum	EnemyType
{
	Basic,
	Alpha,
	Beta
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

public class GameManager : MonoBehaviour {

	public List<WaveDefinition> waves = new List<WaveDefinition>();

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

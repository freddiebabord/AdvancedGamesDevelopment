﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Room : MonoBehaviour {

	public List<Collider> colliders = new List<Collider>();
	public List<EnemyBase> enemies = new List<EnemyBase>();
	public List<NetworkedFirstPersonController> players = new List<NetworkedFirstPersonController>();
	public int enemyCount { get{ return enemies.Count; } }
	public int playerCount { get{ return players.Count; } }
	public List<Collider> doors = new List<Collider>();

	// Use this for initialization
	void Start () {
		colliders = GetComponentsInChildren<Collider>().ToList();
		doors = colliders.FindAll(x => x.gameObject.tag == "Door").ToList();
	}	

	public void OnTriggerEnter(Collider other)
	{
		if(other.GetComponent<EnemyBase>())
		{
			if(!enemies.Contains(other.GetComponent<EnemyBase>()))
				enemies.Add(other.GetComponent<EnemyBase>());
		}
		else if(other.GetComponent<NetworkedFirstPersonController>())
		{
			if(!players.Contains(other.GetComponent<NetworkedFirstPersonController>()))
				players.Add(other.GetComponent<NetworkedFirstPersonController>());
		}
	}

	public void OnTriggerExit(Collider other)
	{
		if(other.GetComponent<EnemyBase>())
		{
			enemies.Remove(other.GetComponent<EnemyBase>());
			enemies.TrimExcess();
		}
		else if(other.GetComponent<NetworkedFirstPersonController>())
		{
			players.Remove(other.GetComponent<NetworkedFirstPersonController>());
		}
	}
}

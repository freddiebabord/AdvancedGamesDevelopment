using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;
using System.Collections;

[RequireComponent (typeof(NavMeshAgent))]

public class GhostBehaviour: MonoBehaviour {

    public float maxHealth = 100f;
    public float score = 500f;
    public float movementSpeed = 5f;
    public float fearPresence = 10f;
    public Transform ghostTarget;

    float currentHealth;
    Dictionary<int,float> damageFromPlayers = new Dictionary<int,float>();
    NavMeshAgent agent;
    

    // Use this for initialization
    void Start ()
    {
        //agent = GetComponent<NavMeshAgent>();
        //agent.destination = ghostTarget.position;
	}
	
	// Update is called once per frame
	void Update ()
    {

	}

    public void TakeDamage(int id)
    {
        if(!damageFromPlayers.ContainsKey(id))
            damageFromPlayers.Add(id, 0);
        damageFromPlayers[id] += 5;
        print("PlayerID: " + id + "\nCurrent Damage: " + damageFromPlayers[id]);
    }
}

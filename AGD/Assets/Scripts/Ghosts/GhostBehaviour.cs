using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;
using System.Collections;

[RequireComponent(typeof(NavMeshAgent))]

public class GhostBehaviour : NetworkBehaviour
{
    public float maxHealth = 100f;
    public float score = 500f;
    public float movementSpeed = 5f;
    public float fearPresence = 10f;
    public Transform ghostTarget;

    float currentHealth;
    NavMeshAgent agent;
    List<float> damageFromPlayers = new List<float>();

    // Use this for initialization
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        //agent.destination = ghostTarget.position;
        agent.speed = movementSpeed;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(int id, float dmg)
    {
        Cmd_TakeDamage(id, dmg);
    }

    [Command]
    public void Cmd_TakeDamage(int id, float dmg)
    {
        while (damageFromPlayers.Count - 1 <= id)
            damageFromPlayers.Add(0.0f);
        Rpc_TakeDamage(id, dmg);
    }

    [ClientRpc]
    public void Rpc_TakeDamage(int id, float dmg)
    {
        while (damageFromPlayers.Count - 1 <= id)
            damageFromPlayers.Add(0.0f);
        damageFromPlayers[id] += dmg;
        print("PlayerID: " + id + "\nCurrent Damage: " + damageFromPlayers[id]);
    }

}
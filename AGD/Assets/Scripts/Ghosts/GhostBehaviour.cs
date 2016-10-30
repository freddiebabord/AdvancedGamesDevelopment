using UnityEngine;
using UnityEngine.Networking;
using System;
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
    public GameObject frustumPrefab;

    [SyncVar]
    float currentHealth;

    NavMeshAgent agent;

    List<float> damageFromPlayers = new List<float>();

    // Use this for initialization
    void Start()
    {
        try
        {
            if (ghostTarget)
            {
                agent = GetComponent<NavMeshAgent>();
                agent.destination = ghostTarget.position;
                agent.speed = movementSpeed;
            }
        }
        catch (NullReferenceException)
        {
            print("<color=red>Ghost Target not set!</color>");
        }

        GameObject frustumChild = Instantiate(frustumPrefab,transform.GetChild(0).position,transform.GetChild(0).rotation) as GameObject;
        frustumChild.GetComponent<Frustum>().parentNetID = netId;
        NetworkServer.Spawn(frustumChild);
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
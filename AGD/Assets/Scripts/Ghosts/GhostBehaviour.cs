using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Collections.Generic;
using System.Collections;

[RequireComponent(typeof(NavMeshAgent))]

public class GhostBehaviour : NetworkBehaviour
{
    public float maxHealth = 100f;
    public float movementSpeed = 5f;
    public float fearPresence = 10f;
    public float score = 100f;
    public Transform ghostTarget;
    public GameObject frustumPrefab;
    [Tooltip("None: Ghost is always Aggravated.\nVisible: Ghost will attack on sight or when attacked.\nAttacking: Ghost will retaliate when attacked.")]
    public PlayerState ghostTrigger;
    [Tooltip("True: The Ghost will never attack.\nFalse: The Ghost can attack.")]
    public bool isPeaceful = false;
    EnemyBase enmBase;

    [SyncVar]
    float currentHealth;

    NavMeshAgent agent;

    List<float> damageFromPlayers = new List<float>();
    private GameObject m_networkFrustum;
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
        currentHealth = maxHealth;

        //TODO: maybe change to add isServer?
        m_networkFrustum = Instantiate(frustumPrefab,transform.GetChild(0).position,transform.GetChild(0).rotation) as GameObject;
        m_networkFrustum.GetComponent<Frustum>().parentNetID = netId;
        NetworkServer.Spawn(m_networkFrustum);
        m_networkFrustum.GetComponent<Frustum>().PostStart();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isServer)
            return;
        if (currentHealth <= 0)
        {
            Cmd_RemoveRadarObj();
        }
    }

    public void TakeDamage(int id, float dmg)
    {
        Debug.Log(id + " " + dmg);
        Cmd_TakeDamage(id, dmg);
    }

    [Command]
    public void Cmd_TakeDamage(int id, float dmg)
    {
        while (damageFromPlayers.Count - 1 <= id)
            damageFromPlayers.Add(0.0f);
        currentHealth -= dmg;
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

    [Command]
    private void Cmd_DestoryGhost(GameObject ghost)
    {
        GameManager.instance.DestroyEnemy();
        NetworkServer.Destroy(ghost);
    }

    [Command]
    void Cmd_RemoveRadarObj()
    {
        Rpc_RemoveRadarObjFromClients();
    }

    [ClientRpc]
    void Rpc_RemoveRadarObjFromClients()
    {
        Radar[] players = FindObjectsOfType<Radar>();

        for (int i = 0; i < players.Length; i++)
        {
            players[i].RemoveRadarObject(gameObject);
        }
        if(isServer)
            Cmd_DestoryGhost(gameObject);
    }

}
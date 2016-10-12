using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;
using System.Collections;
using System.Xml.Xsl;
using UnityEngine.Networking.NetworkSystem;

[RequireComponent(typeof(NavMeshAgent))]

public class GhostBehaviour : NetworkBehaviour
{
    NetworkClient client;

    public float maxHealth = 100f;
    public float score = 500f;
    public float movementSpeed = 5f;
    public float fearPresence = 10f;
    public Transform ghostTarget;

    float currentHealth;
    //Dictionary<int, float> damageFromPlayers = new Dictionary<int, float>();
    NavMeshAgent agent;
    SyncListFloat damageFromPlayers = new SyncListFloat();

    // Use this for initialization
    void Start()
    {
        //agent = GetComponent<NavMeshAgent>();
        //agent.destination = ghostTarget.position;
        client = NetManager.singleton.client;
        client.RegisterHandler(MyMsgType.Dmg, OnDamage);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(int id)
    {
        if (!isServer)
            return;

        while (damageFromPlayers.Count < id)
            damageFromPlayers.Add(0.0f);
        damageFromPlayers[id] += 5;
        print("PlayerID: " + id + "\nCurrent Damage: " + damageFromPlayers[id]);
        SendDamage(id, 5);
    }

    //public void DEBUG()
    //{
    //    foreach (KeyValuePair<int, float> kvp in damageFromPlayers)
    //    {
    //        Debug.Log("Player ID: " + kvp.Key + "\nDamage Dealt: " + kvp.Value);
    //    }
    //}


    public void SendDamage(int id, float damage)
    {
        DamageMessage message = new DamageMessage();
        message.damage = damage;
        message.id = id;
        NetworkServer.SendToAll(MyMsgType.Dmg, message);
    }

    public void OnDamage(NetworkMessage netMsg)
    {
        DamageMessage msg = netMsg.ReadMessage<DamageMessage>();
        damageFromPlayers[msg.id] += msg.damage;
    }

}
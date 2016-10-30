using System;
using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public enum ItemMap { Higher, Lower, Same, Nullus };

public class Pickup : NetworkBehaviour
{
    protected bool respawning = false;
    protected Renderer renderer;
    protected Collider collider;
    public PickupType pickupType;
    [SerializeField] protected Color minimapColor;
    public bool firstPass = false;

    public ItemMap itemMap = ItemMap.Nullus;

    void Start()
    {
        renderer = GetComponent<Renderer>();
        collider = GetComponent<Collider>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (other.GetComponent<NetworkedThirdPersonCharacter>().isLocalPlayer)
            {
                PickupLogic(other.gameObject);
                Cmd_RemoveRadarObj();
            }
            if (!respawning)
                StartCoroutine( RespawnTimer() );
        }
    }

    protected virtual void PickupLogic(GameObject player)
    {
        throw new NotImplementedException();
    }

    IEnumerator RespawnTimer()
    {
        respawning = true;
        renderer.enabled = false;
        collider.enabled = false;
        yield return new WaitForSeconds(20);
        renderer.enabled = true;
        collider.enabled = true;
        respawning = false;
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
    }
}

using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PickUpBase : NetworkBehaviour {

    
    public ItemMap itemMap = ItemMap.Nullus;
    public bool firstPass = false;

    public GameObject player;

    bool alreadyDestroyed = false;
    protected bool respawning = false;
    protected Renderer renderer;
    protected Collider collider;
    public PickupType pickupType;
    public bool ShowOnMinimap {
        get { return !alreadyDestroyed; } }
    void Start()
    {
        renderer = GetComponent<Renderer>();
        collider = GetComponent<Collider>();
    }

    public void Triggered()
    {
        // if (isLocalPlayer)
        // {
        //Cmd_ServerTrigger();
        ApplyEffect();

        if (!alreadyDestroyed)
        {
            DestroyObject();
            alreadyDestroyed = true;
        }

        // }
    }
    


    public void DestroyObject()
    {
        Radar[] players = FindObjectsOfType<Radar>();

        StartCoroutine( RespawnTimer());
        if(isLocalPlayer)
            Cmd_RemoveRadarObj();
    }

    public virtual void ApplyEffect()
    {
        
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
        alreadyDestroyed = false;
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
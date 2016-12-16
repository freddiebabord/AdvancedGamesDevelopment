using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public enum ItemMap { Higher, Lower, Same, Nullus };

public class PickUpBase : NetworkBehaviour {

    
    public ItemMap itemMap = ItemMap.Nullus;
    public bool firstPass = false;

    public GameObject player;

    bool alreadyDestroyed = false;
    protected bool respawning = false;
    protected Renderer m_renderer;
    protected Collider m_collider;
    public bool ShowOnMinimap {
        get { return !alreadyDestroyed; } }
    public PickupType pickupType;

    void Start()
    {
        m_renderer = GetComponent<Renderer>();
        m_collider = GetComponent<Collider>();
        for(int i = 0; i < GameManager.instance.RadarHelper.Count; ++i)
            GameManager.instance.RadarHelper[i].RegisterPickup(this);
    }

    void OnDestroy()
    {
        for(int i = 0; i < GameManager.instance.RadarHelper.Count; ++i)
            GameManager.instance.RadarHelper[i].DeregisterPickup(this);
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
        m_renderer.enabled = false;
        m_collider.enabled = false;
        yield return new WaitForSeconds(20);
        m_renderer.enabled = true;
        m_collider.enabled = true;
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
using UnityEngine;
using System.Collections;
using UnityEngine.Networking;



public class PickUpBase : NetworkBehaviour {

    
    

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
        m_renderer = GetComponentInChildren<Renderer>();
        m_collider = GetComponent<Collider>();
        GameManager.instance.RegisterPickupToRadarHelper(this);
    }

    void FixedUpdate()
    {
        GameManager.instance.RegisterPickupToRadarHelper(this);
    }

    void OnDestroy()
    {
        GameManager.instance.DeRegisterPickupToRadarHelper(this);
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
        GameManager.instance.DeRegisterPickupToRadarHelper(this);
    }

    
}
using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PickUpBase : NetworkBehaviour {

    public enum ItemMap { Higher, Lower, Same, Nullus };
    public ItemMap itemMap = ItemMap.Nullus;
    public bool firstPass = false;

    public GameObject player;

    bool alreadyDestroyed = false;

    public void Triggered()
    {
        // if (isLocalPlayer)
        // {
        //Cmd_ServerTrigger();
        ApplyEffect();

        if(!alreadyDestroyed)
        {
            DestroyObject();
            alreadyDestroyed = true;
        }
            
       // }
    }

    //[Command]
    //void Cmd_ServerTrigger()
    //{
    //    Rpc_RemoveRadarObjFromClients();
    //    //DestroyObject();
    //}

    //[ClientRpc]
    //void Rpc_RemoveRadarObjFromClients()
    //{
    //    Radar[] players = FindObjectsOfType<Radar>();

    //    for (int i = 0; i < players.Length; i++)
    //    {
    //        players[i].RemoveRadarObject(gameObject);
    //    }
    //}


    public void DestroyObject()
    {
        Radar[] players = FindObjectsOfType<Radar>();

        for (int i = 0; i < players.Length; i++)
        {
            players[i].RemoveRadarObject(this.gameObject);
        }
      //  NetworkServer.Destroy(this.gameObject);
    }

    public virtual void ApplyEffect()
    {
        
    }

}

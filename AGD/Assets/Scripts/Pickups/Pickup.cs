using System;
using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Pickup : NetworkBehaviour
{
    protected bool respawning = false;
    protected Renderer renderer;
    protected Collider collider;

    void Start()
    {
        renderer = GetComponent<Renderer>();
        collider = GetComponent<Collider>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if(other.GetComponent<NetworkedThirdPersonCharacter>().isLocalPlayer)
                PickupLogic(other.gameObject);
            if (!respawning)
                respawnTimer();
        }
    }

    protected virtual void PickupLogic(GameObject player)
    {
        throw new NotImplementedException();
    }

    IEnumerator respawnTimer()
    {
        respawning = true;
        renderer.enabled = false;
        collider.enabled = false;
        yield return new WaitForSeconds(20);
        renderer.enabled = true;
        collider.enabled = true;
        respawning = false;
    }
}

using UnityEngine;
using System.Collections;

public class GhostTeleport : MonoBehaviour {

    public float fleeTimer = 10f;
    public float cooldownTimer = 3f;
    public float playerPresenceRadius = 1.5f;

    float startFleeTimer;
    float startCooldownTimer;
    int playersinRange;
    int maxPlayers;

	// Use this for initialization
	void Start ()
    {
        maxPlayers = FindObjectOfType<NetManager>().maxPlayers;
        ResetFleeTimer();
        ResetCooldownTimer();
	}
	
	// Update is called once per frame
	void Update()
    {
        playersinRange = CheckforPlayers();
        print(playersinRange);
        
        if (playersinRange > 0 && startFleeTimer == -1)
        {
            startFleeTimer = Time.time;
        }
	}

    void FixedUpdate()
    {
        if (playersinRange > 0)
        {
            print(Time.time - startFleeTimer);
            if (Time.time - startFleeTimer > (fleeTimer - (((fleeTimer / 2) / (maxPlayers - 1)) * (playersinRange - 1))))
            {
                transform.gameObject.SetActive(false);

            }
        }
    }

    void ResetFleeTimer()
    {
        startFleeTimer = -1;
    }

    void ResetCooldownTimer()
    {
        startCooldownTimer = -1;
    }

    int CheckforPlayers()
    {
        int players = 0;
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, playerPresenceRadius);
        foreach (Collider collider in hitColliders)
        {
            //print(collider);
            players = (collider.gameObject.layer == 8) ? (players += 1) : players;
        }
        return players;
    }
}

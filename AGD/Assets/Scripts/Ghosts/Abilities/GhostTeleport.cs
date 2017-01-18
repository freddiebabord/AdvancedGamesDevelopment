using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEngine.Networking;

public class GhostTeleport : NetworkBehaviour {

    public float fleeTimer = 10f;
    public float cooldownTimer = 3f;
    public float playerPresenceRadius = 5f;

    GhostBehaviour ghostBehaviour;
    RoomCollection roomCollection;
    Animator animator;
    float startFleeTimer;
    float startCooldownTimer;
    int playersinRange;
    int maxPlayers;
    bool isTeleporting;
    Vector3 velocity;
    Vector3 scale;

	// Use this for initialization
	void Start ()
    {
        maxPlayers = FindObjectOfType<NetManager>().maxPlayers;
        ghostBehaviour = GetComponent<GhostBehaviour>();
        roomCollection = roomCollection = FindObjectOfType<RoomCollection>();
        animator = GetComponentInChildren<Animator>();
        ResetFleeTimer();
        ResetCooldownTimer();
        scale = transform.localScale;
	}
	
	// Update is called once per frame
	void Update()
	{
	    if (!isServer)
	        return;
        
        if (playersinRange > 0 && startFleeTimer <= -0.5)
        {
            startFleeTimer = Time.time;
        }
	}

    private float teleportTime = 0;
    public float maxTeleportTime = 1.0f;

    void FixedUpdate()
    {
        if (isTeleporting)
        {
            teleportTime += Time.deltaTime;
            transform.localScale = Vector3.Lerp(scale, Vector3.zero, teleportTime / maxTeleportTime);
           // transform.localScale = Vector3.SmoothDamp(transform.localScale, Vector3.zero, ref velocity, 0.01f);
            if (transform.localScale.magnitude <= 0.001f)
            {
                Teleport();
            }
        }
        if (playersinRange > 0)
        {
            ghostBehaviour.pauseMovement = true;
            animator.SetBool("Player In Range", true);
            if (Time.time - startFleeTimer > (fleeTimer - (((fleeTimer / 2) / (maxPlayers - 1)) * (playersinRange - 1))))
            {
                animator.SetBool("Scared", true);
                animator.SetBool("Player In Range", false);
            }
        }

        //if(isServer)
        //    playersinRange = CheckforPlayers();
    }

    public void StartTeleport()
    {
        isTeleporting = true;
        ghostBehaviour.pauseMovement = true;
        teleportTime = 0.0f;
    }

    void Teleport()
    {
        transform.position = roomCollection.GetRandomPositionInRoom();
        transform.localScale = scale;
        isTeleporting = false;
        ghostBehaviour.pauseMovement = false;
        animator.SetBool("Scared", false);
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
        players = hitColliders.Count(x => x.gameObject.CompareTag("Player"));
        //foreach (Collider collider in hitColliders)
        //{
        //    //print(collider);
        //    players = (collider.gameObject.tag == "Player") ? (players += 1) : players;
        //}
        return players;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Players in range: " +  playersinRange);
            playersinRange++;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            playersinRange--;
    }
}

using UnityEngine;
using System.Collections;

public class GhostTeleport : MonoBehaviour {

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
        roomCollection = roomCollection = FindObjectOfType<RoomCollection>() as RoomCollection;
        animator = GetComponentInChildren<Animator>();
        ResetFleeTimer();
        ResetCooldownTimer();
        scale = transform.localScale;
	}
	
	// Update is called once per frame
	void Update()
    {
        playersinRange = CheckforPlayers();
        
        if (playersinRange > 0 && startFleeTimer == -1)
        {
            startFleeTimer = Time.time;
        }
	}

    void FixedUpdate()
    {
        if (isTeleporting)
        {
            ghostBehaviour.pauseMovement = true;
            transform.localScale = Vector3.SmoothDamp(transform.localScale, Vector3.zero, ref velocity, 0.01f);
            if (Vector3.Distance(transform.localScale, Vector3.zero) < 0.1f)
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
    }

    public void StartTeleport()
    {
        isTeleporting = true;
    }

    void Teleport()
    {
        transform.position = roomCollection.GetRandomPositionInRoom();
        transform.localScale = scale;
        isTeleporting = false;
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
        foreach (Collider collider in hitColliders)
        {
            //print(collider);
            players = (collider.gameObject.tag == "Player") ? (players += 1) : players;
        }
        return players;
    }
}

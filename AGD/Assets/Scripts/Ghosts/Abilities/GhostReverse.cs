using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Networking;
using UnityStandardAssets.Characters.ThirdPerson;

public class GhostReverse : NetworkBehaviour {

	public float screamRadius = 5f;
	public float screamTime = 2f;
	public float cooldownTimer = 2f;

    private float currentScreenTime;

    GhostBehaviour ghostBehaviour;
    RoomCollection roomCollection;
    Animator animator;
    float startFleeTimer;
    int playersinRange;
    int maxPlayers;
    bool isScreaming;
    Vector3 velocity;
    public AudioSource audioSource;

    // Use this for initialization
    void Start()
    {
        maxPlayers = FindObjectOfType<NetManager>().maxPlayers;
        ghostBehaviour = GetComponent<GhostBehaviour>();
        roomCollection = roomCollection = FindObjectOfType<RoomCollection>();
        animator = GetComponentInChildren<Animator>();
        ResetFleeTimer();
        ResetCooldownTimer();
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
    
    void FixedUpdate()
    {
        if (isScreaming)
        {
            currentScreenTime += Time.deltaTime;
          //  transform.localScale = Vector3.Lerp(scale, Vector3.zero, teleportTime / maxTeleportTime);
            // transform.localScale = Vector3.SmoothDamp(transform.localScale, Vector3.zero, ref velocity, 0.01f);
            if (currentScreenTime >= screamTime)
            {
                Teleport();
            }
        }
        if (playersinRange > 0)
        {
            ghostBehaviour.pauseMovement = true;
            //animator.SetBool("Player In Range", true);
            if (Time.time - startFleeTimer > (screamTime - (((screamTime / 2) / (maxPlayers - 1)) * (playersinRange - 1))))
            {
                StartTeleport();
            }
        }

        //if(isServer)
        //    playersinRange = CheckforPlayers();
    }

    public void StartTeleport()
    {
        isScreaming = true;
        ghostBehaviour.pauseMovement = true;
        currentScreenTime = 0.0f;
        animator.SetBool("Screaming", true);
        audioSource.Play();
        ReversePlayerControls();
    }

    void Teleport()
    {
        //transform.position = roomCollection.GetRandomPositionInRoom();
       // transform.localScale = scale;
        isScreaming = false;
        ghostBehaviour.pauseMovement = false;
        audioSource.Stop();
        animator.SetBool("Screaming", false);
    }

    void ResetFleeTimer()
    {
        startFleeTimer = -1;
    }

    void ResetCooldownTimer()
    {
        currentScreenTime = -1;
    }

    int CheckforPlayers()
    {
        int players = 0;
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, screamRadius);
        players = hitColliders.Count(x => x.gameObject.CompareTag("Player"));
        //foreach (Collider collider in hitColliders)
        //{
        //    //print(collider);
        //    players = (collider.gameObject.tag == "Player") ? (players += 1) : players;
        //}
        return players;
    }

    private void ReversePlayerControls()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, screamRadius);
        var hc = hitColliders.ToList().FindAll(x => x.GetComponent<NetworkedThirdPersonCharacter>() != null);
        for (int i = 0; i < hc.Count; ++i)
            hc[i].GetComponent<NetworkedThirdPersonCharacter>().ReversedControls = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Players in range: " + playersinRange);
            playersinRange++;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            playersinRange--;
    }
}

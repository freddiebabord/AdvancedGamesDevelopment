using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;
using System.Collections;

public class PlayerData
{
    public PlayerData(Transform player)
    {
        playerTransform = player;
    }
    public int CalculatePriority()
    {
        priority = (int)state + distanceADJ;
        return priority;
    }

    public PlayerState state = PlayerState.None;
    public int distanceADJ = 0;
    public int priority = 0;
    public int firstIndex = 0;
    public Transform playerTransform;
}

public class GhostBehaviour : NetworkBehaviour
{
    public float maxHealth = 100f;
    public Vector2 waitTime = new Vector2(3f, 5f);
    public float movementSpeed = 5f;
    public float rotationSpeed = 10f;
    public float acceleration = 0.1f;
    public int fearPresence = 100;
    public float targetOffset = 0.1f;
    public float score = 100f;
    public Transform ghostTarget;
    public Vector3 goalPosition;
    public GameObject frustumPrefab;
    [Tooltip("None: Ghost is always Aggravated.\nVisible: Ghost will attack on sight or when attacked.\nAttacking: Ghost will retaliate when attacked.")]
    public PlayerState ghostTrigger;
    [Tooltip("True: The Ghost will never attack.\nFalse: The Ghost can attack.")]
    public bool isPeaceful = false;
    public ObjectPool damageTextPool;
    public bool pauseMovement;
    public float CurrentHealth { get { return currentHealth; } }

    [SyncVar]
    float currentHealth;
    Animator animator;
    float waitTimeStart;
    float chosenWaitTime;
    bool waiting;
    int targetKey;
    Dictionary<int, PlayerData> knownPlayers = new Dictionary<int, PlayerData>();
    List<float> damageFromPlayers = new List<float>();
    Vector3 velocity;
    Vector3 interestVector;
    GhostCharge charge;
    RoomCollection roomCollection;

    // Use this for initialization
    void Start()
    {
        roomCollection = FindObjectOfType<RoomCollection>() as RoomCollection;
        GameManager.instance.RegisterEnemyToRadarHelper(this);
        animator = GetComponentInChildren<Animator>();
        currentHealth = maxHealth;
        Frustum frustum = GetComponent<Frustum>();
        //m_networkFrustum = frustum.gameObject;
        //TODO: maybe change to add isServer?
        //if (isServer) {
        //m_networkFrustum = Instantiate (frustumPrefab, transform.GetChild (0).position, transform.GetChild (0).rotation) as GameObject;

        //NetworkServer.Spawn (m_networkFrustum);
        //m_networkFrustum.transform.parent = transform.GetChild(0).transform;
        frustum.PostStart();
        //}
        damageTextPool = GetComponent<ObjectPool>();
        charge = GetComponent<GhostCharge>();
        if (!isServer)
            return;
        Rpc_SetTarget(roomCollection.GetRandomPositionInRoom());
    }

    void OnDestroy()
    {
        GameManager.instance.DeRegisterEnemyToRadarHelper(this);
    }

    void Update()
    {
        pauseMovement = false;
        if (waiting && Time.time - waitTimeStart > chosenWaitTime)
        {
            waiting = false;
            Cmd_SetTarget(roomCollection.GetRandomPositionInRoom());
        }
        Quaternion targetRotation = Quaternion.LookRotation(interestVector - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        if (ghostTarget != null)
        {
            if (Physics.Linecast(transform.position, ghostTarget.position))
            {
                if (!isServer)
                {
                    Rpc_LosePlayer();
                }
            }
        }
    }

    void LateUpdate()
    {
        animator.SetFloat("Damage", damage);
        if (!pauseMovement)
        {
            transform.position = Vector3.SmoothDamp(transform.position, goalPosition, ref velocity, acceleration, movementSpeed);
            animator.SetFloat("Speed", velocity.magnitude);
            if (Vector3.Distance(transform.position, goalPosition) < targetOffset && !waiting)
            {
                if (charge)
                {
                    if (charge.isCharging)
                    {
                        charge.CalmGhost();
                    }
                }
                waiting = true;

                if (!isServer)
                    return;

                SetHover();
            }
        }
        if (pauseMovement || waiting)
        {
            animator.SetFloat("Speed", 0);
        }
    }

    void SetHover()
    {
        chosenWaitTime = Random.Range(waitTime.x, waitTime.y);
        waitTimeStart = Time.time;
        Rpc_SetHover(chosenWaitTime, waitTimeStart);
    }

    [ClientRpc]
    void Rpc_SetHover(float time, float startTime)
    {
        chosenWaitTime = time;
        waitTimeStart = startTime;
    }

    public void Kill()
    {
        currentHealth = 0;
    }

    public void TakeDamage(GameObject player, float dmg)
    {
        //Debug.Log(id + " " + dmg);
        Cmd_TakeDamage(player, dmg);
    }

    [Command]
    public void Cmd_TakeDamage(GameObject player, float dmg)
    {
        while (damageFromPlayers.Count - 1 <= player.GetComponent<NetworkedThirdPersonCharacter>().playerID)
            damageFromPlayers.Add(0.0f);
        currentHealth -= dmg;
        if (currentHealth > 0.0f)
            Rpc_TakeDamage(player, dmg);
        else {
            for (int i = 0; i < damageFromPlayers.Count; ++i)
                GameManager.instance.PostScoreToScoreTable(i, Mathf.RoundToInt((damageFromPlayers[i] / maxHealth) * score));
            //Cmd_RemoveRadarObj();
            Cmd_DestroyGhost(player.GetComponent<NetworkedThirdPersonCharacter>().playerID);
        }
    }

    [Command]
    public void Cmd_SetTarget(Vector3 newTarget)
    {
        if (!isServer)
            return;
        Rpc_SetTarget(newTarget);
    }

    [ClientRpc]
    public void Rpc_SetTarget(Vector3 newTarget)
    {
        goalPosition = newTarget;
        interestVector = goalPosition;
    }

    [ClientRpc]
    public void Rpc_TakeDamage(GameObject player, float dmg)
    {
        if (!charge)
        {
            pauseMovement = true;
        }
        while (damageFromPlayers.Count - 1 <= player.GetComponent<NetworkedThirdPersonCharacter>().playerID)
            damageFromPlayers.Add(0.0f);
        damageFromPlayers[player.GetComponent<NetworkedThirdPersonCharacter>().playerID] += dmg;

        //print("PlayerID: " + id + "\nCurrent Damage: " + damageFromPlayers[id]);
        if (!displayingText)
            StartCoroutine(TextDisplay(dmg));
        else
            damage += damage;
    }

    private bool displayingText = false;
    private float damage = 0;
    private WaitForSeconds wfs = new WaitForSeconds(0.5f);

    IEnumerator TextDisplay(float dmg)
    {
        displayingText = true;
        damage = dmg;
        yield return wfs;
        GhostDamageText txt = damageTextPool.Spawn(transform.position + Vector3.forward * 1.5f, transform.rotation).GetComponent<GhostDamageText>();
        txt.SetDamageText(Mathf.RoundToInt(damage));
        damage = 0;
        displayingText = false;
    }

    [Command]
    private void Cmd_DestroyGhost(int playerID)
    {
        if (charge)
            GameManager.instance.PostScoreToScoreTable(playerID, charge.scoreStolen);
        GameManager.instance.DestroyEnemy();
        NetworkServer.Destroy(gameObject);
    }

    //    [Command]
    //    void Cmd_RemoveRadarObj()
    //    {
    //        Rpc_RemoveRadarObjFromClients();
    //    }

    //    [ClientRpc]
    //    void Rpc_RemoveRadarObjFromClients()
    //    {
    //        Radar[] players = FindObjectsOfType<Radar>();
    //
    //        for (int i = 0; i < players.Length; i++)
    //        {
    //            players[i].RemoveRadarObject(gameObject);
    //        }
    //            
    //    }

    void OnTriggerStay(Collider other)
    {
        if (!isServer)
            return;

        if (other.tag == "Player")
        {
            Rpc_RemoveScore(other.GetComponent<NetworkedThirdPersonCharacter>().playerID);
        }
    }

    [ClientRpc]
    void Rpc_RemoveScore(int playerID)
    {
        int pointsTaken;
        if (charge)
        {
            charge.StealPoints();

        }

        pointsTaken = charge ? charge.pointsToSteal + fearPresence : fearPresence;

        GameManager.instance.PostScoreToScoreTable(playerID, -pointsTaken);
    }

    [ClientRpc]
    void Rpc_LosePlayer()
    {
        ghostTarget = null;
    }
}
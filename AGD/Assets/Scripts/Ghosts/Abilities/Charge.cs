using UnityEngine;
using System.Collections;

public class Charge : MonoBehaviour
{

    /* Freddie:
       Anything labelled with test is for single player ONLY and WILL NOT function properly over LAN.
       If you manage to get the Frustum script to function properly then you sir, are a wizard...*/
    
    
    public Frustum ghostFrustum;
    //public LocalFrustum testFrustum;
    [Tooltip("Time before ghost can charge again.")]
    public float cooldownTimer = 2f;
    [Tooltip("Distance the ghost will charge past the player.")]
    public float chargeOverstep = 5f;
    public float chargeSpeed = 10f;
    public float chargeAccel = 10f;
    public float chargeTime = 3f;
    [Tooltip("Fear to be applied to player on collision with ghost.")]
    public float fearAdjustment = 5f;
    [Tooltip("Points stolen from player on collision with ghost.")]
    public float pointsToSteal = 300f;
    [Tooltip("Points awarded for capturing the ghost.")]
    public float pointsValue = 500f;
    [HideInInspector]
    public bool isCooldown;

    NavMeshAgent testAgent;
    //EnemyBase agent;
    float startCooldownTimer;
    float scoreStolen;
    float startSpeed;
    float startChargeTime;
    float startAccel;
    
	// Use this for initialization
	void Start ()
    {
        ghostFrustum = transform.GetComponent<Frustum>();
        //agent = GetComponent<EnemyBase>().agent;
        //testFrustum = transform.GetChild(0).GetComponentInChildren<LocalFrustum>();
        testAgent = GetComponent<NavMeshAgent>();
        startSpeed = testAgent.speed;
        startAccel = testAgent.acceleration;
	}

    void Update()
    {
        //if (ghostFrustum.isTriggered && Time.time - startCoolDownTimer > coolDownTimer)
        //ghostFrustum.isTriggered = false;
        if (!ghostFrustum)
        {
            ghostFrustum = transform.GetComponentInChildren<Frustum>();
            return;
        }

        if (ghostFrustum.isTriggered && !isCooldown && ghostFrustum.focus == null)
        {
            CalmGhost();
        }
        if (!ghostFrustum.isTriggered && isCooldown && Time.time - startCooldownTimer > cooldownTimer)
            isCooldown = false;
    }

    public void Triggered(Vector3 targetPos)
    {
        if (isCooldown)
        {
            ghostFrustum.isTriggered = false;
            return;
        }
        //Vector3 heading = transform.position - ghostFrustum.target.position;
        //float distance = heading.magnitude;
        //Vector3 direction = heading / distance;
        //agent.SetDestination(ghostFrustum.target.position + direction * chargeOverstep);
        Vector3 testHeading = targetPos - transform.position;
//        print(targetPos + (testHeading.normalized * chargeOverstep));
        testAgent.SetDestination(targetPos + (testHeading.normalized * chargeOverstep));
        testAgent.acceleration = chargeAccel;
        testAgent.speed = chargeSpeed;
    }

    public void StealPoints()
    {
        print("STOLEN POINTS!");
        scoreStolen += pointsToSteal;
        if (ghostFrustum.isTriggered)
        {
            CalmGhost();
        }
    }

    void CalmGhost()
    {
        isCooldown = true;
        ghostFrustum.isTriggered = false;
        testAgent.speed = startSpeed;
        testAgent.acceleration = startAccel;
        ghostFrustum.focus = null;
        startCooldownTimer = Time.time;
    }
}

using UnityEngine;
using System.Collections;

public class GhostCharge : MonoBehaviour
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
    public int pointsToSteal = 300;
    [HideInInspector]
    public bool isCooldown;

    GhostBehaviour ghostBehaviour;
    //EnemyBase agent;
    public float startCooldownTimer;
    public int scoreStolen;
    float startSpeed;
    float startChargeTime;
    float startAccel;
    public bool isCharging;
    
	// Use this for initialization
	void Start ()
    {
        ghostFrustum = transform.GetComponentInChildren<Frustum>();
        ghostBehaviour = GetComponent<GhostBehaviour>();
        startSpeed = ghostBehaviour.movementSpeed;
        startAccel = ghostBehaviour.acceleration;
	}

    void Update()
    {
        if (ghostFrustum.isTriggered && !isCooldown && ghostBehaviour.ghostTarget == null)
            CalmGhost();
        //if (!ghostFrustum.isTriggered && isCooldown && Time.time - startCooldownTimer > cooldownTimer)
        //    isCooldown = false;
    }

    public void Triggered(Vector3 targetPos)
    {
        if (isCooldown)
        {
            ghostFrustum.isTriggered = false;
            return;
        }
        if (isCharging)
            return;

        //Vector3 heading = transform.position - ghostFrustum.target.position;
        //float distance = heading.magnitude;
        //Vector3 direction = heading / distance;
        //agent.SetDestination(ghostFrustum.target.position + direction * chargeOverstep);
        Vector3 testHeading = targetPos - transform.position;
//        print(targetPos + (testHeading.normalized * chargeOverstep));
        ghostBehaviour.Cmd_SetTarget(targetPos + (testHeading.normalized * chargeOverstep));
        ghostBehaviour.acceleration = chargeAccel;
        ghostBehaviour.movementSpeed = chargeSpeed;
        isCharging = true;
    }

    public void StealPoints()
    {
        scoreStolen += pointsToSteal;
        if (ghostFrustum.isTriggered)
        {
            CalmGhost();
        }
    }

   public void CalmGhost()
    {
        isCharging = false;
        isCooldown = true;
        ghostFrustum.isTriggered = false;
        ghostBehaviour.movementSpeed = startSpeed;
        ghostBehaviour.acceleration = startAccel;
        ghostBehaviour.ghostTarget = null;
        StartCoroutine(CooldownWait());
    }

    IEnumerator CooldownWait()
    {
        isCooldown = true;
        yield return new WaitForSeconds(cooldownTimer);
        isCooldown = false;
    }
}

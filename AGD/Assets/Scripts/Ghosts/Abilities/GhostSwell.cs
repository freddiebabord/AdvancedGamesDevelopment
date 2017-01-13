using UnityEngine;
using System.Collections;

public class GhostSwell : MonoBehaviour {

    Animator animator;
    Frustum ghostFrustum;
    [Tooltip("Time before ghost can swell up again.")]
    public float cooldownTimer = 2f;
    [Tooltip("Time before ghost can return to normal again.")]
    public float swellTimer = 2f;
    [Tooltip("Margin of error for float calculations")]
    public float targetOffset = 0.1f;
    [Tooltip("Scale to increase the ghost model by.")]
    public float ghostScale = 2f;
    [Tooltip("Time taken to swell.")]
    public float scaleTime = 1f;
    [HideInInspector]
    public bool isCooldown;

    float startCooldownTimer;
    float startSwellTimer;
    Vector3 velocity = Vector3.zero;
    Vector3 startScale;
    Vector3 endScale;
    Vector3 targetScale;
    bool isAngry;

	// Use this for initialization
	void Start ()
    {
        ghostFrustum = transform.GetComponentInChildren<Frustum>();
        startScale = transform.localScale;
        endScale = startScale * ghostScale;
        targetScale = startScale;
        animator = GetComponentInChildren<Animator>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (ghostFrustum.isTriggered && !isCooldown && ghostFrustum.focus == null)
            CalmGhost();
        if (!ghostFrustum.isTriggered && isCooldown && Time.time - startCooldownTimer > cooldownTimer)
            isCooldown = false;

        transform.localScale = Vector3.SmoothDamp(transform.localScale, targetScale, ref velocity, scaleTime);

        if (Vector3.Distance(transform.localScale, targetScale) < targetOffset)
        {
            animator.SetBool("Growing", false);
            if ((Time.time - startSwellTimer > swellTimer))
                targetScale = startScale;
        }
	}
    public void Triggered()
    {
        if (isCooldown)
        {
            ghostFrustum.isTriggered = false;
            return;
        }

        if (!isAngry)
        {
            isAngry = true;
            animator.SetBool("Growing", true);
            targetScale = endScale;
        }
        startSwellTimer = Time.time;
    }

    void CalmGhost()
    {
        isCooldown = true;
        isAngry = false;
        targetScale = startScale;
        ghostFrustum.isTriggered = false;
        ghostFrustum.focus = null;
        startCooldownTimer = Time.time;
    }
}

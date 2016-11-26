using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.PostProcessing;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;
using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Animator))]
public class NetworkedThirdPersonCharacter : NetworkBehaviour
{ 
	[SerializeField] float m_MovingTurnSpeed = 360;
	[SerializeField] float m_StationaryTurnSpeed = 180;
	[SerializeField] float m_JumpPower = 12f;
	[Range(1f, 4f)][SerializeField] float m_GravityMultiplier = 2f;
	[SerializeField] float m_RunCycleLegOffset = 0.2f; //specific to the character in sample assets, will need to be modified to work with others
	[SerializeField] float m_MoveSpeedMultiplier = 1f;
	[SerializeField] float m_AnimSpeedMultiplier = 1f;
	[SerializeField] float m_GroundCheckDistance = 0.1f;
    [SerializeField] public MouseLook m_MouseLook;

    Rigidbody m_Rigidbody;
	Animator m_Animator;
	bool m_IsGrounded;
	float m_OrigGroundCheckDistance;
	const float k_Half = 0.5f;
	float m_TurnAmount;
	float m_ForwardAmount;
	Vector3 m_GroundNormal;
	float m_CapsuleHeight;
	Vector3 m_CapsuleCenter;
	CapsuleCollider m_Capsule;
	bool m_Crouching;
    [SyncVar] public int playerID;
	[SyncVar] public string playerName;
	[SyncVar] public Color playerColour;
	[SyncVar] public float playerHealth;
    [SyncVar] public int playerScore;

    LineRenderer lineRenderer;
	public Transform weaponSpawnPoint;
	private Camera m_Camera;
    public GameObject tempDecalParticleSystem, muzzleParticleSystem;
    private int m_uNetID;

    private bool spawningPS = false;
    private Transform spawnedParticleSystem;
    public Text enemiesRemainingText;

    private Material beamMaterial;
    [Space(10)]
    [Header("Weapon Variables")]
    public float maxWeaponFireTime = 10.0f;
    private float currentWeaponFireTime = 0.0f;
    public float weaponRechargeRate = 0.75f;
    private bool isFiring = false;
    public CustomLight beamLight;
	public Decal impactDecal;
    private Material weaponRechargeRenderer;
    public GameObject weaponRechargeIndicator;
    private Material beamRenderer;

    void Start()
	{
		m_Animator = GetComponent<Animator>();
		m_Rigidbody = GetComponent<Rigidbody>();
		m_Capsule = GetComponent<CapsuleCollider>();
		m_CapsuleHeight = m_Capsule.height;
		m_CapsuleCenter = m_Capsule.center;
        m_Camera = GetComponentInChildren<Camera>();

        m_Rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
		m_OrigGroundCheckDistance = m_GroundCheckDistance;
		lineRenderer = GetComponentInChildren<LineRenderer>();
        m_MouseLook.Init(transform, m_Camera.transform);

        GetComponentInChildren<Renderer>().material.color = playerColour;

        playerScore = 0;

        spawnedParticleSystem = ((GameObject)Instantiate(tempDecalParticleSystem, transform.position, Quaternion.identity)).transform;
        ParticleSystem rootMuzzleParticleSystem = muzzleParticleSystem.GetComponent<ParticleSystem>();
        rootMuzzleParticleSystem.Stop(true);
        StopParticleSystem();
        weaponRechargeRenderer = weaponRechargeIndicator.GetComponent<Renderer>().material;
        beamRenderer = lineRenderer.GetComponent<Renderer>().material;
        beamRenderer.SetColor("_Colour", playerColour * 20);

        rootMuzzleParticleSystem.startColor = playerColour;
        var cbs = rootMuzzleParticleSystem.colorBySpeed;
        var grad = new ParticleSystem.MinMaxGradient();
        grad.colorMin = playerColour;
        grad.colorMax = Color.white;
        cbs.color = grad;
        ParticleSystem sparkspsRootMzzle = rootMuzzleParticleSystem.gameObject.transform.FindChild("Sparks").GetComponent<ParticleSystem>();
        sparkspsRootMzzle.startColor = playerColour;
        var cbssparks = sparkspsRootMzzle.colorBySpeed;
        cbssparks.color = grad;

        spawnedParticleSystem.GetComponent<ParticleSystem>().startColor = playerColour;
        cbs = spawnedParticleSystem.GetComponent<ParticleSystem>().colorBySpeed;
        grad = new ParticleSystem.MinMaxGradient();
        grad.colorMin = playerColour;
        grad.colorMax = Color.white;
        cbs.color = grad;
        sparkspsRootMzzle = spawnedParticleSystem.transform.FindChild("Sparks").GetComponent<ParticleSystem>();
        sparkspsRootMzzle.startColor = playerColour;
        cbssparks = sparkspsRootMzzle.colorBySpeed;
        cbssparks.color = grad;

        if (!isLocalPlayer)
        {
            m_Camera.gameObject.SetActive(false);
            weaponRechargeIndicator.gameObject.SetActive(false);
        }

        if (isLocalPlayer)
        {
            if (isServer)
                m_uNetID = GetComponent<NetworkIdentity>().connectionToClient.connectionId;
            else
                m_uNetID = GetComponent<NetworkIdentity>().connectionToServer.connectionId;
            
            //spawnedParticleSystem.gameObject.SetActive(false);
           
            FindObjectOfType<SplitscreenManager>().RegisterCamera(m_Camera);
            SettingsManager.instance.RegisterPostProfile(m_Camera.GetComponent<PostProcessingBehaviour>().profile);
            //var pnc = FindObjectsOfType<PlayerNameCanvas>();
            //for (var i = 0; i < pnc.Length; i++)
            //{
            //    pnc[i].targetCamera = m_Camera;
            //}
            GameManager.instance.enemiesRemainigText.Add(enemiesRemainingText);
        }

        beamMaterial = lineRenderer.material;
        GameManager.instance.players.Add(this);

	}
    public float damagePerSecond = 10;

    void Update()
    {
        if(firing)
        {
            Ray ray = new Ray(m_Camera.transform.position, weaponSpawnPoint.forward);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100))
            {
                lineRenderer.SetPosition(0, weaponSpawnPoint.position);
                lineRenderer.SetPosition(1, hit.point);
                float dist = Vector3.Distance(hit.point, weaponSpawnPoint.position);
                float halfDist = dist / 2;
                beamLight.m_TubeLength = halfDist;
                beamLight.transform.position = weaponSpawnPoint.position;
                beamLight.transform.LookAt(hit.point);
                beamLight.transform.Rotate(beamLight.transform.up, 90);
                beamLight.transform.Translate(beamLight.transform.right * -halfDist);
                beamLight.transform.Translate(beamLight.transform.forward * -halfDist);

                if (hit.transform.GetComponentInParent<GhostBehaviour>() && isLocalPlayer)
                {
                    hit.transform.GetComponentInParent<GhostBehaviour>().TakeDamage(playerID, damagePerSecond * Time.deltaTime);
                }
                else
                {
                    StartParticleSystem();
                    Vector3 norm = weaponSpawnPoint.position - hit.point;
                    norm.Normalize();
                    spawnedParticleSystem.position = hit.point + norm * 0.1f;
                }
            }
            else
            {
                StopParticleSystem();
                lineRenderer.SetPosition(0, weaponSpawnPoint.position);
                lineRenderer.SetPosition(1, weaponSpawnPoint.position + weaponSpawnPoint.forward * 100.0f);
            }
        }

        if(!isLocalPlayer)
            return;
        if (firing)
        {
            currentWeaponFireTime += Time.deltaTime;
            if (currentWeaponFireTime >= maxWeaponFireTime)
            {
                currentWeaponFireTime = maxWeaponFireTime;
                Cmd_EndFire();
            }
        }
        else
        {
            if (currentWeaponFireTime > 0)
                currentWeaponFireTime -= weaponRechargeRate*Time.deltaTime;
            else
                currentWeaponFireTime = 0;
        }
        float value = currentWeaponFireTime/maxWeaponFireTime;
        weaponRechargeRenderer.SetFloat("_CurrentOverheatValue", value);


    }

    void OnDisable()
    {
        if (GameManager.instance.players.Contains(this))
        {
            GameManager.instance.players.Remove(this);
            GameManager.instance.players.TrimExcess();
        }
    }


	public void Move(Vector3 move, bool crouch, bool jump, bool cursorLock)
	{

		// convert the world relative moveInput vector into a local-relative
		// turn amount and forward amount required to head in the desired
		// direction.
		if (move.magnitude > 1f) move.Normalize();
		move = transform.InverseTransformDirection(move);
		CheckGroundStatus();
		move = Vector3.ProjectOnPlane(move, m_GroundNormal);
	    move = transform.TransformDirection(move);
        m_TurnAmount = move.x;
		m_ForwardAmount = move.z;

		//ApplyExtraTurnRotation();

		// control and velocity handling is different when grounded and airborne:
		if (m_IsGrounded)
		{
			HandleGroundedMovement(crouch, jump);
		}
		else
		{
			HandleAirborneMovement();
		}

		ScaleCapsuleForCrouching(crouch);
		PreventStandingInLowHeadroom();

		// send input and other state parameters to the animator
		UpdateAnimator(move);
        m_MouseLook.SetCursorLock(!cursorLock);
	   
        //m_MouseLook.UpdateCursorLock();
        if(!cursorLock)
            RotateView();

	}
    

	void ScaleCapsuleForCrouching(bool crouch)
	{
		if (m_IsGrounded && crouch)
		{
			if (m_Crouching) return;
			m_Capsule.height = m_Capsule.height / 2f;
			m_Capsule.center = m_Capsule.center / 2f;
			m_Crouching = true;
		}
		else
		{
			Ray crouchRay = new Ray(m_Rigidbody.position + Vector3.up * m_Capsule.radius * k_Half, Vector3.up);
			float crouchRayLength = m_CapsuleHeight - m_Capsule.radius * k_Half;
			if (Physics.SphereCast(crouchRay, m_Capsule.radius * k_Half, crouchRayLength, Physics.AllLayers, QueryTriggerInteraction.Ignore))
			{
				m_Crouching = true;
				return;
			}
			m_Capsule.height = m_CapsuleHeight;
			m_Capsule.center = m_CapsuleCenter;
			m_Crouching = false;
		}
	}

	void PreventStandingInLowHeadroom()
	{
		// prevent standing up in crouch-only zones
		if (!m_Crouching)
		{
			Ray crouchRay = new Ray(m_Rigidbody.position + Vector3.up * m_Capsule.radius * k_Half, Vector3.up);
			float crouchRayLength = m_CapsuleHeight - m_Capsule.radius * k_Half;
			if (Physics.SphereCast(crouchRay, m_Capsule.radius * k_Half, crouchRayLength, Physics.AllLayers, QueryTriggerInteraction.Ignore))
			{
				m_Crouching = true;
			}
		}
	}


	void UpdateAnimator(Vector3 move)
	{
		// update the animator parameters
		m_Animator.SetFloat("Forward", m_ForwardAmount, 0.1f, Time.deltaTime);
		m_Animator.SetFloat("Turn", m_TurnAmount, 0.1f, Time.deltaTime);
		m_Animator.SetBool("Crouch", m_Crouching);
		m_Animator.SetBool("OnGround", m_IsGrounded);
		if (!m_IsGrounded)
		{
			m_Animator.SetFloat("Jump", m_Rigidbody.velocity.y);
		}

		// calculate which leg is behind, so as to leave that leg trailing in the jump animation
		// (This code is reliant on the specific run cycle offset in our animations,
		// and assumes one leg passes the other at the normalized clip times of 0.0 and 0.5)
		float runCycle =
			Mathf.Repeat(
				m_Animator.GetCurrentAnimatorStateInfo(0).normalizedTime + m_RunCycleLegOffset, 1);
		float jumpLeg = (runCycle < k_Half ? 1 : -1) * m_ForwardAmount;
		if (m_IsGrounded)
		{
			m_Animator.SetFloat("JumpLeg", jumpLeg);
		}

		// the anim speed multiplier allows the overall speed of walking/running to be tweaked in the inspector,
		// which affects the movement speed because of the root motion.
		if (m_IsGrounded && move.magnitude > 0)
		{
			m_Animator.speed = m_AnimSpeedMultiplier;
		}
		else
		{
			// don't use that while airborne
			m_Animator.speed = 1;
		}
	}


	void HandleAirborneMovement()
	{
		// apply extra gravity from multiplier:
		Vector3 extraGravityForce = (Physics.gravity * m_GravityMultiplier) - Physics.gravity;
		m_Rigidbody.AddForce(extraGravityForce);

		m_GroundCheckDistance = m_Rigidbody.velocity.y < 0 ? m_OrigGroundCheckDistance : 0.2f;
	}


	void HandleGroundedMovement(bool crouch, bool jump)
	{
		// check whether conditions are right to allow a jump:
		if (jump && !crouch && m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Grounded"))
		{
			// jump!
			m_Rigidbody.velocity = new Vector3(m_Rigidbody.velocity.x, m_JumpPower, m_Rigidbody.velocity.z);
			m_IsGrounded = false;
			m_Animator.applyRootMotion = false;
			m_GroundCheckDistance = 0.1f;
		}
	}

	void ApplyExtraTurnRotation()
	{
		// help the character turn faster (this is in addition to root rotation in the animation)
		float turnSpeed = Mathf.Lerp(m_StationaryTurnSpeed, m_MovingTurnSpeed, m_ForwardAmount);
		transform.Rotate(0, m_TurnAmount * turnSpeed * Time.deltaTime, 0);
	}


	public void OnAnimatorMove()
	{
		// we implement this function to override the default root motion.
		// this allows us to modify the positional speed before it's applied.
		if (m_IsGrounded && Time.deltaTime > 0)
		{
			Vector3 v = (m_Animator.deltaPosition * m_MoveSpeedMultiplier) / Time.deltaTime;

			// we preserve the existing y part of the current velocity.
			v.y = m_Rigidbody.velocity.y;
			m_Rigidbody.velocity = v;
		}
	}


	void CheckGroundStatus()
	{
		RaycastHit hitInfo;
#if UNITY_EDITOR
		// helper to visualise the ground check ray in the scene view
		Debug.DrawLine(transform.position + (Vector3.up * 0.1f), transform.position + (Vector3.up * 0.1f) + (Vector3.down * m_GroundCheckDistance));
#endif
		// 0.1f is a small offset to start the ray from inside the character
		// it is also good to note that the transform position in the sample assets is at the base of the character
		if (Physics.Raycast(transform.position + (Vector3.up * 0.1f), Vector3.down, out hitInfo, m_GroundCheckDistance))
		{
			m_GroundNormal = hitInfo.normal;
			m_IsGrounded = true;
			m_Animator.applyRootMotion = true;
		}
		else
		{
			m_IsGrounded = false;
			m_GroundNormal = Vector3.up;
			m_Animator.applyRootMotion = false;
		}
	}

    private void RotateView()
    {
        m_MouseLook.LookRotation(transform, m_Camera.transform);
    }

    public void Fire(bool fire)
	{
        if (isLocalPlayer)
        {
            //Cmd_Fire(fire, m_uNetID);
        }
		
	}

    [Command]
    public void Cmd_BeginFire()
    {
        Rpc_BeginFire();
    }

    [ClientRpc]
    void Rpc_BeginFire()
    {
        firing = true;
        lineRenderer.enabled = true;
        lineRenderer.SetPosition(0, weaponSpawnPoint.position);
        Ray ray = new Ray(m_Camera.transform.position, weaponSpawnPoint.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100))
        {
            lineRenderer.SetPosition(1, hit.point);
            float dist = Vector3.Distance(hit.point, weaponSpawnPoint.position);
            float halfDist = dist / 2;
            beamLight.m_TubeLength = halfDist;
            beamLight.transform.position = weaponSpawnPoint.position;
            beamLight.transform.LookAt(hit.point);
            beamLight.transform.Rotate(beamLight.transform.up, 90);
            beamLight.transform.Translate(beamLight.transform.right * -halfDist);
            beamLight.transform.Translate(beamLight.transform.forward * -halfDist);

            if (hit.transform.GetComponentInParent<GhostBehaviour>() && isLocalPlayer)
            {
                hit.transform.GetComponentInParent<GhostBehaviour>().TakeDamage(playerID, damagePerSecond * Time.deltaTime);
            }
            else
            {
                StartParticleSystem();
                Vector3 norm = weaponSpawnPoint.position - hit.point;
                norm.Normalize();
                spawnedParticleSystem.position = hit.point + norm * 0.1f;
            }
        }
        else
        {
            lineRenderer.SetPosition(1, weaponSpawnPoint.position + weaponSpawnPoint.forward * 100.0f);
        }
        ParticleSystem rootMuzzleParticleSystem = muzzleParticleSystem.GetComponent<ParticleSystem>();
        rootMuzzleParticleSystem.Play(true);
    }

    [Command]
    public void Cmd_EndFire()
    {
        Rpc_EndFire();
    }

    bool firing = false;
    [ClientRpc]
    void Rpc_EndFire()
    {
        firing = false;
        lineRenderer.enabled = false;
        //spawnedParticleSystem.gameObject.SetActive(false);
        beamLight.gameObject.SetActive(false);
        StopParticleSystem();
        ParticleSystem rootMuzzleParticleSystem = muzzleParticleSystem.GetComponent<ParticleSystem>();
        rootMuzzleParticleSystem.Stop(true);
    }

 //   [Command]
	//void Cmd_Fire(bool fire, int unID)
	//{
	//	Rpc_Fire(fire, unID);
	//}

	//[ClientRpc]
	//void Rpc_Fire(bool fire, int unID)
	//{
	//    isFiring = fire;
		
 //       if (!fire /*|| currentWeaponFireTime >= maxWeaponFireTime*/)
	//    {
	//        lineRenderer.gameObject.SetActive(false);
 //           //spawnedParticleSystem.gameObject.SetActive(false);
 //           beamLight.gameObject.SetActive(false);
 //           StopParticleSystem();
 //           return;
	//    }
	//	Debug.Log(fire);
 //       beamLight.gameObject.SetActive(true);
 //       //beamMaterial.SetFloat("Intensity", 1-(maxWeaponFireTime / currentWeaponFireTime));
 //       Ray ray = new Ray(m_Camera.transform.position, weaponSpawnPoint.forward);
	//	RaycastHit hit;
 //       lineRenderer.gameObject.SetActive(true);
 //       lineRenderer.SetPosition(0, weaponSpawnPoint.position);
 //       muzzleParticleSystem.position = weaponSpawnPoint.position;
 //       if (Physics.Raycast(ray, out hit, 100))
	//	{
			
	//		lineRenderer.SetPosition(1, hit.point);
	//	    float dist = Vector3.Distance(hit.point, weaponSpawnPoint.position);
	//	    float halfDist = dist/2;
	//	    beamLight.m_TubeLength = halfDist;
	//	    beamLight.transform.position = weaponSpawnPoint.position;
 //           beamLight.transform.LookAt(hit.point);
 //           beamLight.transform.Rotate(beamLight.transform.up, 90);
 //           beamLight.transform.Translate(beamLight.transform.right * -halfDist);
 //           beamLight.transform.Translate(beamLight.transform.forward * -halfDist);

 //           if (hit.transform.GetComponentInParent<GhostBehaviour>())
 //           {
 //               hit.transform.GetComponentInParent<GhostBehaviour>().TakeDamage(playerID, 5);
 //           }
 //           else
 //           {
 //               StartParticleSystem();
 //               Vector3 norm = weaponSpawnPoint.position - hit.point;
 //               norm.Normalize();
 //               spawnedParticleSystem.position = hit.point + norm * 0.1f;
 //           }
	//	}
	//	else
	//	{
	//	    StopParticleSystem();
 //           lineRenderer.SetPosition(1, weaponSpawnPoint.position + weaponSpawnPoint.forward * 100.0f);
 //       }
	//}
    
    //[Command]
    //private void Cmd_DestoryGhost(GameObject ghost)
    //{
    //    GameManager.instance.DestroyEnemy();
    //    NetworkServer.Destroy(ghost);
    //}


    void StopParticleSystem()
    {
        ParticleSystem rootParticleSystem = spawnedParticleSystem.GetComponent<ParticleSystem>();
        rootParticleSystem.Stop(true);
        
    }

    void StartParticleSystem()
    {
        ParticleSystem rootParticleSystem = spawnedParticleSystem.GetComponent<ParticleSystem>();
        rootParticleSystem.Play(true);
    }
    
}


using System.Collections;
using Rewired;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.PostProcessing;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;
using UnityStandardAssets.Characters.ThirdPerson;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(NavMeshAgent))]
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

    private Transform spawnedParticleSystem;
    public Text enemiesRemainingText;

//    private Material beamMaterial;

    [HideInInspector] public bool controllsReversed = false;
    public bool ReversedControls {
        get { return controllsReversed; }
        set {
            controllsReversed = value;
            uc.reverseControls = controllsReversed;
        }
    }
    [HideInInspector] public bool disableControls = false;
    public bool DisableControls {
        get { return disableControls; }
        set {
            disableControls = value;
            uc.disableControls = disableControls;
            m_MouseLook.disabled = value;
        }
    }
    private NavMeshAgent navMeshAgent;

    [Space(10)]
    [Header("Weapon Variables")]
    public float weaponRechargeRate = 0.75f;
    public float damagePerSecond = 10;
    public CameraShake shake;
    private float distance = 0;
    public float distanceOverTime = 10.0f;
    public float startupTime = 2.0f;
    public float curresntStartupTime = 0.0f;
    private bool hasStarted = false;
    public float currentWeaponTime = 0;
    public float maxWeaponTime = 8.0f;
    public Transform beamLight;
	public CustomLight beamLightCLight;
	public Decal impactDecal;
    private Material weaponRechargeRenderer;
    public GameObject weaponRechargeIndicator;
    private Material beamRenderer;
    public GameObject captureSpherePrefab;
    private GameObject spawnedCaptureSphere;
    public ParticleSystem weaponSteam;
    private NetworkedThirdPersonUserControl uc;
    public float lineNoise = 1.00f;
    [Range(0.0f, 1.0f)]public float cameraShakeIntensity = 0.5f;

    [Space(10)]
    [Header("Weapon Variables")]
    public AudioClip weaponChargeSound;
    public AudioClip weaponMainSound;
    public AudioClip weaponEndSound;
    public AudioClip playerStepSound;
    public List<AudioClip> playerAmbientSounds = new List<AudioClip>();
    [Tooltip("Do not edit")]
    public VirtualAudioSource virtaulWeaponAudioSource;
    [Tooltip("Do not edit")]
    public AudioSource weaponAudioSource;
	private List<Transform> beamLightSegments = new List<Transform> ();
	private List<CustomLight> beamLightCLightSegments = new List<CustomLight> ();

    Vector3 positionOnSpawn = Vector3.zero;
	ParticleSystem rootMuzzleParticleSystem;
	ParticleSystem rootParticleSystem;
	bool firing = false;
	RaycastHit hit;
	List<Joystick> joysticks = new List<Joystick>();

	Vector3 planeProjection = Vector3.zero;
	Vector3 endPosition = Vector3.zero;
	Vector3 previousPos = Vector3.zero;
	int vertCount = 0;
	float effectDistance = 0.0f;
	float halfDist = 0.0f;
	Vector3 pos = Vector3.zero;
	Ray ray ;
	int i = 0;
	Ray crouchRay;
	float crouchRayLength = 0.0f;

    void Start()
	{
		m_Animator = GetComponent<Animator>();
		m_Rigidbody = GetComponent<Rigidbody>();
		m_Capsule = GetComponent<CapsuleCollider>();
        uc = GetComponent<NetworkedThirdPersonUserControl>();
        m_CapsuleHeight = m_Capsule.height;
		m_CapsuleCenter = m_Capsule.center;
        m_Camera = GetComponentInChildren<Camera>();
        navMeshAgent = GetComponent<NavMeshAgent>();

        m_Rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
		m_OrigGroundCheckDistance = m_GroundCheckDistance;
		lineRenderer = GetComponentInChildren<LineRenderer>();
        m_MouseLook.Init(transform, m_Camera.transform);

        GetComponentInChildren<Renderer>().material.color = playerColour;

        playerScore = 0;

        spawnedParticleSystem = ((GameObject)Instantiate(tempDecalParticleSystem, transform.position, Quaternion.identity)).transform;
		rootParticleSystem = spawnedParticleSystem.GetComponent<ParticleSystem>();
		rootMuzzleParticleSystem = muzzleParticleSystem.GetComponent<ParticleSystem>();
        rootMuzzleParticleSystem.Stop();
        StopParticleSystem();
        weaponRechargeRenderer = weaponRechargeIndicator.GetComponent<Renderer>().material;
        beamRenderer = lineRenderer.GetComponent<Renderer>().material;
        beamRenderer.SetColor("_Colour", playerColour * 2);

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

		rootParticleSystem.startColor = playerColour;
		cbs = rootParticleSystem.colorBySpeed;
        grad = new ParticleSystem.MinMaxGradient();
        grad.colorMin = playerColour;
        grad.colorMax = Color.white;
        cbs.color = grad;
        sparkspsRootMzzle = spawnedParticleSystem.transform.FindChild("Sparks").GetComponent<ParticleSystem>();
        sparkspsRootMzzle.startColor = playerColour;
        cbssparks = sparkspsRootMzzle.colorBySpeed;
        cbssparks.color = grad;
        lineRenderer.SetVertexCount(20);
        spawnedCaptureSphere = Instantiate(captureSpherePrefab);
        spawnedCaptureSphere.SetActive(false);
        weaponSteam.Stop(true);
        positionOnSpawn = transform.position;
		//beamLightCLight = beamLight.GetComponent<CustomLight> ();
		for (i = 0; i < 50; ++i) {
			beamLightSegments.Add (((GameObject)Instantiate (beamLight.gameObject)).transform);
		}
		for (i = 0; i < 50; ++i) {
			beamLightSegments [i].parent = beamLight.transform.parent;
			beamLightSegments [i].GetComponentInChildren<CustomLight> ().m_TubeLength = 0.45f;
			beamLightCLightSegments.Add (beamLightSegments [i].GetComponentInChildren<CustomLight> ());
			beamLightSegments [i].gameObject.SetActive (false);
		}
		beamLight.gameObject.SetActive (false);
		beamLightCLight.m_Color = Color.cyan;
        if (!isLocalPlayer)
        {
            m_Camera.gameObject.SetActive(false);
            weaponRechargeIndicator.gameObject.SetActive(false);
        }

        if (isLocalPlayer)
        {
            
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

        //beamMaterial = lineRenderer.material;
        GameManager.instance.players.Add(this);
		joysticks = new List<Joystick>(uc.player.controllers.Joysticks);
	}
		


    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F3))
        {
            RevokePlayerControlAndSetPlayerAIToTarget(positionOnSpawn);
        }
        if (navMeshAgent.enabled)
        {
			planeProjection = Vector3.ProjectOnPlane(navMeshAgent.velocity, m_GroundNormal);
			planeProjection = transform.TransformDirection(planeProjection);
			m_TurnAmount = planeProjection.x;
			m_ForwardAmount = planeProjection.z;

            UpdateAnimator(transform.InverseTransformDirection(navMeshAgent.velocity));
            if (Vector3.Distance(transform.position, navMeshAgent.destination) < 2.5f &&  navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
            {
                navMeshAgent.enabled = false;
                DisableControls = false;
            }
        }
        else
        {
            if (firing)
            {
                if (!hasStarted)
                {
                    if (curresntStartupTime < startupTime)
                    {
                        shake.ShakeCamera((curresntStartupTime / startupTime) * cameraShakeIntensity, Time.deltaTime);
                        curresntStartupTime += Time.deltaTime;
						for (i = 0; i < joysticks.Count; ++i) {
							if (!joysticks [i].supportsVibration)
								continue;
							joysticks[i].SetVibration((curresntStartupTime / startupTime), (curresntStartupTime / startupTime));
						}
                    }
                    else
                    {
                        hasStarted = true;
                        virtaulWeaponAudioSource.clip = weaponMainSound;
                        virtaulWeaponAudioSource.loop = true;
                        virtaulWeaponAudioSource.Play();
                    }
                }
                else
                {
                    if (currentWeaponTime < maxWeaponTime)
                    {
						if (!lineRenderer.enabled || !beamLight.gameObject.activeInHierarchy) {
							lineRenderer.enabled = true;
							beamLight.gameObject.SetActive (true);
						}
                        rootMuzzleParticleSystem.Play();
                        shake.ShakeCamera(1 * cameraShakeIntensity, Time.deltaTime);
                        
						for (i = 0; i < joysticks.Count; ++i) {
							if (!joysticks [i].supportsVibration)
								continue;
							joysticks[i].SetVibration(1, 1);
						}

                        ray = new Ray(m_Camera.transform.position + m_Camera.transform.forward * m_Camera.nearClipPlane, weaponSpawnPoint.forward);
                        
						endPosition = weaponSpawnPoint.position + weaponSpawnPoint.forward * distance;
						vertCount = Mathf.RoundToInt (distance);
						lineRenderer.SetVertexCount (vertCount);
						if (vertCount > 0)
							lineRenderer.SetPosition(0, weaponSpawnPoint.position);
						effectDistance = distance;

                        if (Physics.Raycast(ray, out hit, distance))
                        {
							endPosition = hit.point;
							effectDistance = Mathf.Abs(Vector3.Distance(hit.point, weaponSpawnPoint.position));

                            if (hit.transform.GetComponentInParent<GhostBehaviour>())
                            {
                                if (isLocalPlayer)
                                    hit.transform.GetComponentInParent<GhostBehaviour>()
                                        .TakeDamage(playerID, damagePerSecond * Time.deltaTime);
                                spawnedCaptureSphere.transform.position = hit.transform.gameObject.transform.position + Vector3.up;
                                spawnedCaptureSphere.GetComponent<Renderer>()
                                    .material.SetFloat("_PercentageComplete",
                                        1 -
                                        (hit.transform.GetComponentInParent<GhostBehaviour>().CurrentHealth /
                                         hit.transform.GetComponentInParent<GhostBehaviour>().maxHealth));
                            }
                            else
                            {
                                StartParticleSystem();
                                Vector3 norm = weaponSpawnPoint.position - hit.point;
                                norm.Normalize();
                                spawnedParticleSystem.position = hit.point + norm * 0.1f;
                                //spawnedCaptureSphere.SetActive(false);
                            }
                        }
                        else
                        {
                            StopParticleSystem();
                            //spawnedCaptureSphere.SetActive(false);
                            distance += distanceOverTime * Time.deltaTime;
                        }


						halfDist = effectDistance / 2;
						beamLightCLight.m_TubeLength = halfDist;
						beamLight.transform.localPosition = Vector3.zero;
						//beamLight.transform.localRotation = Quaternion.Euler (Vector3.up * 90);
						beamLight.transform.position += weaponSpawnPoint.forward * halfDist;
						previousPos = weaponSpawnPoint.position;
						for (i = 1; i < vertCount; i++)
						{
							//Set the position here to the current location and project it in the forward direction of the object it is attached to
							pos = weaponSpawnPoint.position + weaponSpawnPoint.forward * i * (effectDistance / vertCount) +
								new Vector3(Random.Range(-lineNoise, lineNoise), Random.Range(-lineNoise, lineNoise), 0);

							lineRenderer.SetPosition(i, pos);
							if(!beamLightSegments[i].gameObject.activeInHierarchy)
								beamLightSegments [i].gameObject.SetActive (true);
							beamLightSegments [i].transform.position = pos;
							beamLightSegments [i].transform.localRotation = Quaternion.Euler ( beamLightSegments [i].TransformDirection( (previousPos - pos).normalized));
							//beamLightSegments [i].transform.Rotate(beamLightSegments [i].up * 90);
							beamLightSegments [i].transform.position += (previousPos - pos).normalized * 0.5f;
							beamLightCLightSegments [i].m_TubeLength = Mathf.Abs (Vector3.Distance (previousPos, pos) / 2);
							beamLightSegments [i].transform.LookAt (previousPos);
							previousPos = pos;
						}
						if (vertCount > 1) {
							lineRenderer.SetPosition (vertCount - 1, endPosition);
							beamLightSegments [vertCount - 1].transform.position = beamLightSegments [vertCount - 2].position;
							beamLightSegments [vertCount - 1].transform.localRotation = Quaternion.Euler ( beamLightSegments [vertCount - 1].TransformDirection( (beamLightSegments [vertCount - 2].position - endPosition).normalized));
							beamLightSegments [vertCount - 1].transform.position -= (beamLightSegments [vertCount - 2].position - endPosition).normalized * Mathf.Abs(Vector3.Distance(beamLightSegments [vertCount - 2].position, endPosition)/2);
							beamLightSegments [vertCount - 1].transform.LookAt (endPosition);
						}
                        currentWeaponTime += Time.deltaTime;
                    }
                    else
                    {

                        if (isLocalPlayer)
                            Cmd_EndFire();
                    }
                }

            }
        }

        if (!firing)
        {
            if (currentWeaponTime > 0)
                currentWeaponTime -= weaponRechargeRate * Time.deltaTime;
            else
                currentWeaponTime = 0;
        }

		currentOverheatValue = currentWeaponTime / maxWeaponTime;
		weaponRechargeRenderer.SetFloat("_CurrentOverheatValue", currentOverheatValue);

        
        
    }

	float currentOverheatValue = 0.0f;

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
			crouchRay = new Ray(m_Rigidbody.position + Vector3.up * m_Capsule.radius * k_Half, Vector3.up);
			crouchRayLength = m_CapsuleHeight - m_Capsule.radius * k_Half;
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
			crouchRay = new Ray(m_Rigidbody.position + Vector3.up * m_Capsule.radius * k_Half, Vector3.up);
			crouchRayLength = m_CapsuleHeight - m_Capsule.radius * k_Half;
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
#if UNITY_EDITOR
		// helper to visualise the ground check ray in the scene view
		Debug.DrawLine(transform.position + (Vector3.up * 0.1f), transform.position + (Vector3.up * 0.1f) + (Vector3.down * m_GroundCheckDistance));
#endif
		// 0.1f is a small offset to start the ray from inside the character
		// it is also good to note that the transform position in the sample assets is at the base of the character
		if (Physics.Raycast(transform.position + (Vector3.up * 0.1f), Vector3.down, out hit, m_GroundCheckDistance))
		{
			m_GroundNormal = hit.normal;
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


    [Command]
    public void Cmd_BeginFire()
    {
        Rpc_BeginFire();
    }

    [ClientRpc]
    void Rpc_BeginFire()
    {
        firing = true;
        distance = 0;
        hasStarted = false;
        curresntStartupTime = 0.0f;
        weaponAudioSource.clip = weaponChargeSound;
        virtaulWeaponAudioSource.clip = weaponChargeSound;
        virtaulWeaponAudioSource.Play();
    }

    [Command]
    public void Cmd_EndFire()
    {
        Rpc_EndFire();
    }

    [ClientRpc]
    void Rpc_EndFire()
    {
        firing = false;
        lineRenderer.enabled = false;
        //spawnedParticleSystem.gameObject.SetActive(false);
        beamLight.gameObject.SetActive(false);
        StopParticleSystem();
        if(hasStarted)
            weaponSteam.Play(true);
        rootMuzzleParticleSystem.Stop();

        virtaulWeaponAudioSource.clip = weaponEndSound;
        virtaulWeaponAudioSource.loop = false;
        virtaulWeaponAudioSource.Play();

		for (i = 0; i < 50; ++i) {
			if (beamLightSegments [i].gameObject.activeInHierarchy)
				beamLightSegments [i].gameObject.SetActive (false);
		}

		for (i = 0; i < joysticks.Count; ++i) {
			if (!joysticks [i].supportsVibration)
				continue;
			joysticks[i].StopVibration();
		}

    }

    void StopParticleSystem()
    {
        rootParticleSystem.Stop(true);
    }

    void StartParticleSystem()
    {
        rootParticleSystem.Play(true);
    }
    
    public void RevokePlayerControlAndSetPlayerAIToTarget(Transform target)
    {
        RevokePlayerControlAndSetPlayerAIToTarget(target.position);
    }

    public void RevokePlayerControlAndSetPlayerAIToTarget(Vector3 targetPosition)
    {
        DisableControls = true;
        navMeshAgent.enabled = true;
        navMeshAgent.SetDestination(targetPosition);
    }
}


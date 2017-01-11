using System;
using System.Collections.Generic;
using Rewired;
using UnityEngine;
using UnityEngine.Networking;
using UnityStandardAssets.Characters.FirstPerson;
using UnityStandardAssets.CrossPlatformInput;
using Joystick = Rewired.Joystick;

namespace UnityStandardAssets.Characters.ThirdPerson
{
	enum ReInputPlayerConfigs
	{
		Controller0 = 0,
		Controller1,
		KeyboardOnly
	}

    [RequireComponent(typeof(NetworkedThirdPersonCharacter))]
    public class NetworkedThirdPersonUserControl : NetworkBehaviour
    {
        private NetworkedThirdPersonCharacter m_Character; // A reference to the ThirdPersonCharacter on the object
        private Transform m_Cam; // A reference to the main camera in the scenes transform
        private Vector3 m_CamForward; // The current forward direction of the camera
        private Vector3 m_Move;

        private bool m_Jump;
            // the world-relative desired move direction, calculated from the camForward and user input.

//        private bool m_firing;
        public int multiplyer = 1;
        private bool m_isRunning = false;

        public bool running
        {
            get { return m_isRunning; }
        }

        public bool allowRunning = true;
        public bool escapeMenu = false;

        private int playerID = 0; // Rewired playerid
        public Player player; // The Rewired Player
        public GameObject scorboardUI;
        [HideInInspector]
        public bool reverseControls = false;
        [HideInInspector]
        public bool disableControls = false;

        public void PreStart()
        {
			if (!isLocalPlayer)
				return;


			if (ReInput.controllers.joystickCount > 1) {
				if (GameManager.instance.playerOneAssigned)
					playerID = 1;
				else
					playerID = 0;
			} else {
				if (!GameManager.instance.playerOneAssigned)
					playerID = 0;
				else
					playerID = 2;
			}
            player = ReInput.players.GetPlayer(playerID);
            player.isPlaying = true;
            GameManager.instance.playerOneAssigned = true;
        }

        private void Start()
        {
            PreStart();
            multiplyer = 1;
            
            // get the third person character ( this should never be null due to require component )
            m_Character = GetComponent<NetworkedThirdPersonCharacter>();
            m_Character.m_MouseLook.player = player;
			m_Cam = transform.FindChild ("Camera").transform;

            if (!isLocalPlayer)
            {
                m_Cam.gameObject.SetActive(false);
                m_Character.joysticks = new List<Joystick>(player.controllers.Joysticks);
                //m_Character.enabled = false;
            }

        }


        private void Update()
        {
            if (!isLocalPlayer)
                return;

			if (player.GetButtonDown ("Menu")) {
				escapeMenu = !escapeMenu;
				m_Character.ShowEscapeMenuRoot (escapeMenu);
				m_Character.ShowEscapeMenu (escapeMenu);
			}
            if (!escapeMenu || disableControls)
            {
                if (!m_Jump)
                {
                    m_Jump = player.GetButtonDown("Jump");
                }
                if(player.GetButtonDown("Fire"))
                {
                    m_Character.Cmd_BeginFire();
                }
                else if(player.GetButtonUp("Fire"))
                {
                    m_Character.Cmd_EndFire();
                }
                if(player.GetButtonDown("Score"))
                    scorboardUI.SetActive(!scorboardUI.activeInHierarchy);
            }
        }

		bool crouch = false;
		float h, v;
        // Fixed update is called in sync with physics
        private void FixedUpdate()
        {
            if (!isLocalPlayer)
                return;

           
            crouch = false;
            if (!escapeMenu || disableControls)
            {
                // read inputs
                h = player.GetAxis("WalkHorizontal") * (reverseControls ? -1 : 1);
                v = player.GetAxis("WalkVertical") * (reverseControls ? -1 : 1);

                // TODO: crouch input
                crouch = player.GetButton("Crouch");
                m_Move = new Vector3(h, 0, v) / 2;
                m_Move *= multiplyer;
                
                // TODO: run input
                // run speed multiplier
                if (player.GetButton("Run") && allowRunning)
                {
                    m_isRunning = true;
                    m_Move *= 2.0f;
                }
                else
                {
                    m_isRunning = false;
                }
            }
            else
            {
                m_Move = Vector3.zero;
            }
            // pass all parameters to the character control script
            m_Character.Move(m_Move, crouch, m_Jump, escapeMenu);
            //m_Character.Fire(m_firing);
            m_Jump = false;

        }
    }
}

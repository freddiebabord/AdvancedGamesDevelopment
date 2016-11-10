using System;
using System.Collections.Generic;
using Rewired;
using UnityEngine;
using UnityEngine.Networking;
using UnityStandardAssets.Characters.FirstPerson;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof(ThirdPersonCharacter))]
    public class NetworkedThirdPersonUserControl : NetworkBehaviour
    {
        private NetworkedThirdPersonCharacter m_Character; // A reference to the ThirdPersonCharacter on the object
        private Transform m_Cam; // A reference to the main camera in the scenes transform
        private Vector3 m_CamForward; // The current forward direction of the camera
        private Vector3 m_Move;

        private bool m_Jump;
            // the world-relative desired move direction, calculated from the camForward and user input.

        private bool m_firing;
        public int multiplyer = 1;
        private bool m_isRunning = false;

        public bool running
        {
            get { return m_isRunning; }
        }

        public bool allowRunning = true;
        public bool escapeMenu = false;

        public int playerID; // Rewired playerid
        private Player player; // The Rewired Player

        void Awake()
        {
            // Get the Rewired Player object for this player and keep it for the duration of the character's lifetime
            if (GameManager.instance.playerOneAssigned)
                playerID++;
            player = ReInput.players.GetPlayer(playerID);
            GameManager.instance.playerOneAssigned = true;
        }

        private void Start()
        {
            multiplyer = 1;
            m_Cam = GetComponentInChildren<Camera>().transform;
            // get the third person character ( this should never be null due to require component )
            m_Character = GetComponent<NetworkedThirdPersonCharacter>();
            m_Character.m_MouseLook.player = player;


            if (!isLocalPlayer)
            {
                m_Cam.gameObject.SetActive(false);
                m_Character.enabled = false;
            }

        }


        private void Update()
        {
            if (!isLocalPlayer)
                return;

            if (player.GetButton("Menu"))
                escapeMenu = !escapeMenu;

            if (!escapeMenu)
            {
                if (!m_Jump)
                {
                    m_Jump = player.GetButtonDown("Jump");
                }
                if (!m_firing)
                {
                    m_firing = player.GetButton("Fire");
                }
            }
        }


        // Fixed update is called in sync with physics
        private void FixedUpdate()
        {
            if (!isLocalPlayer)
                return;
            bool crouch = false;
            if (!escapeMenu)
            {
                // read inputs
                float h = player.GetAxis("WalkHorizontal");
                float v = player.GetAxis("WalkVertical");

                // TODO: crouch input
                crouch = player.GetButton("Crouch");
                m_Move = new Vector3(h, 0, v) / 2;
                m_Move *= multiplyer;

#if !MOBILE_INPUT
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
#endif
            }
            // pass all parameters to the character control script
            m_Character.Move(m_Move, crouch, m_Jump, escapeMenu);
            m_Character.Fire(m_firing);
            m_firing = false;
            m_Jump = false;

        }
    }
}

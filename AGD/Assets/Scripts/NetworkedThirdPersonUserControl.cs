using System;
using UnityEngine;
using UnityEngine.Networking;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof (ThirdPersonCharacter))]
    public class NetworkedThirdPersonUserControl : NetworkBehaviour
    {
        private NetworkedThirdPersonCharacter m_Character; // A reference to the ThirdPersonCharacter on the object
        private Transform m_Cam;                  // A reference to the main camera in the scenes transform
        private Vector3 m_CamForward;             // The current forward direction of the camera
        private Vector3 m_Move;
        private bool m_Jump;                      // the world-relative desired move direction, calculated from the camForward and user input.
        private bool m_firing;
        
        private void Start()
        {
            m_Cam = GetComponentInChildren<Camera>().transform;

            // get the third person character ( this should never be null due to require component )
            m_Character = GetComponent<NetworkedThirdPersonCharacter>();
        }


        private void Update()
        {
            if(!isLocalPlayer)
                return;

            if (!m_Jump)
            {
                m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
            }
            if(!m_firing)
            {
                m_firing = Input.GetButton("Fire1");
            }
        }


        // Fixed update is called in sync with physics
        private void FixedUpdate()
        {
            if(!isLocalPlayer)
                return;

            // read inputs
            float h = CrossPlatformInputManager.GetAxis("Horizontal");
            float v = CrossPlatformInputManager.GetAxis("Vertical");
            bool crouch = Input.GetKey(KeyCode.C);
            m_Move = new Vector3(h, 0, v);
            // calculate move direction to pass to character
            /*if (m_Cam != null)
            {
                // calculate camera relative direction to move:
                m_CamForward = Vector3.Scale(m_Cam.forward, new Vector3(1, 0, 1)).normalized;
                m_Move = v*m_CamForward + h*m_Cam.right;
            }
            else
            {
                // we use world-relative directions in the case of no main camera
                m_Move = v*Vector3.forward + h*Vector3.right;
            }*/
#if !MOBILE_INPUT
			// walk speed multiplier
	        if (Input.GetKey(KeyCode.LeftShift)) m_Move *= 0.5f;
#endif

            // pass all parameters to the character control script
            m_Character.Move(m_Move, crouch, m_Jump);
            m_Character.Fire(m_firing);
            m_firing = false;
            m_Jump = false;
        }
    }
}

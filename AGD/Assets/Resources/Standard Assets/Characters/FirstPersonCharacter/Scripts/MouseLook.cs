using System;
using Rewired;
using UnityEngine;
using Cursor = UnityEngine.Cursor;

namespace UnityStandardAssets.Characters.FirstPerson
{
    [Serializable]
    public class MouseLook
    {
        public float XSensitivity = 2f;
        public float YSensitivity = 2f;

        public float MinimumX = -90F;
        public float MaximumX = 90F;
        public bool lockCursor = true;

        
        private bool m_cursorIsLocked = true;
        public Transform weaponRotationPoint;
        public Transform weapon;
        public Player player;

        public void Init(Transform character, Transform camera)
        {

        }
        
        public void LookRotation(Transform character, Transform camera)
        {
            if (player == null)
                return;

            float yRot = player.GetAxis("LookHorizontal") * XSensitivity;
            float xRot = player.GetAxis("LookVertical") * YSensitivity;



            //character.localRotation = Quaternion.Slerp (character.localRotation, m_CameraTargetRot, smoothTime * Time.deltaTime);
            // m_CameraTargetRot.z = 0.0f;
            //camera.localRotation = Quaternion.Slerp (camera.localRotation, m_CameraTargetRot, smoothTime * Time.deltaTime);
            character.transform.RotateAround(character.position, Vector3.up, yRot);
            camera.transform.RotateAround(camera.position, camera.right, -xRot);
            camera.transform.localRotation = ClampRotationAroundXAxis(camera.transform.localRotation);
            //weapon.RotateAround(weaponRotationPoint.position, camera.right, -xRot);
            Vector3 eRot = camera.transform.rotation.eulerAngles;
            eRot.y = 0;
            eRot.z = 0;
            weapon.transform.localRotation = Quaternion.Euler(eRot);
            // UpdateCursorLock();
        }

        public void SetCursorLock(bool value)
        {
            lockCursor = value;
            if(!lockCursor)
            {//we force unlock the cursor if the user disable the cursor locking helper
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }

        public void UpdateCursorLock()
        {
            //if the user set "lockCursor" we check & properly lock the cursos
           // if (lockCursor)
            //    InternalLockUpdate();
        }

        private void InternalLockUpdate()
        {
            
            if (m_cursorIsLocked)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            else if (!m_cursorIsLocked)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }

        Quaternion ClampRotationAroundXAxis(Quaternion q)
        {
            q.x /= q.w;
            q.y /= q.w;
            q.z /= q.w;
            q.w = 1.0f;

            float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan (q.x);

            angleX = Mathf.Clamp (angleX, MinimumX, MaximumX);

            q.x = Mathf.Tan (0.5f * Mathf.Deg2Rad * angleX);

            return q;
        }

    }
}

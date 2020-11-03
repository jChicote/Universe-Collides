﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public interface ICameraControl
{
    void SetCameraLocking(InputValue value);
}

namespace PlayerSystems
{
    public class CameraController : MonoBehaviour, ICameraControl
    {
        CinemachineVirtualCamera virtualCamera;
        CameraAttributes attributes;
        [HideInInspector] public CinemachineTransposer cameraTransposer;
        [HideInInspector] public CinemachineComposer cameraComposer;

        public bool isFocused = false;

        void Start()
        {
            cameraTransposer = virtualCamera.GetCinemachineComponent<CinemachineTransposer>();
            cameraComposer = virtualCamera.GetCinemachineComponent<CinemachineComposer>();
            SetCameraTrack(attributes.composerDefaultY, attributes.defaultXDamp, attributes.defaultYDamp, attributes.defaultZDamp,
                            attributes.defaultPitchDamp, attributes.defaultYawDamp, attributes.defaultRollDamp);
        }

        public void Init(CinemachineVirtualCamera camera, VesselType vesselType)
        {
            this.virtualCamera = camera;
            PlayerSettings playerSettings = GameManager.Instance.gameSettings.playerSettings;
            attributes = playerSettings.cameraAttributes.Where(x => x.type == vesselType).First();
        }

        public void SetCameraLocking(InputValue value)
        {
            isFocused = value.isPressed;
            SetCameraTracking();
            StartCoroutine(LockingCamera());
        }

        public void SetFieldOfView(float throttleValue)
        {
            //Returns to normal when no throttling is applied
            if (throttleValue == 0)
            {
                virtualCamera.m_Lens.FieldOfView = Mathf.Lerp(virtualCamera.m_Lens.FieldOfView, attributes.defaultFOV, 0.1f);
                return;
            }

            //Increases or decreases camera FOV on throttle value
            if (throttleValue > 0)
            {
                virtualCamera.m_Lens.FieldOfView = Mathf.Lerp(virtualCamera.m_Lens.FieldOfView, attributes.maxFOV, 0.1f);
            }
            else
            {
                virtualCamera.m_Lens.FieldOfView = Mathf.Lerp(virtualCamera.m_Lens.FieldOfView, attributes.minFOV, 0.04f);
            }
        }

        public void SetCameraTracking()
        {
            if (isFocused)
            {
                SetCameraTrack(attributes.composerModifiedY, attributes.focusedXDamp, attributes.focusedYDamp, attributes.focusedZDamp,
                            attributes.focusedPitchDamp, attributes.focusedYawDamp, attributes.focusedRollDamp);
            }
            else
            {
                SetCameraTrack(attributes.composerDefaultY, attributes.defaultXDamp, attributes.defaultYDamp, attributes.defaultZDamp,
                            attributes.defaultPitchDamp, attributes.defaultYawDamp, attributes.defaultRollDamp);
            }
        }

        private void SetCameraTrack(float offsetY, float xDamp, float yDamp, float zDamp, float pitchDamp, float yawDamp, float rollDamp)
        {
            cameraComposer.m_TrackedObjectOffset.y = offsetY;
            cameraTransposer.m_XDamping = xDamp;
            cameraTransposer.m_YDamping = yDamp;
            cameraTransposer.m_ZDamping = zDamp;

            cameraTransposer.m_PitchDamping = pitchDamp;
            cameraTransposer.m_YawDamping = yawDamp;
            cameraTransposer.m_RollDamping = rollDamp;
        }

        public IEnumerator LockingCamera()
        {
            float zOffset = 0;
            float yOffset = 0;

            //Applies offset when camera is focused
            while (isFocused && Mathf.Round(zOffset) != attributes.modifiedZOffset)
            {
                zOffset = Mathf.SmoothStep(cameraTransposer.m_FollowOffset.z, attributes.modifiedZOffset, 0.2f);
                yOffset = Mathf.SmoothStep(cameraTransposer.m_FollowOffset.y, attributes.modifiedYOffset, 0.01f);
                cameraTransposer.m_FollowOffset = new Vector3(cameraTransposer.m_FollowOffset.x, yOffset, zOffset);
                yield return null;
            }

            if (isFocused) yield return null;

            //Applied default when camera is NOT focused
            while (!isFocused && Mathf.Round(zOffset) != attributes.defaultZOffset)
            {
                zOffset = Mathf.SmoothStep(cameraTransposer.m_FollowOffset.z, attributes.defaultZOffset, 0.2f);
                yOffset = Mathf.SmoothStep(cameraTransposer.m_FollowOffset.y, attributes.defaultYOffset, 0.01f);
                cameraTransposer.m_FollowOffset = new Vector3(cameraTransposer.m_FollowOffset.x, yOffset, zOffset);
                yield return null;
            }
        }
    }
}

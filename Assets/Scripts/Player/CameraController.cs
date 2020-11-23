using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public interface ICameraControl
{
    void SetCameraLocking(InputValue value);
}

public interface ICameraShake
{
    void TriggerCameraShake(float targetValue);
}

namespace PlayerSystems
{
    public class CameraController : MonoBehaviour, ICameraControl, ICameraShake
    {
        private CinemachineVirtualCamera virtualCamera;
        private CameraAttributes attributes;
        [HideInInspector] public CinemachineTransposer cameraTransposer;
        [HideInInspector] public CinemachineComposer cameraComposer;
        private CinemachineBasicMultiChannelPerlin cameraShakeChannel;

        public bool isFocused = false;
        private bool isThrusting = false;
        private bool isShaking = true;

        void Start()
        {
            cameraTransposer = virtualCamera.GetCinemachineComponent<CinemachineTransposer>();
            cameraComposer = virtualCamera.GetCinemachineComponent<CinemachineComposer>();
            cameraShakeChannel = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            SetCameraTrack(attributes.composerDefaultY, attributes.defaultXDamp, attributes.defaultYDamp, attributes.defaultZDamp,
                            attributes.defaultPitchDamp, attributes.defaultYawDamp, attributes.defaultRollDamp);
        }

        public void Init(CinemachineVirtualCamera camera, VesselType vesselType)
        {
            this.virtualCamera = camera;
            PlayerSettings playerSettings = GameManager.Instance.gameSettings.playerSettings;
            attributes = playerSettings.cameraAttributes.Where(x => x.type == vesselType).First();
        }

        /// <summary>
        /// Sets the virtual camera transposer to the focused state
        /// </summary>
        public void SetCameraLocking(InputValue value)
        {
            isFocused = value.isPressed;
            SetCameraTracking();
            StopCoroutine("TransposeCamera");
            StartCoroutine(TransposeCamera(attributes.modifiedYOffset, attributes.modifiedZOffset));
            //StartCoroutine(LockingCamera());
        }

        /// <summary>
        /// Sets the field of view when the ship changes on throttle up and throttle down.
        /// </summary>
        public void SetFieldOfView(float throttleValue)
        {
            //Returns to normal when no throttling is applied
            if (throttleValue == 0)
            {
                if (isThrusting && !isFocused)
                {
                    isThrusting = false;
                    StopCoroutine("TransposeCamera");
                    StartCoroutine(TransposeCamera(attributes.thrustYOffset, attributes.thrustZOffset));
                }

                virtualCamera.m_Lens.FieldOfView = Mathf.Lerp(virtualCamera.m_Lens.FieldOfView, attributes.defaultFOV, 0.1f);
                return;
            }

            //Increases or decreases camera FOV on throttle value
            if (throttleValue > 0)
            {
                if (!isThrusting && !isFocused)
                {
                    isThrusting = true;
                    StopCoroutine("TransposeCamera");
                    StartCoroutine(TransposeCamera(attributes.thrustYOffset, attributes.thrustZOffset));
                }

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

        public void TriggerCameraShake(float targetValue)
        {
            StopCoroutine(nameof(ShakeCamera));
            StartCoroutine(ShakeCamera(targetValue));
        }

        /// <summary>
        /// Transposes camera body into a set offset during travel.
        /// </summary>
        public IEnumerator TransposeCamera(float modifiedY, float modifiedZ)
        {
            float zOffset = 0;
            float yOffset = 0;

            TriggerCameraShake(0.05f);

            //Applies offset when camera is focused
            while (isFocused || isThrusting && Mathf.Round(zOffset) != modifiedZ)
            {
                Debug.Log("is increasing");
                zOffset = Mathf.SmoothStep(cameraTransposer.m_FollowOffset.z, modifiedZ, 0.08f);
                yOffset = Mathf.SmoothStep(cameraTransposer.m_FollowOffset.y, modifiedY, 0.01f);
                cameraTransposer.m_FollowOffset = new Vector3(cameraTransposer.m_FollowOffset.x, yOffset, zOffset);
                yield return null;
            }

            if (isFocused || isThrusting) yield return null;

            zOffset = 0;
            yOffset = 0;

            

            //Applied default when camera is NOT focused
            while (!isFocused || !isThrusting && Mathf.Round(zOffset) != attributes.defaultZOffset)
            {
                Debug.Log("Is returning");
                zOffset = Mathf.SmoothStep(cameraTransposer.m_FollowOffset.z, attributes.defaultZOffset, 0.08f);
                yOffset = Mathf.SmoothStep(cameraTransposer.m_FollowOffset.y, attributes.defaultYOffset, 0.01f);
                cameraTransposer.m_FollowOffset = new Vector3(cameraTransposer.m_FollowOffset.x, yOffset, zOffset);
                yield return null;
            }

            if (!isFocused && !isThrusting)
            {
                Debug.Log("Is slowiong");
                TriggerCameraShake(-0.05f);
                cameraTransposer.m_FollowOffset = new Vector3(cameraTransposer.m_FollowOffset.x, attributes.defaultYOffset, attributes.defaultZOffset);
                cameraShakeChannel.m_FrequencyGain = 0;
            }
        }

        public IEnumerator ShakeCamera(float targetLerpValue)
        {
            float currentShakeVal = cameraShakeChannel.m_FrequencyGain;

            while (currentShakeVal <= attributes.maxCamShake && currentShakeVal >= 0)
            {
                Debug.Log(targetLerpValue);
                currentShakeVal += targetLerpValue;
               // currentShakeVal = Mathf.Clamp(currentShakeVal, 0, targetLerpValue);
                cameraShakeChannel.m_FrequencyGain = currentShakeVal;
                yield return null;
            }
        }
    }
}

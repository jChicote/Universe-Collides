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
    public class CameraController : MonoBehaviour, ICameraControl
    {
        private CinemachineVirtualCamera virtualCamera;
        private CameraAttributes attributes;
        [HideInInspector] public CinemachineTransposer cameraTransposer;
        [HideInInspector] public CinemachineComposer cameraComposer;
        private CameraShaker cameraShaker;

        public bool isFocused = false;
        private bool isThrusting = false;
        private bool isTransposing = false;

        void Start()
        {
            cameraTransposer = virtualCamera.GetCinemachineComponent<CinemachineTransposer>();
            cameraComposer = virtualCamera.GetCinemachineComponent<CinemachineComposer>();
            cameraShaker = new CameraShaker();
            cameraShaker.Init(virtualCamera);
            cameraShaker.SetClamp(0, 1.5f);

            SetCameraTrack(attributes.composerDefaultY, attributes.defaultXDamp, attributes.defaultYDamp, attributes.defaultZDamp,
                            attributes.defaultPitchDamp, attributes.defaultYawDamp, attributes.defaultRollDamp);
        }

        public void Init(CinemachineVirtualCamera camera, VesselType vesselType)
        {
            this.virtualCamera = camera;
            PlayerSettings playerSettings = GameManager.Instance.gameSettings.playerSettings;
            attributes = playerSettings.cameraAttributes.Where(x => x.type == vesselType).First();

            CameraExternalShake externalShaker = this.GetComponent<CameraExternalShake>();
            externalShaker.Init(camera);
        }


        /// <summary>
        /// Sets the virtual camera transposer to the focused state
        /// </summary>
        public void SetCameraLocking(InputValue value)
        {
            isFocused = value.isPressed;
            isTransposing = value.isPressed;
            SetCameraTracking();

            StopCoroutine("TransposeCamera");
            StartCoroutine(TransposeCamera(attributes.modifiedYOffset, attributes.modifiedZOffset));
        }

        /// <summary>
        /// Sets the field of view when the ship changes on throttle up and throttle down.
        /// </summary>
        public void SetFieldOfView(float throttleValue)
        {
            //Returns to normal when no throttling is applied
            if (throttleValue == 0)
            {
                isThrusting = false;
                if (!isFocused)
                {
                    StartCoroutine(TransposeToOrigin());
                }

                virtualCamera.m_Lens.FieldOfView = Mathf.Lerp(virtualCamera.m_Lens.FieldOfView, attributes.defaultFOV, 0.1f);
                return;
            }

            //Increases or decreases camera FOV on throttle value
            if (throttleValue > 0)
            {
                isThrusting = true;

                if (!isFocused)
                {
                    isTransposing = true;
                    StartCoroutine(TransposeCamera(attributes.thrustYOffset, attributes.thrustZOffset));
                }

                StopCoroutine("ShakeCamera");
                StartCoroutine(ShakeCamera());

                virtualCamera.m_Lens.FieldOfView = Mathf.Lerp(virtualCamera.m_Lens.FieldOfView, attributes.maxFOV, 0.1f);
            }
            else
            {
                isThrusting = false;
                StopCoroutine("ShakeCamera");
                StartCoroutine(ShakeCamera());
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

        /// <summary>
        /// Transposes camera body into a set offset during travel.
        /// </summary>
        public IEnumerator TransposeCamera(float modifiedY, float modifiedZ)
        {
            float zOffset = 0;
            float yOffset = 0;

            StopCoroutine("TransposeToOrigin");
            StopCoroutine("ShakeCamera");
            StartCoroutine(ShakeCamera());

            //Applies offset when camera is focused
            while (isTransposing && Mathf.Round(zOffset) != modifiedZ)
            {
                //Debug.Log("is increasing");
                zOffset = Mathf.SmoothStep(cameraTransposer.m_FollowOffset.z, modifiedZ, 0.08f);
                yOffset = Mathf.SmoothStep(cameraTransposer.m_FollowOffset.y, modifiedY, 0.01f);
                cameraTransposer.m_FollowOffset = new Vector3(cameraTransposer.m_FollowOffset.x, yOffset, zOffset);
                if(isThrusting) cameraShaker.IncreaseShake(0.07f);
                yield return null;
            }
        }

        public IEnumerator TransposeToOrigin()
        {
            float zOffset = cameraTransposer.m_FollowOffset.z;
            float yOffset = cameraTransposer.m_FollowOffset.y;

            StopCoroutine("TransposeCamera");
            StopCoroutine("ShakeCamera");
            StartCoroutine(ShakeCamera());

            while (isFocused || isThrusting && Mathf.Round(zOffset) != attributes.defaultZOffset)
            {
                //Debug.Log(Mathf.Round(zOffset) + " AND " + attributes.defaultZOffset);
                zOffset = Mathf.SmoothStep(cameraTransposer.m_FollowOffset.z, attributes.defaultZOffset, 0.08f);
                yOffset = Mathf.SmoothStep(cameraTransposer.m_FollowOffset.y, attributes.defaultYOffset, 0.01f);
                cameraTransposer.m_FollowOffset = new Vector3(cameraTransposer.m_FollowOffset.x, yOffset, zOffset);
                yield return null;
            }

            //This ensures that values are clean when back to original
            cameraTransposer.m_FollowOffset = new Vector3(cameraTransposer.m_FollowOffset.x, attributes.defaultYOffset, attributes.defaultZOffset);
        }

        public IEnumerator ShakeCamera()
        {
            while (isThrusting && cameraShaker.IncreaseShake(0.07f))
            {
                yield return null;
            }

            while(!isThrusting && cameraShaker.DecreaseShake(0.05f))
            {
                yield return null;
            }

            if (!isThrusting)
            {
                cameraShaker.ResetShake();
            }
        }
    }
}

public class CameraShaker
{
    private CinemachineBasicMultiChannelPerlin cameraShakeChannel;

    private float minimumValue = 0;
    private float maximumValue = 0;
    private float shakeValue = 0;

    private bool isShaking = true;

    public void Init(CinemachineVirtualCamera virtualCamera)
    {
        cameraShakeChannel = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public void SetClamp(float minVal, float maxVal)
    {
        minimumValue = minVal;
        maximumValue = maxVal;
    }

    public void SetShakeValue(float value)
    {
        shakeValue = value;
    }

    public bool IncreaseShake(float incrementVal)
    {
        shakeValue += incrementVal;
        shakeValue = Mathf.Clamp(shakeValue, minimumValue, maximumValue);
        cameraShakeChannel.m_FrequencyGain = shakeValue;
        return shakeValue == maximumValue;
    }

    public bool DecreaseShake(float decrementVal)
    {
        shakeValue -= decrementVal;
        shakeValue = Mathf.Clamp(shakeValue, minimumValue, maximumValue);
        cameraShakeChannel.m_FrequencyGain = shakeValue;
        return shakeValue == minimumValue;
    }

    public void ResetShake()
    {
        shakeValue = 0;
        cameraShakeChannel.m_FrequencyGain = shakeValue;
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class XWingState : PlayerState
{
    XWingWeaponSystem weaponSystem;
    Rigidbody playerRB;

    VesselShipStats shipStats;

    Vector3 currentRotation = Vector3.zero;
    Vector3 currentVelocity;

    float speed;
    float thrust;

    float pitchStrength;
    float yawStrength;
    float rollStrength;

    float pitchSteer;
    float yawSteer;
    float rollSteer;
    
    bool isLocking = false;

    public override void BeginState()
    {
        inputType = InputType.GamePad;
        playerSettings = GameManager.instance.gameSettings.playerSettings;

        playerController = this.GetComponent<PlayerController>();
        playerRB = this.GetComponent<Rigidbody>();
        weaponSystem = this.GetComponent<XWingWeaponSystem>();
        cameraTransposer = playerController.virtualCamera.GetCinemachineComponent<CinemachineTransposer>();
        cameraComposer = playerController.virtualCamera.GetCinemachineComponent<CinemachineComposer>();

        shipStats = GameManager.instance.gameSettings.vesselStats.Where(x => x.type.Equals(weaponSystem.vesselType)).First();
        speed = shipStats.speed;
    }

    void FixedUpdate()
    {
        if(isPaused) return;

        ApplyRotation();
        Movement();
        Throttle();
        weaponSystem.RunSystem();
    }

    private void Movement() {
        currentVelocity = speed * transform.forward * Time.fixedDeltaTime;
        transform.position += currentVelocity;
    }

    private void Throttle(){
        if(thrust == 0) {
            speed = Mathf.Lerp(speed, shipStats.speed, 0.05f);
            pitchStrength = Mathf.Lerp(pitchStrength, shipStats.pitchRate, 0.05f);
            yawStrength = Mathf.Lerp(yawStrength, shipStats.pitchRate, 0.05f);
            rollStrength = Mathf.Lerp(rollStrength, shipStats.pitchRate, 0.05f);
        } else {
            speed = Mathf.Lerp(speed, shipStats.speed + thrust, 0.05f);
            pitchStrength = Mathf.Lerp(pitchStrength, shipStats.pitchRate + pitchSteer, 0.05f);
            yawStrength = Mathf.Lerp(yawStrength, shipStats.pitchRate + yawSteer, 0.05f);
            rollStrength = Mathf.Lerp(rollStrength, shipStats.pitchRate + rollSteer, 0.05f);
        }
    }

    private void OnThrottle(InputValue value) {
        if(value.Get<Vector2>().y == 0) {
            thrust = 0;
            pitchSteer = 0;
            yawSteer = 0;
            rollSteer = 0;
            return;
        }

        float throttleValue = value.Get<Vector2>().y;
        thrust = shipStats.throttleSpeed * throttleValue;

        if(throttleValue < 0) {
            pitchSteer = shipStats.pitchRate * (-1 * throttleValue);
            yawSteer = shipStats.extraYaw * (-1 * throttleValue);
            rollSteer = shipStats.extraRoll * (-1 * throttleValue);
        }
    }

    ///TODO: Optimise control scheme to have matching universal outputs that is applied on rotation
    private void OnStickRotation(InputValue value) {
        if(inputType != InputType.GamePad) inputType = InputType.GamePad;

        inputX = value.Get<Vector2>().x * playerSettings.mouseSensitivity;
        inputY = value.Get<Vector2>().y * playerSettings.mouseSensitivity;

        pitch = pitchStrength * inputY * Time.fixedDeltaTime;
        yaw = yawStrength * inputX * Time.fixedDeltaTime;
        roll = rollStrength * inputX * Time.fixedDeltaTime;
    }

    private void OnMouseRotation(InputValue value) {

        float screenInputX = value.Get<Vector2>().x;
        float screenInputY = value.Get<Vector2>().y;

        inputX = (screenInputX - (Screen.width / 2)) / (Screen.width / 2) * playerSettings.mouseSensitivity;
        inputY = (screenInputY - (Screen.height / 2)) / (Screen.height / 2) * playerSettings.mouseSensitivity;
        
        pitch = pitchStrength * inputY * Time.fixedDeltaTime;
        yaw = yawStrength * inputX * Time.fixedDeltaTime;
        roll = rollStrength * inputX * Time.fixedDeltaTime;
    }

    private void ApplyRotation() {
        if(inputX == 0 || isLocking) {
            playerController.modelTransform.rotation = Quaternion.Slerp(playerController.modelTransform.rotation, transform.rotation, 0.1f);
            if(inputX == 0) return;
        }

        currentRotation = Vector3.zero;
        currentRotation = new Vector3(-1 * pitch, yaw, -1 * roll);
        transform.Rotate(currentRotation);

        Vector3 modelLocalAngles =  playerController.modelTransform.localEulerAngles;
        float modelAngle = (modelLocalAngles.z > 180) ? modelLocalAngles.z - 360 : modelLocalAngles.z;

        float angleResult = modelAngle + (-1 * roll);
        if (angleResult > -30 && angleResult < 30 )
            playerController.modelTransform.Rotate(0, 0, -1 * roll);
        else {
            playerController.modelTransform.localEulerAngles = new Vector3(0, 0, modelAngle);
        }
    }

    //CAN BE SET IN WEAPON SYSTEMS
    private void OnLocking(InputValue value) {
        isLocking = value.isPressed;

        if(isLocking){
            ModifyCameraTracking(1f, 0, 0, 0.2f, 0, 0, 0);
        } else {
            ModifyCameraTracking(0.7f, 1.5f, 1f, 0.5f, 0.6f, 0.6f, 0.3f);
        }

        StartCoroutine(LockingCamera());
    }

    private void ModifyCameraTracking(float offsetY, float xDamp, float yDamp, float zDamp, float pitchDamp, float yawDamp, float rollDamp) {
        cameraComposer.m_TrackedObjectOffset.y = offsetY;
        cameraTransposer.m_XDamping = xDamp;
        cameraTransposer.m_YDamping = yDamp;
        cameraTransposer.m_ZDamping = zDamp;

        cameraTransposer.m_PitchDamping = pitchDamp;
        cameraTransposer.m_YawDamping = yawDamp;
        cameraTransposer.m_RollDamping = rollDamp;
    }

    private IEnumerator LockingCamera() {
        float zOffset = 0;
        float yOffset = 0;

        while(isLocking && zOffset != -2) {
            zOffset = Mathf.Lerp(cameraTransposer.m_FollowOffset.z, -3, 0.01f);
            yOffset = Mathf.Lerp(cameraTransposer.m_FollowOffset.y, 1.5f, 0.1f);
            cameraTransposer.m_FollowOffset = new Vector3(cameraTransposer.m_FollowOffset.x, yOffset, zOffset);
            yield return null;
        }

        if (isLocking) yield break;

        while(!isLocking && zOffset != -5) {
            zOffset = Mathf.Lerp(cameraTransposer.m_FollowOffset.z, -5, 0.01f);
            yOffset = Mathf.Lerp(cameraTransposer.m_FollowOffset.y, 1f, 0.1f);
            cameraTransposer.m_FollowOffset = new Vector3(cameraTransposer.m_FollowOffset.x, yOffset, zOffset);
            yield return null;
        }
    }
}

public enum InputType {
    Mouse,
    GamePad
}

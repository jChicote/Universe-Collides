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

    Vector3 modelLocalAngles;
    
    float throttleInput;
    float modelAngle;
    float angleResult;

    public override void BeginState()
    {
        playerSettings = GameManager.Instance.gameSettings.playerSettings;

        playerController = this.GetComponent<PlayerController>();
        playerRB = this.GetComponent<Rigidbody>();
        weaponSystem = this.GetComponent<XWingWeaponSystem>();

        shipStats = GameManager.Instance.gameSettings.vesselStats.Where(x => x.type.Equals(weaponSystem.vesselType)).First();
        speed = shipStats.speed;
    }

    void FixedUpdate()
    {
        if(isPaused) return;

        ApplyRotation();
        Movement();
        Throttle();
        weaponSystem.RunSystem();
        weaponSystem.SetAimPosition(speed);
    }

    private void Movement() {
        currentVelocity = speed * transform.forward * Time.fixedDeltaTime;
        transform.position += currentVelocity;
    }

    private void Throttle(){
        playerController.cameraController.ModifyFieldOfView(throttleInput);

        if(thrust == 0 || playerController.cameraController.isFocused) {
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
            throttleInput = 0;
            thrust = 0;
            pitchSteer = 0;
            yawSteer = 0;
            rollSteer = 0;
            return;
        }

        throttleInput = value.Get<Vector2>().y;
        thrust = shipStats.throttleSpeed * throttleInput;

        if(throttleInput < 0) {
            pitchSteer = shipStats.pitchRate * (-1 * throttleInput);
            yawSteer = shipStats.extraYaw * (-1 * throttleInput);
            rollSteer = shipStats.extraRoll * (-1 * throttleInput);
        }
    }

    private void OnStickRotation(InputValue value) {
        inputX = value.Get<Vector2>().x * playerSettings.sensitivity;
        inputY = value.Get<Vector2>().y * playerSettings.sensitivity;

        pitch = pitchStrength * inputY * Time.fixedDeltaTime;
        yaw = yawStrength * inputX * Time.fixedDeltaTime;
        roll = rollStrength * inputX * Time.fixedDeltaTime;
    }

    private void OnMouseRotation(InputValue value) {
        float screenInputX = value.Get<Vector2>().x;
        float screenInputY = value.Get<Vector2>().y;

        inputX = (screenInputX - (Screen.width / 2)) / (Screen.width / 2) * playerSettings.sensitivity;
        inputY = (screenInputY - (Screen.height / 2)) / (Screen.height / 2) * playerSettings.sensitivity;
        
        pitch = pitchStrength * inputY * Time.fixedDeltaTime;
        yaw = yawStrength * inputX * Time.fixedDeltaTime;
        roll = rollStrength * inputX * Time.fixedDeltaTime;
    }

    private void ApplyRotation() {
        if(inputX == 0 || playerController.cameraController.isFocused) {
            playerController.modelTransform.rotation = Quaternion.Slerp(playerController.modelTransform.rotation, transform.rotation, 0.1f);
            if(inputX == 0) return;
        }

        currentRotation = Vector3.zero;
        currentRotation = new Vector3(-1 * pitch, yaw, -1 * roll);
        transform.Rotate(currentRotation);

        modelLocalAngles =  playerController.modelTransform.localEulerAngles;
        modelAngle = (modelLocalAngles.z > 180) ? modelLocalAngles.z - 360 : modelLocalAngles.z;

        angleResult = modelAngle + (-1 * roll);
        if (angleResult > -30 && angleResult < 30 )
            playerController.modelTransform.Rotate(0, 0, -1 * roll);
        else {
            playerController.modelTransform.localEulerAngles = new Vector3(0, 0, modelAngle);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position + transform.forward * speed * 5, 0.5f);
    }
}

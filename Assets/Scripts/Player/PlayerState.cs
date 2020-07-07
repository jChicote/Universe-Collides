using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public interface IPausable {
    void Pause();
    void UnPause();
}

public interface IMovementController {
    void SetSteeringStrength(float pitchStrength, float yawStrength, float rollStrength);
}

public interface IRotationController {
    void Something();
}

public abstract class PlayerState : BaseState, IMovementController
{
    [HideInInspector] public PlayerSettings playerSettings;
    [HideInInspector] public PlayerController controller;
    [HideInInspector] public Rigidbody playerRB;
    public InputMovementController movementController;

    public float inputX = 0;
    public float inputY = 0;
    //public float throttleInput = 0;

    public float pitchStrength = 0;
    public float yawStrength = 0;
    public float rollStrength = 0;

    /*public float pitchSteer = 0;
    public float yawSteer = 0;
    public float rollSteer = 0;*/

    public void ApplyMovement() {
        //currentVelocity = speed * transform.forward * Time.fixedDeltaTime;
        movementController.CalculateSteeringStrength(controller.cameraController, pitchStrength, yawStrength, rollStrength);
        speed = movementController.CalculateCurrentSpeed(controller.cameraController, speed);
        currentVelocity = movementController.CalculateVelocity(currentVelocity, transform.forward, speed);
        transform.position += currentVelocity;
    }

    public void SetSteeringStrength(float pitchStrength, float yawStrength, float rollStrength) {
        this.pitchStrength = pitchStrength;
        this.yawStrength = yawStrength;
        this.rollStrength = rollStrength;
    }

    public void SetSteering() {
        

        /*controller.cameraController.ModifyFieldOfView(throttleInput);

        if(thrust == 0 || controller.cameraController.isFocused) {
            speed = Mathf.Lerp(speed, shipStats.speed, 0.05f);
            pitchStrength = Mathf.Lerp(pitchStrength, shipStats.pitchRate, 0.05f);
            yawStrength = Mathf.Lerp(yawStrength, shipStats.yawRate, 0.05f);
            rollStrength = Mathf.Lerp(rollStrength, shipStats.rollRate, 0.05f);
        } else {
            speed = Mathf.Lerp(speed, shipStats.speed + thrust, 0.05f);
            pitchStrength = Mathf.Lerp(pitchStrength, shipStats.pitchRate + pitchSteer, 0.05f);
            yawStrength = Mathf.Lerp(yawStrength, shipStats.yawRate + yawSteer, 0.05f);
            rollStrength = Mathf.Lerp(rollStrength, shipStats.rollRate + rollSteer, 0.05f);
        }*/
    }

    public void OnThrottle(InputValue value) {
        if(isPaused) return;
        inputY = value.Get<Vector2>().y;

        movementController.ResetThrustSteer(inputY);
        movementController.CalculateThrust(inputY);
        movementController.CalculateSteering(inputY);

        /*if(value.Get<Vector2>().y == 0) {
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
        }*/
    }

    public void OnStickRotation(InputValue value) {
        if(isPaused) return;

        inputX = value.Get<Vector2>().x * playerSettings.sensitivity;
        inputY = value.Get<Vector2>().y * playerSettings.sensitivity;

        pitch = pitchStrength * inputY * Time.fixedDeltaTime;
        yaw = yawStrength * inputX * Time.fixedDeltaTime;
        roll = rollStrength * inputX * Time.fixedDeltaTime;
    }

    public void OnMouseRotation(InputValue value) {
        if(isPaused) return;

        //Mouse inputs offers screen position instead.
        float screenInputX = value.Get<Vector2>().x;
        float screenInputY = value.Get<Vector2>().y;

        //Scalued input value to center of screen
        inputX = (screenInputX - (Screen.width / 2)) / (Screen.width / 2) * playerSettings.sensitivity;
        inputY = (screenInputY - (Screen.height / 2)) / (Screen.height / 2) * playerSettings.sensitivity;
        
        pitch = pitchStrength * inputY * Time.fixedDeltaTime;
        yaw = yawStrength * inputX * Time.fixedDeltaTime;
        roll = rollStrength * inputX * Time.fixedDeltaTime;
    }
}

public class InputMovementController {

    private float throttleInput = 0;

    public float thrust = 0;
    public float pitchSteer = 0;
    public float yawSteer = 0;
    public float rollSteer = 0;

    private IMovementController movementController;
    private VesselShipStats shipStats;

    public InputMovementController(IMovementController controller, VesselShipStats shipStats) {
        this.movementController = controller;
        this.shipStats = shipStats;
    }

    public Vector3 CalculateVelocity(Vector3 currentVelocity, Vector3 forward, float speed) {
        currentVelocity = speed * forward * Time.fixedDeltaTime;
        return currentVelocity;
    }

    #region MOVEMENT RELATED CALCULATIONS

    public float CalculateCurrentSpeed(CameraController cameraController, float speed) {
        cameraController.ModifyFieldOfView(throttleInput);

        if(thrust == 0 || cameraController.isFocused) {
            speed = Mathf.Lerp(speed, shipStats.speed, 0.05f);
        } else {
            speed = Mathf.Lerp(speed, shipStats.speed + thrust, 0.05f);
        }

        return speed;
    }

    public void CalculateThrust(float inputY) {
        throttleInput = inputY;
        thrust = shipStats.throttleSpeed * inputY;
    }
    
    public void CalculateSteering(float inputY) {
        if(inputY > 0) return;

        pitchSteer = shipStats.pitchRate * (-1 * inputY);
        yawSteer = shipStats.extraYaw * (-1 * inputY);
        rollSteer = shipStats.extraRoll * (-1 * inputY);
    }

    public void CalculateSteeringStrength(CameraController cameraController, float pitchStrength, float yawStrength, float rollStrength) {
        if(thrust == 0 || cameraController.isFocused) {
            pitchStrength = Mathf.Lerp(pitchStrength, shipStats.pitchRate + pitchSteer, 0.05f);
            yawStrength = Mathf.Lerp(yawStrength, shipStats.yawRate + yawSteer, 0.05f);
            rollStrength = Mathf.Lerp(rollStrength, shipStats.rollRate + rollSteer, 0.05f);
        }else {
            pitchStrength = Mathf.Lerp(pitchStrength, shipStats.pitchRate, 0.05f);
            yawStrength = Mathf.Lerp(yawStrength, shipStats.yawRate, 0.05f);
            rollStrength = Mathf.Lerp(rollStrength, shipStats.rollRate, 0.05f);
        }

        movementController.SetSteeringStrength(pitchStrength, yawStrength, rollStrength);
    }

    public void ResetThrustSteer(float inputY) {
        if (inputY != 0) return;
        throttleInput = 0;
        thrust = 0;
        pitchSteer = 0;
        yawSteer = 0;
        rollSteer = 0;
    }

    #endregion
}

public class InputRotationController {
    private IRotationController movementController;
    private VesselShipStats shipStats;

    public void SetMovementController(IRotationController controller, VesselShipStats shipStats) {
        this.movementController = controller;
        this.shipStats = shipStats;
    }

    public void CalculateAxisRotation() {

    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    #region MOVEMENT RELATED CALCULATIONS

    public Vector3 CalculateVelocity(Vector3 currentVelocity, Vector3 forward, float speed) {
        currentVelocity = speed * forward * Time.fixedDeltaTime;
        return currentVelocity;
    }

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

        pitchSteer = shipStats.extraPitch * (-1 * inputY);
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

    public bool ThrottleIsZero(float inputY) {
        if (inputY != 0) return false;
        throttleInput = 0;
        thrust = 0;
        pitchSteer = 0;
        yawSteer = 0;
        rollSteer = 0;
        return true;
    }

    #endregion
}
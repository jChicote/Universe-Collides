using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public interface IPausable {
    void Pause();
    void UnPause();
}

public abstract class PlayerState : BaseState
{
    [HideInInspector] public PlayerSettings playerSettings;
    [HideInInspector] public PlayerController playerController;
    [HideInInspector] public Rigidbody playerRB;

    public float inputX = 0;
    public float inputY = 0;
    public float throttleInput = 0;

    public float pitchStrength = 0;
    public float yawStrength = 0;
    public float rollStrength = 0;

    public float pitchSteer = 0;
    public float yawSteer = 0;
    public float rollSteer = 0;

    public void Movement() {
        if(isEngineDamaged)
            currentVelocity = (shipStats.speed - shipStats.throttleSpeed) * transform.forward * Time.fixedDeltaTime;
        else
            currentVelocity = speed * transform.forward * Time.fixedDeltaTime;
        transform.position += currentVelocity;
    }

    public void Throttle(){
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

    public void OnThrottle(InputValue value) {
        if(isPaused) return;

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
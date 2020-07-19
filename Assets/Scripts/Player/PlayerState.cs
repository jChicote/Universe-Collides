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

public abstract class PlayerState : BaseState, IMovementController
{
    [HideInInspector] public PlayerSettings playerSettings;
    [HideInInspector] public PlayerController controller;
    [HideInInspector] public Rigidbody playerRB;
    
    public InputMovementController movementController;
    public InputRotationController rotationController;

    public float inputX = 0;
    public float inputY = 0;

    public float pitchStrength = 0;
    public float yawStrength = 0;
    public float rollStrength = 0;

    public void ApplyMovement() {
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

    public void OnThrottle(InputValue value) {
        if(isPaused) return;
        inputY = value.Get<Vector2>().y;

        if(movementController.ThrottleIsZero(inputY)) return;
        movementController.CalculateThrust(inputY);
        movementController.CalculateSteering(inputY);
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

        //Scales screen position input to center of screen
        inputX = rotationController.CalculateInputToCenter(value.Get<Vector2>().x, Screen.width, playerSettings.sensitivity);
        inputY = rotationController.CalculateInputToCenter(value.Get<Vector2>().y, Screen.height, playerSettings.sensitivity);

        pitch = rotationController.CalculateAxis(pitchStrength, inputY);
        yaw = rotationController.CalculateAxis(yawStrength, inputX);
        roll = rotationController.CalculateAxis(rollStrength, inputX);
    }

    public void PlayerDeath() {
        controller.SetState<DeathState>();
    }

    public override void OnRecievedDamage(float damage, string id, SoundType soundType){
        base.OnRecievedDamage(damage, id, soundType);
        controller.audioSystem.PlaySoundEffect(soundType);
    }
}
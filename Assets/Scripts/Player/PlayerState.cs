using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public interface IPausable {
    void Pause();
    void UnPause();
}

public abstract class PlayerState : MonoBehaviour, IPausable
{
    [HideInInspector] public PlayerSettings playerSettings;
    [HideInInspector] public PlayerController playerController;
    [HideInInspector] public VesselShipStats shipStats;

    [HideInInspector] public Vector3 currentRotation = Vector3.zero;
    [HideInInspector] public Vector3 currentVelocity = Vector3.zero;

    public float inputX = 0;
    public float inputY = 0;
    public float yaw = 0;
    public float pitch = 0;
    public float roll = 0;

    public float speed = 0;
    public float thrust = 0;

    public float pitchStrength = 0;
    public float yawStrength = 0;
    public float rollStrength = 0;

    public float pitchSteer = 0;
    public float yawSteer = 0;
    public float rollSteer = 0;

    public bool isPaused = false;

    public abstract void BeginState();

    public virtual void EndState() { }

    public void Pause()
    {
        isPaused = true;
    }

    public void UnPause()
    {
        isPaused = false;
    }
}
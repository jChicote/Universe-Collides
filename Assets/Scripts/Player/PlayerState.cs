using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public abstract class PlayerState : MonoBehaviour
{
    [HideInInspector] public InputType inputType;
    [HideInInspector] public CinemachineTransposer cameraTransposer;
    [HideInInspector] public CinemachineComposer cameraComposer;
    [HideInInspector] public PlayerSettings playerSettings;
    [HideInInspector] public PlayerController playerController;

    public float inputX = 0;
    public float inputY = 0;
    public float yaw = 0;
    public float pitch = 0;
    public float roll = 0;

    public bool isPaused = false;

    public abstract void BeginState();

    public virtual void EndState() { }
}
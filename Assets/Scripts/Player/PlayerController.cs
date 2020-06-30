using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public interface IEntity {
    string GetObjectID();
}

public class PlayerController : EntityController
{
    public VesselType vesselSelection;
    public Transform modelTransform;
    public CinemachineVirtualCamera virtualCamera;

    [HideInInspector] public PlayerStateManager playerState = null;
    [HideInInspector] public VesselAudioSystem audioSystem;
    [HideInInspector] public CameraController cameraController;

    void Awake() {
        if (playerState == null) playerState = this.gameObject.AddComponent<PlayerStateManager>();
        audioSystem = this.gameObject.AddComponent<VesselAudioSystem>();
    }

    void Start() {
        cameraController = this.gameObject.AddComponent<CameraController>();
        
        //Sets the state based on selected type
        switch (vesselSelection) {
            case VesselType.xWing:
                SetState<XWingState>();
                break;
        }
    }

    public void SetState<T>() where T : PlayerState {
        playerState.AddState<T>();
    }

    public void OnPause(InputValue value) {
        //Externally triggers pause funciton
        GameManager.Instance.sessionData.TogglePause();
    }
}

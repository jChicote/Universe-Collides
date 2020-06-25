using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class PlayerController : MonoBehaviour
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
        
        switch (vesselSelection) {
            case VesselType.xWing:
                SetState<XWingState>();
                break;
        }
    }

    public void SetState<T>() where T : PlayerState {
        playerState.AddState<T>();
    }

}

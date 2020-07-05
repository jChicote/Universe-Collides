using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    public StatHandler statHandler;

    [HideInInspector] public PlayerStateManager playerState = null;
    [HideInInspector] public VesselAudioSystem audioSystem;
    [HideInInspector] public CameraController cameraController;

    void Awake() {
        if (playerState == null) playerState = this.gameObject.AddComponent<PlayerStateManager>();
        audioSystem = this.gameObject.AddComponent<VesselAudioSystem>();
        PlayerSettings playerSettings = GameManager.Instance.gameSettings.playerSettings;
        BaseStats playerStats = playerSettings.basePlayerStats.Where(x => x.type == vesselSelection).First();
        statHandler = new StatHandler(playerStats);
    }

    void Start() {
        cameraController = this.gameObject.AddComponent<CameraController>();
        
        //Sets the state based on selected type
        switch (vesselSelection) {
            case VesselType.xWing:
                SetState<XWingState>();
                break;
        }

        Debug.Log("Starting Health: " + statHandler.CurrentHealth);
        Debug.Log("Starting Damage: " + statHandler.CriticalDamage);
    }

    public void SetState<T>() where T : PlayerState {
        playerState.AddState<T>();
    }

    public void OnPause(InputValue value) {
        //Externally triggers pause funciton
        GameManager.Instance.sessionData.TogglePause();
    }
}

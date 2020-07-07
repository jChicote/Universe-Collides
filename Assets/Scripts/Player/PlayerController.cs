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
    public CinemachineVirtualCamera virtualCamera;

    [HideInInspector] public PlayerStateManager playerState = null;

    void Awake() {
        if (playerState == null) playerState = this.gameObject.AddComponent<PlayerStateManager>();
        audioSystem = this.gameObject.AddComponent<VesselAudioSystem>();
        PlayerSettings playerSettings = GameManager.Instance.gameSettings.playerSettings;

        IWeaponSystem weaponSystem = this.GetComponent<IWeaponSystem>();
        weaponSystem.SetupSystem(GetObjectID(), this, false);

        GameSettings gameSettings = GameManager.Instance.gameSettings;
        VesselShipStats vesselStats = gameSettings.vesselStats.Where(x => x.type == vesselSelection).First();
        BaseStats playerStats = vesselStats.baseShipStats;
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

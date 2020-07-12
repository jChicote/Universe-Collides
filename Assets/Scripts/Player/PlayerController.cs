using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class PlayerController : BaseEntityController
{
    [SerializeField] private CinemachineVirtualCamera virtualCamera;

    [HideInInspector] public PlayerStateManager playerState = null;

    void Awake() {
        if (playerState == null) playerState = this.gameObject.AddComponent<PlayerStateManager>();
        audioSystem = this.gameObject.AddComponent<VesselAudioSystem>();
        PlayerSettings playerSettings = GameManager.Instance.gameSettings.playerSettings;

        //Initialise Stat Handler
        GameSettings gameSettings = GameManager.Instance.gameSettings;
        VesselShipStats vesselStats = gameSettings.vesselStats.Where(x => x.type == vesselSelection).First();
        BaseStats playerStats = vesselStats.baseShipStats;
        statHandler = new StatHandler(playerStats);

        //Initialise Weapon System
        IWeaponSystem weaponSystem = this.GetComponent<IWeaponSystem>();
        weaponSystem.Init(GetObjectID(), this, false, vesselStats);

        cameraController = this.gameObject.AddComponent<CameraController>();
        cameraController.Init(virtualCamera, vesselSelection);

        //Load weapon/damage components.
        weaponSystem = this.GetComponent<IWeaponSystem>();
        damageSystem = new FighterDamageManager();
        damageSystem.Init(weaponSystem, statHandler);
    }

    void Start() {
        //Sets the state based on selected type
        switch (vesselSelection) {
            case VesselType.xWing:
                SetState<XWingState>();
                break;
        }

        Debug.Log("Starting Health: " + statHandler.CurrentHealth);
        Debug.Log("Starting Damage: " + statHandler.CriticalDamage);
    }

    public void SetState<T>() where T : BaseState {
        playerState.AddState<T>();
    }

    public void OnPause(InputValue value) {
        //Externally triggers pause funciton
        GameManager.Instance.sessionData.TogglePause();
    }
}

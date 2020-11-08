using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

namespace PlayerSystems
{
    public class PlayerController : BaseEntityController
    {
        [HideInInspector] public PlayerStateManager playerState = null;
        

        [Header("Player Virtual Camera")]
        [SerializeField] private CinemachineVirtualCamera virtualCamera;

        public override void Init(SpawnManager spawner, TeamColor teamColor)
        {
            base.Init(spawner, teamColor);

            if (playerState == null) playerState = this.gameObject.AddComponent<PlayerStateManager>();
            audioSystem = this.GetComponent<VesselAudioSystem>();
            audioSystem.Init(EntityType.Player);
            audioSystem.PlayFlightAudio(vesselSelection);
            PlayerSettings playerSettings = GameManager.Instance.gameSettings.playerSettings;

            //Initialise Stat Handler
            GameSettings gameSettings = GameManager.Instance.gameSettings;
            VesselShipStats vesselStats = gameSettings.vesselStats.Where(x => x.type == vesselSelection).First();
            BaseStats playerStats = vesselStats.baseShipStats;
            statHandler = new StatHandler(playerStats, EntityType.Player, this);

            //Initialize Health Component
            HealthComponent healthComponent = this.GetComponent<HealthComponent>();
            healthComponent.Init(statHandler);

            //Initialise Weapon System
            IWeaponSystem weaponSystem = this.GetComponent<IWeaponSystem>();
            weaponSystem.Init(GetObjectID(), this, false, vesselStats);

            cameraController = this.GetComponent<CameraController>();
            cameraController.Init(virtualCamera, vesselSelection);

            //Load weapon/damage components.
            //weaponSystem = this.GetComponent<IWeaponSystem>();
            damageSystem = new FighterDamageManager();
            damageSystem.Init(weaponSystem, statHandler);

            //Load Movement Manager
            MovementRegister movementRegister = this.GetComponent<MovementRegister>();
            Debug.Log(movementRegister);
            movementRegister.Init(this, cameraController);

            SetInitalState();
        }

        void SetInitalState()
        {
            //Sets the state based on selected type
            switch (vesselSelection)
            {
                case VesselType.xWing:
                    SetState<XWingState>();
                    break;
            }
        }

        public void SetState<T>() where T : BaseState
        {
            playerState.AddState<T>();
        }

        public override void OnEntityDeath()
        {
            spawner.OnEntityRespawn.Invoke(objectID, vesselSelection, EntityType.Player, teamColor);
        }
    }
}

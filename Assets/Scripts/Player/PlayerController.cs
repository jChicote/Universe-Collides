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

            GameManager gameManager = GameManager.Instance;
            SceneController sceneController = gameManager.sceneController;

            if (playerState == null) playerState = this.gameObject.AddComponent<PlayerStateManager>();
            audioSystem = this.GetComponent<VesselAudioSystem>();
            audioSystem.Init(EntityType.Player);
            audioSystem.PlayFlightAudio(vesselSelection);
            PlayerSettings playerSettings = gameManager.gameSettings.playerSettings;

            //Initialise Stat Handler
            GameSettings gameSettings = gameManager.gameSettings;
            VesselShipStats vesselStats = gameSettings.vesselStats.Where(x => x.type == vesselSelection).First();
            BaseStats playerStats = vesselStats.baseShipStats;
            statHandler = new StatHandler(playerStats, EntityType.Player, this);

            //Initialize Health Component
            HealthComponent healthComponent = this.GetComponent<HealthComponent>();
            healthComponent.Init(statHandler, sceneController);

            //Initialise Weapon System
            IWeaponSystem weaponSystem = this.GetComponent<IWeaponSystem>();
            weaponSystem.Init(GetObjectID(), this, false, vesselStats, sceneController);

            //Initializes the player's camera system
            cameraController = this.GetComponent<CameraController>();
            cameraController.Init(virtualCamera, vesselSelection);

            //Load weapon/damage components.
            FighterDamageManager damageManager = this.GetComponent<FighterDamageManager>();
            damageManager.Init(statHandler, audioSystem);

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
            base.OnEntityDeath();
            spawner.OnEntityRespawn.Invoke(objectID, vesselSelection, EntityType.Player, teamColor);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PlayerSystems
{
    public class XWingState : PlayerState
    {
        private MovementRegister movementController;

        private float modelAngle;
        private float angleResult;

        public override void BeginState()
        {
            //Load primary player components.
            playerSettings = GameManager.Instance.gameSettings.playerSettings;
            controller = this.GetComponent<PlayerController>();
            objectID = controller.objectID;
            playerRB = this.GetComponent<Rigidbody>();

            //Load game setting.
            GameSettings gameSettings = GameManager.Instance.gameSettings;

            //Load weapon/damage components.
            weaponSystem = this.GetComponent<IWeaponSystem>();
            damageSystem = new FighterDamageManager();
            damageSystem.Init(weaponSystem, controller.statHandler);

            //Load movement controller
            movementController = this.GetComponent<MovementRegister>();
        }

        void FixedUpdate()
        {
            if (isPaused) return;

            //Check if player is dead
            if (controller.statHandler.CurrentHealth <= 0) PlayerDeath();

            movementController.DoMovement();
            movementController.SetRotation();

            weaponSystem.RunSystem();
            weaponSystem.SetAimPosition(speed);
        }
    }

}
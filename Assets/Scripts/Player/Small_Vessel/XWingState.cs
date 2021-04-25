﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PlayerSystems
{
    public class XWingState : PlayerState
    {
        private MovementRegister movementController;

        private IPausableAudio shipAudioPause;

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
            weaponAim = this.GetComponent<IWeaponAim>();

            //Load movement controller
            movementController = this.GetComponent<MovementRegister>();

            //Load Audio Variables
            shipAudioPause = this.GetComponent<IPausableAudio>();
        }

        public override void ActivateDeathState()
        {
            controller.SetState<DeathState>();
        }

        void FixedUpdate()
        {
            if (isPaused) return;

            //Check if player is dead
            //if (controller.statHandler.CurrentHealth <= 0) PlayerDeath();

            movementController.DoMovement();
            movementController.SetRotation();

            weaponSystem.RunSystem();
            weaponAim.SetAimPosition(speed);
        }

        public override void Pause()
        {
            base.Pause();
            shipAudioPause.PauseAllAudio();
        }

        public override void UnPause()
        {
            base.UnPause();
            shipAudioPause.UnpauseAllAudio();
        }
    }
}
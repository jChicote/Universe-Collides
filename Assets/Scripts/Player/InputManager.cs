﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerSystems
{
    public class InputManager : MonoBehaviour
    {
        private IMovementController _movementControl;
        private IWeaponSystem _weaponSystem;
        private IWeaponFire _weaponFire;
        private ICameraControl _cameraControl;

        void Awake()
        {
            _movementControl = this.GetComponent<IMovementController>();
            _weaponSystem = this.GetComponent<IWeaponSystem>();
            _weaponFire = this.GetComponent<IWeaponFire>();
            _cameraControl = this.GetComponent<ICameraControl>();
        }
        
        private void OnPrimaryFire(InputValue value)
        {
            _weaponFire.SetPrimaryFire(value);
        }

        private void OnSecondaryFire(InputValue value)
        {
            _weaponFire.SetSecondaryFire(value);
        }

        private void OnLocking(InputValue value)
        {
            _cameraControl.SetCameraLocking(value);
        }

        private void OnThrottle(InputValue value)
        {
            _movementControl.SetThrottle(value);
        }

        public void OnStickRotation(InputValue value)
        {
            _movementControl.SetStickRotation(value);
        }

        public void OnMouseRotation(InputValue value)
        {
            _movementControl.SetMouseRotation(value);
        }

        // Summary: Externally triggers pause funciton
        //
        private void OnPause(InputValue value)
        {
            GameManager.Instance.sessionData.TogglePause();
        }
    }

}

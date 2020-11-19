using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerSystems
{
    public interface IMovementController
    {
        void SetSteeringStrength(float pitchStrength, float yawStrength, float rollStrength);
        void SetThrottle(InputValue value);
        void SetStickRotation(InputValue value);
        void SetMouseRotation(InputValue value);
    }

    public class MovementRegister : MonoBehaviour, IMovementController
    {
        public Transform childModel;

        private PlayerController playerController;
        private CameraController cameraController;
        private InputMovementSystem movementController;
        private InputRotationController rotationController;
        private PlayerSettings settings;

        private Vector3 _currentRotation = Vector3.zero;
        private Vector3 _currentVelocity = Vector3.zero;

        private bool _isPaused = false;

        private float _inputX = 0;
        private float _inputY = 0;

        private float _pitchStrength = 0;
        private float _yawStrength = 0;
        private float _rollStrength = 0;

        private float _modelAngle;
        private float _angleResult;

        private float _yaw = 0;
        private float _pitch = 0;
        private float _roll = 0;

        private float _speed = 0;

        /// <summary>
        /// Initialises the movemovent controller for player movement and necessary references.
        /// </summary>
        public void Init(PlayerController controller, CameraController cameraControl)
        {
            this.playerController = controller;
            this.cameraController = cameraControl;

            GameManager gameManager = GameManager.Instance;

            //Load shipstates.
            GameSettings gameSettings = gameManager.gameSettings;
            settings = gameSettings.playerSettings;
            VesselShipStats shipStats = gameSettings.vesselStats.Where(x => x.type.Equals(controller.vesselSelection)).First();
            _speed = shipStats.speed;

            //Load Input Controllers
            ThrustUI thrustUI = gameManager.sceneController.gameplayHUD.thrustUI;
            movementController = new InputMovementSystem(this, shipStats, thrustUI);
            rotationController = new InputRotationController();

            movementController.OnShipThrust.AddListener(playerController.audioSystem.CalculateThrustSound);
            movementController.OnShipThrust.AddListener(playerController.audioSystem.CalculateEngineTempo);
        }

        /// <summary>
        /// Performs movment calculations of its speed and velocity
        /// </summary>
        public void DoMovement()
        {
            movementController.CalculateSteeringStrength(playerController.cameraController, _pitchStrength, _yawStrength, _rollStrength);
            _speed = movementController.CalculateCurrentSpeed(playerController.cameraController, _speed);
            _currentVelocity = movementController.CalculateVelocity(_currentVelocity, transform.forward, _speed);
            transform.position += _currentVelocity;
        }

        /// <summary>
        /// Sets the rotation of the vessel
        /// </summary>
        public void SetRotation()
        {
            if (_inputX == 0 || cameraController.isFocused)
            {
                childModel.rotation = Quaternion.Slerp(childModel.rotation, transform.rotation, 0.1f);
                if (_inputX == 0) return;
            }

            _currentRotation = Vector3.zero;
            _currentRotation = new Vector3(-1 * _pitch, _yaw, -1 * _roll);
            transform.Rotate(_currentRotation);

            SetShipRoll();
        }

        /// <summary>
        /// Sets the modified streering strength for rotation
        /// </summary>
        public void SetSteeringStrength(float pitchStrength, float yawStrength, float rollStrength)
        {
            this._pitchStrength = pitchStrength;
            this._yawStrength = yawStrength;
            this._rollStrength = rollStrength;
        }

        /// <summary>
        /// Sets the rotation values of the vessel through the mouse input using 2D vector screen position
        /// </summary>
        public void SetMouseRotation(InputValue value)
        {
            if (_isPaused) return;

            //Scales screen position input to center of screen
            _inputX = rotationController.CalculateInputToCenter(value.Get<Vector2>().x, Screen.width, settings.sensitivity);
            _inputY = rotationController.CalculateInputToCenter(value.Get<Vector2>().y, Screen.height, settings.sensitivity);

            _pitch = rotationController.CalculateAxis(_pitchStrength, _inputY);
            _yaw = rotationController.CalculateAxis(_yawStrength, _inputX);
            _roll = rotationController.CalculateAxis(_rollStrength, _inputX);
        }

        /// <summary>
        /// Sets the rotation values of the vessel through the gamepad right stick
        /// </summary>
        public void SetStickRotation(InputValue value)
        {
            if (_isPaused) return;

            _inputX = value.Get<Vector2>().x * settings.sensitivity;
            _inputY = value.Get<Vector2>().y * settings.sensitivity;

            _pitch = _pitchStrength * _inputY * Time.fixedDeltaTime;
            _yaw = _yawStrength * _inputX * Time.fixedDeltaTime;
            _roll = _rollStrength * _inputX * Time.fixedDeltaTime;
        }

        /// <summary>
        /// Sets the vessel throttle input value for speed using the gamepad left stick
        /// </summary>
        public void SetThrottle(InputValue value)
        {
            if (_isPaused) return;
            _inputY = value.Get<Vector2>().y;

            if (movementController.ThrottleIsZero(_inputY)) return;
            movementController.CalculateThrust(_inputY);
            movementController.CalculateSteering(_inputY);
        }

        // Summary: Modifies the vessel's child model to roll on the z axis
        //
        private void SetShipRoll()
        {
            //Finds roll angle of local model
            _modelAngle = rotationController.CalculateObjectRoll(childModel.localEulerAngles.z);

            //Applied roll rotation of model
            _angleResult = _modelAngle + (-1 * _roll);
            if (_angleResult > -30 && _angleResult < 30)
            {
                childModel.Rotate(0, 0, -1 * _roll);
            }
            else
            {
                childModel.localEulerAngles = new Vector3(0, 0, _modelAngle);
            }
        }
    }
}

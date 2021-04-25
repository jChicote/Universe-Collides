using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace PlayerSystems
{
    public class InputMovementSystem
    {

        private float throttleInput = 0;

        public float thrust = 0;
        public float pitchSteer = 0;
        public float yawSteer = 0;
        public float rollSteer = 0;

        public ThrustEvent OnShipThrust = new ThrustEvent();

        private IMovementController movementController;
        private VesselShipStats shipStats;

        /// <summary>
        /// Constructor for initialising important movement variables for calculation
        /// </summary>
        public InputMovementSystem(IMovementController controller, VesselShipStats shipStats, ThrustUI thrustUI)
        {
            this.movementController = controller;
            this.shipStats = shipStats;
            this.OnShipThrust.AddListener(thrustUI.SetThrustLevel);
        }

        /// <summary>
        /// Calculates the movement velocity of the vessel returning vector
        /// </summary>
        public Vector3 CalculateVelocity(Vector3 currentVelocity, Vector3 forward, float speed)
        {
            currentVelocity = speed * forward * Time.fixedDeltaTime;
            return currentVelocity;
        }

        /// <summary>
        /// Calculates the current speed that the vessel is flying at
        /// </summary>
        public float CalculateCurrentSpeed(CameraController cameraController, float speed)
        {
            cameraController.SetFieldOfView(throttleInput);

            if (thrust == 0 || cameraController.isFocused)
            {
                speed = Mathf.Lerp(speed, shipStats.speed, 0.05f);
            }
            else
            {
                speed = Mathf.Lerp(speed, shipStats.speed + thrust, 0.05f);
            }

            OnShipThrust.Invoke(throttleInput);
            return speed;
        }

        /// <summary>
        /// Calculates the forward thrust input value used for the vessel throttling
        /// </summary>
        public void CalculateThrust(float inputY)
        {
            throttleInput = inputY;
            thrust = shipStats.throttleSpeed * inputY;
        }

        /// <summary>
        /// Calculates the steering modifier of the each rotational axis
        /// </summary>
        public void CalculateSteering(float inputY)
        {
            if (inputY > 0) return;

            pitchSteer = shipStats.extraPitch * (-1 * inputY);
            yawSteer = shipStats.extraYaw * (-1 * inputY);
            rollSteer = shipStats.extraRoll * (-1 * inputY);
        }

        /// <summary>
        /// Calculates the steeting strength of the rotational steer with its modifier
        /// </summary>
        public void CalculateSteeringStrength(CameraController cameraController, float pitchStrength, float yawStrength, float rollStrength)
        {
            if (thrust == 0 || cameraController.isFocused)
            {
                pitchStrength = Mathf.Lerp(pitchStrength, shipStats.pitchRate + pitchSteer, 0.05f);
                yawStrength = Mathf.Lerp(yawStrength, shipStats.yawRate + yawSteer, 0.05f);
                rollStrength = Mathf.Lerp(rollStrength, shipStats.rollRate + rollSteer, 0.05f);
            }
            else
            {
                pitchStrength = Mathf.Lerp(pitchStrength, shipStats.pitchRate, 0.05f);
                yawStrength = Mathf.Lerp(yawStrength, shipStats.yawRate, 0.05f);
                rollStrength = Mathf.Lerp(rollStrength, shipStats.rollRate, 0.05f);
            }

            movementController.SetSteeringStrength(pitchStrength, yawStrength, rollStrength);
        }

        /// <summary>
        /// Resets the calculation movement variables when the input on y is zero
        /// </summary>
        public bool ThrottleIsZero(float inputY)
        {
            if (inputY != 0) return false;
            throttleInput = 0;
            thrust = 0;
            pitchSteer = 0;
            yawSteer = 0;
            rollSteer = 0;
            return true;
        }
    }
}

public class ThrustEvent : UnityEvent<float> {}
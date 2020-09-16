using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class XWingState : PlayerState
{    
    private float modelAngle;
    private float angleResult;

    public override void BeginState()
    {
        //Load primary player components.
        playerSettings = GameManager.Instance.gameSettings.playerSettings;
        controller = this.GetComponent<PlayerController>();
        objectID = controller.objectID;
        playerRB = this.GetComponent<Rigidbody>();

        //Load shipstates.
        GameSettings gameSettings = GameManager.Instance.gameSettings;
        shipStats = gameSettings.vesselStats.Where(x => x.type.Equals(controller.vesselSelection)).First();
        speed = shipStats.speed;

        //Load Input Controllers
        ThrustUI thrustUI = GameManager.Instance.gameplayHUD.thrustUI;
        movementController = new InputMovementController(this, shipStats, thrustUI);
        rotationController = new InputRotationController();

        movementController.OnShipThrust.AddListener(controller.audioSystem.CalculateThrustSound);
        movementController.OnShipThrust.AddListener(controller.audioSystem.CalculateEngineTempo);

        //Load weapon/damage components.
        weaponSystem = this.GetComponent<IWeaponSystem>();
        damageSystem = new FighterDamageManager();
        damageSystem.Init(weaponSystem, controller.statHandler);
    }

    void FixedUpdate()
    {
        if(isPaused) return;

        //Check if player is dead
        if(controller.statHandler.CurrentHealth <= 0) PlayerDeath();

        SetRotation();
        DoMovement();
        //SetThrottling();
        

        weaponSystem.RunSystem();
        weaponSystem.SetAimPosition(speed);
    }

    private void SetRotation() {
        if(inputX == 0 || controller.cameraController.isFocused) {
            controller.modelTransform.rotation = Quaternion.Slerp(controller.modelTransform.rotation, transform.rotation, 0.1f);
            if(inputX == 0) return;
        }

        currentRotation = Vector3.zero;
        currentRotation = new Vector3(-1 * pitch, yaw, -1 * roll);
        transform.Rotate(currentRotation);

        SetShipRoll();
    }

    //Summary:
    //  This sets the rotational roll of the local model of the ship.
    private void SetShipRoll() {
        //Finds roll angle of local model
        modelAngle =rotationController.CalculateObjectRoll(controller.modelTransform.localEulerAngles.z);

        //Applied roll rotation of model
        angleResult = modelAngle + (-1 * roll);
        if (angleResult > -30 && angleResult < 30 )
            controller.modelTransform.Rotate(0, 0, -1 * roll);
        else {
            controller.modelTransform.localEulerAngles = new Vector3(0, 0, modelAngle);
        }
    }
}

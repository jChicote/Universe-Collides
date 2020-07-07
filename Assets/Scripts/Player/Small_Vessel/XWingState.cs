using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class XWingState : PlayerState
{
    Vector3 modelLocalAngles;
    
    float modelAngle;
    float angleResult;

    public override void BeginState()
    {
        //Load primary player components.
        playerSettings = GameManager.Instance.gameSettings.playerSettings;
        controller = this.GetComponent<PlayerController>();
        playerRB = this.GetComponent<Rigidbody>();

        //Load shipstates.
        GameSettings gameSettings = GameManager.Instance.gameSettings;
        shipStats = gameSettings.vesselStats.Where(x => x.type.Equals(controller.vesselSelection)).First();
        speed = shipStats.speed;

        //Load Input Controllers
        movementController = new InputMovementController(this, shipStats);

        //Load weapon/damage components.
        weaponSystem = this.GetComponent<IWeaponSystem>();
        damageSystem = new FighterDamageManager();
        damageSystem.Init(this, weaponSystem, controller.statHandler);
    }

    void FixedUpdate()
    {
        if(isPaused) return;

        ApplyRotation();
        ApplyMovement();
        //SetThrottling();
        

        weaponSystem.RunSystem();
        weaponSystem.SetAimPosition(speed);
    }

    private void ApplyRotation() {
        if(inputX == 0 || controller.cameraController.isFocused) {
            controller.modelTransform.rotation = Quaternion.Slerp(controller.modelTransform.rotation, transform.rotation, 0.1f);
            if(inputX == 0) return;
        }

        currentRotation = Vector3.zero;
        currentRotation = new Vector3(-1 * pitch, yaw, -1 * roll);
        transform.Rotate(currentRotation);

        RollLocalModel();
    }

    private void RollLocalModel() {
        //Finds roll angle of local model
        modelLocalAngles =  controller.modelTransform.localEulerAngles;
        modelAngle = (modelLocalAngles.z > 180) ? modelLocalAngles.z - 360 : modelLocalAngles.z;

        //Applied roll rotation of model
        angleResult = modelAngle + (-1 * roll);
        if (angleResult > -30 && angleResult < 30 )
            controller.modelTransform.Rotate(0, 0, -1 * roll);
        else {
            controller.modelTransform.localEulerAngles = new Vector3(0, 0, modelAngle);
        }
    }
}

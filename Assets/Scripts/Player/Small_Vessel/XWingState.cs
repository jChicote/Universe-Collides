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
        playerSettings = GameManager.Instance.gameSettings.playerSettings;

        playerController = this.GetComponent<PlayerController>();
        playerRB = this.GetComponent<Rigidbody>();
        weaponSystem = this.GetComponent<XWingWeaponSystem>();

        shipStats = GameManager.Instance.gameSettings.vesselStats.Where(x => x.type.Equals(weaponSystem.vesselType)).First();
        speed = shipStats.speed;

        damageSystem = this.gameObject.AddComponent<FighterDamageManager>();
        damageSystem.Init(this, weaponSystem);
        damageSystem.components = shipStats.components;
    }

    void FixedUpdate()
    {
        if(isPaused) return;

        ApplyRotation();
        Movement();
        Throttle();
        weaponSystem.RunSystem();
        weaponSystem.SetAimPosition(speed);
    }

    private void ApplyRotation() {
        if(inputX == 0 || playerController.cameraController.isFocused) {
            playerController.modelTransform.rotation = Quaternion.Slerp(playerController.modelTransform.rotation, transform.rotation, 0.1f);
            if(inputX == 0) return;
        }

        currentRotation = Vector3.zero;
        currentRotation = new Vector3(-1 * pitch, yaw, -1 * roll);
        transform.Rotate(currentRotation);

        //Finds roll angle of local model
        modelLocalAngles =  playerController.modelTransform.localEulerAngles;
        modelAngle = (modelLocalAngles.z > 180) ? modelLocalAngles.z - 360 : modelLocalAngles.z;

        //Applied roll rotation of model
        angleResult = modelAngle + (-1 * roll);
        if (angleResult > -30 && angleResult < 30 )
            playerController.modelTransform.Rotate(0, 0, -1 * roll);
        else {
            playerController.modelTransform.localEulerAngles = new Vector3(0, 0, modelAngle);
        }
    }
}

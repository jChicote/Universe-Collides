using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class XWingWeaponSystem : BaseWeaponSystem
{
    LaserCannon laserCannon;

    public override void Init(string objectID, BaseEntityController controller, bool weaponsActive, VesselShipStats shipStats) {
        base.Init(objectID, controller, weaponsActive, shipStats);

        //Create the aim assist object
        aimAssist = new AimAssist();
        aimAssist.Init(this, GameManager.Instance.gameplayHUD.aimSightUI);

        SetupWeapons();
    }

    void SetupWeapons() {
        laserCannon = this.gameObject.AddComponent<LaserCannon>();
        laserCannon.Init(objectID, false, controller.statHandler.FireRate, shipTransforms.forwardGuns, controller.audioSystem, aimAssist);
    }

    /// <summary>
    /// Runs the primary systems of the weapons
    /// </summary>
    public override void RunSystem() {
        if (canShootPrimary) {
            aimAssist.SetTargetInAimRange();
            laserCannon.Shoot(controller.statHandler.DamageBuff, controller.statHandler.CriticalDamage, SoundType.LaserCannon_1);
        }
    }

    public override void SetAimPosition(float speed)
    {
        float timeAhead = Mathf.Clamp(speed, 3, 50);
        gameManager.gameplayHUD.aimSightUI.SetAimPosition(transform.position, transform.forward, timeAhead, controller.cameraController.isFocused);
    }

    public override void SetFireRate(float fireRate, bool isParallel) {
        laserCannon.ModifyStats(controller.statHandler.FireRate);
    }

    //METHODS BELOW ARE CALLED FROM INPUT SYSTEM
    private void OnPrimaryFire(InputValue value) {
        canShootPrimary = value.isPressed;
    }

    private void OnSecondaryFire(InputValue value) {
        canShootSecondary = value.isPressed;
    }
    private void OnLocking(InputValue value) {
        controller.cameraController.isFocused = value.isPressed;
        controller.cameraController.SetCameraTracking();

        StartCoroutine(controller.cameraController.LockingCamera());
    }
}

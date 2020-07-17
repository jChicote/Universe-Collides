using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class XWingWeaponSystem : BaseWeaponSystem
{
    AimAssist aimAssist;
    LaserCannon laserCannon;
    bool isLocking = false;

    void Start() {
        aimAssist = new AimAssist();
        aimAssist.Init(this, GameManager.Instance.gameplayHUD.aimSightUI);

        SetupWeapons();
    }

    void SetupWeapons() {
        laserCannon = this.gameObject.AddComponent<LaserCannon>();
        laserCannon.Init(objectID, false, controller.statHandler.FireRate, transforms.forwardGuns, controller.audioSystem, aimAssist);
    }

    public override void RunSystem() {
        if (canShootPrimary) {
            aimAssist.CheckWithinAimRange();
            laserCannon.Shoot(controller.statHandler.DamageBuff, controller.statHandler.CriticalDamage, SoundType.LaserCannon_1);
        }
    }

    public override void SetAimPosition(float speed)
    {
        float timeAhead = Mathf.Clamp(speed, 3, 50);
        gameManager.gameplayHUD.aimSightUI.SetAimPosition(transform.position, transform.forward, timeAhead, controller.cameraController.isFocused);
    }

    public override void ChangeFireRate(float fireRate, bool isParallel) {
        laserCannon.ModifyStats(controller.statHandler.FireRate);
    }

    //METHODS BELOW ARE CALLED FROM INPUT SYSTEM
    private void OnPrimaryFire(InputValue value) {
        canShootPrimary = value.isPressed;
    }

    private void OnSecondaryFire(InputValue value) {
        Debug.Log("secondary fire");
        canShootSecondary = value.isPressed;
    }
    private void OnLocking(InputValue value) {
        controller.cameraController.isFocused = value.isPressed;
        controller.cameraController.ModifyCameraTracking();

        StartCoroutine(controller.cameraController.LockingCamera());
    }
}

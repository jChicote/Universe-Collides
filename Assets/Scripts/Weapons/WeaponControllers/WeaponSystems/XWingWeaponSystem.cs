using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class XWingWeaponSystem : BaseWeaponSystem
{
    LaserCannon laserCannon;

    void Start() {
        SetupWeapons();
    }

    void SetupWeapons() {
        laserCannon = this.gameObject.AddComponent<LaserCannon>();
        laserCannon.Init(objectID, false, controller.statHandler.FireRate, transforms.forwardGuns, controller.audioSystem);
    }

    public override void RunSystem() {
        if (canShootPrimary) laserCannon.Shoot(controller.statHandler.DamageBuff, controller.statHandler.CriticalDamage, SoundType.LaserCannon_1);
    }

    public override void SetAimPosition(float speed)
    {
        float timeAhead = Mathf.Clamp(speed, 5, 50);
        Vector3 future = transform.position + transform.forward * timeAhead * 3;
        Debug.DrawLine(transform.position,future);
        gameManager.gameplayHUD.aimSightUI.SetAimPosition(future);
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

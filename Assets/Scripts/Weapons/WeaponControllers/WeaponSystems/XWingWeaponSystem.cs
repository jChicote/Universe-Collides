using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class XWingWeaponSystem : BaseWeaponSystem
{
    LaserCannon laserCannon;
    VesselTransforms transforms;
    
    void Awake() {
        gameManager = GameManager.Instance;
        transforms = this.GetComponent<VesselTransforms>();
        //controller = this.GetComponent<PlayerController>();
    }

    void Start() {
        
        SetupWeapons();
    }

    void SetupWeapons() {
        laserCannon = this.gameObject.AddComponent<LaserCannon>();
        laserCannon.Init(objectID, true, transforms.forwardGuns, controller.audioSystem);
    }

    public override void RunSystem() {
        if (canShootPrimary) laserCannon.Shoot(controller.statHandler.DamageBuff, controller.statHandler.CriticalDamage, SoundType.LaserCannon_1);
    }

    public override void SetAimPosition(float speed)
    {
        Vector3 future = transform.position + transform.forward * speed * 3;
        Debug.DrawLine(transform.position,future);
        GameManager.Instance.gameplayHUD.aimSightUI.SetAimPosition(future);
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

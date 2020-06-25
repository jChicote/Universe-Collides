using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class XWingWeaponSystem : BaseWeaponSystem
{
    PlayerController playerController;
    LaserCannon laserCannon;
    VesselTransforms transforms;
    bool canShootPrimary = false;
    bool canShootSecondary = false;
    
    void Awake() {
        playerController = this.GetComponent<PlayerController>();
        vesselType = VesselType.xWing;
    }

    void Start() {
        Init();
        transforms = this.GetComponent<VesselTransforms>();
        laserCannon = this.gameObject.AddComponent<LaserCannon>();
        SetupWeapons();
    }

    public override void Init()
    {
        base.Init();
    }

    private void SetupWeapons() {
        laserCannon.firePoint = transforms.forwardGuns;
        laserCannon.audioSystem = playerController.audioSystem;
    }

    public override void RunSystem() {
        if (canShootPrimary) laserCannon.Shoot();
    }

    private void OnPrimaryFire(InputValue value) {
        canShootPrimary = value.isPressed;
    }

    private void OnSecondaryFire(InputValue value) {
        Debug.Log("secondary fire");
        canShootSecondary = value.isPressed;
    }

    private void OnLocking(InputValue value) {
        playerController.cameraController.isFocused = value.isPressed;
        playerController.cameraController.ModifyCameraTracking();

        StartCoroutine(playerController.cameraController.LockingCamera());
    }

    public void SetAimPosition(float speed)
    {
        Vector3 future = transform.position + transform.forward * speed * 3;
        Debug.DrawLine(transform.position,future);
        GameManager.Instance.gameplayHUD.aimSightUI.SetAimPosition(future);
    }
}

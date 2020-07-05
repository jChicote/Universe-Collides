using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class XWingWeaponSystem : BaseWeaponSystem
{
    PlayerController playerController;
    FighterDamageManager damageSystem;
    LaserCannon laserCannon;
    VesselTransforms transforms;
    
    void Awake() {
        gameManager = GameManager.Instance;
        playerController = this.GetComponent<PlayerController>();
    }

    void Start() {
        transforms = this.GetComponent<VesselTransforms>();
        damageSystem = this.GetComponent<FighterDamageManager>();
        laserCannon = this.gameObject.AddComponent<LaserCannon>();
        objectID = playerController.objectID;
        laserCannon.shooterID = objectID;
        Init();
    }

    public override void Init() {
        laserCannon.Init(playerController.statHandler);
        laserCannon.firePoint = transforms.forwardGuns;
        laserCannon.audioSystem = playerController.audioSystem;
    }

    public override void RunSystem() {
        if (canShootPrimary) laserCannon.Shoot();
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
        playerController.cameraController.isFocused = value.isPressed;
        playerController.cameraController.ModifyCameraTracking();

        StartCoroutine(playerController.cameraController.LockingCamera());
    }
}

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
        playerController.SetState<XWingState>(); //Sets the player controller to implement the appropriate state
        transforms = this.GetComponent<VesselTransforms>();
        laserCannon = this.gameObject.AddComponent<LaserCannon>();
        laserCannon.firePoint = transforms.forwardGuns;
    }

    public override void Init()
    {
        base.Init();
    }

    public override void RunSystem() {
        if (canShootPrimary) laserCannon.Shoot();
    }

    private void OnPrimaryFire(InputValue value) {
        //Debug.Log("primary fire");
        canShootPrimary = value.isPressed;
    }

    private void OnSecondaryFire(InputValue value) {
        Debug.Log("secondary fire");
        canShootSecondary = value.isPressed;
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class XWingWeaponSystem : BaseWeaponSystem
{
    PlayerController playerController;

    public Transform[] forwardGuns;
    public Transform[] forwardLaunchers;

    void Awake() {
        playerController = this.GetComponent<PlayerController>();
        vesselType = VesselType.xWing;
    }

    void Start() {
        Init();
        playerController.SetState<XWingState>(); //Sets the player controller to implement the appropriate state
    }

    public override void Init()
    {
        base.Init();
    }

    private void OnPrimaryFire(InputValue value) {
        Debug.Log("primary fire");
    }

    private void OnSecondaryFire(InputValue value) {
        Debug.Log("secondary fire");
    }
}

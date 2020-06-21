using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public override void Shoot()
    {
        base.Shoot();
        Debug.Log("is shooting");
    }

    public override string ToString()
    {
        return base.ToString();
    }
}

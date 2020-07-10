using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TieWeaponSystem : BaseWeaponSystem
{
    private LaserCannon laserCannon;

    void Start()
    {
        targetChecker = new TargetDirectionCheck();
        transforms = this.GetComponent<VesselTransforms>();
        detectionDist = shipStats.maxProximityDist / 3; 

        SetupWeapons();
    }

    void SetupWeapons() {
        laserCannon = this.gameObject.AddComponent<LaserCannon>();
        laserCannon.Init(objectID, true, controller.statHandler.FireRate, transforms.forwardGuns, controller.audioSystem);
    }

    public override void RunSystem() {

        if(canShootPrimary) {
            laserCannon.Shoot(controller.statHandler.DamageBuff, controller.statHandler.CriticalDamage, SoundType.LaserCannon_2);
        }
    }
}

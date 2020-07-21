using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TieWeaponSystem : BaseWeaponSystem
{
    private LaserCannon laserCannon;
    //AimAssist aimAssist;

    public override void Init(string objectID, BaseEntityController controller, bool weaponsActive, VesselShipStats shipStats) {
        base.Init(objectID, controller, weaponsActive, shipStats);
        aimAssist = new AimAssist();
        aimAssist.Init(this, GameManager.Instance.gameplayHUD.aimSightUI);

        targetChecker = new TargetDirectionCheck();
        //shipTransforms = this.GetComponent<VesselTransforms>();
        detectionDist = shipStats.maxProximityDist / 3; 

        SetupWeapons();
    }

    void SetupWeapons() {
        laserCannon = this.gameObject.AddComponent<LaserCannon>();
        laserCannon.Init(objectID, true, controller.statHandler.FireRate, shipTransforms.forwardGuns, controller.audioSystem, aimAssist);
    }

    public override void RunSystem() {

        if(canShootPrimary) {
            laserCannon.Shoot(controller.statHandler.DamageBuff, controller.statHandler.CriticalDamage, SoundType.LaserCannon_2);
        }
    }
}

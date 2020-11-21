using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TieWeaponSystem : BaseWeaponSystem
{
    private LaserCannon laserCannon;
    //AimAssist aimAssist;

    public override void Init(string objectID, BaseEntityController controller, bool weaponsActive, VesselShipStats shipStats, SceneController sceneController) {
        base.Init(objectID, controller, weaponsActive, shipStats, sceneController);
        aimAssist = new AimAssist();
        aimAssist.Init(this, sceneController.dynamicHud.aimSightUI);

        targetChecker = new TargetDirectionCheck();
        //shipTransforms = this.GetComponent<VesselTransforms>();
        detectionDist = shipStats.maxProximityDist / 3; 

        SetupWeapons();
    }

    void SetupWeapons() {
        laserCannon = this.gameObject.AddComponent<LaserCannon>();
        //laserCannon.Init(objectID, controller.statHandler.FireRate, controller.audioSystem, aimAssist);
    }

    public override void RunSystem() {

        /*if(canShootPrimary) {
            laserCannon.Shoot(controller.statHandler.DamageBuff, controller.statHandler.CriticalDamage);
        }*/
    }
}

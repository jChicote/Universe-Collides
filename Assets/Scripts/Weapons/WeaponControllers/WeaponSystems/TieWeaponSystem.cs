using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TieWeaponSystem : BaseWeaponSystem
{
    //private EnemyController controller;
    private LaserCannon laserCannon;
    private VesselTransforms transforms;
    private TargetDirectionCheck targetChecker;

    float detectionDist;

    void Awake()
    {
        gameManager = GameManager.Instance;
        controller = this.GetComponent<EnemyController>();
        shipStats = GameManager.Instance.gameSettings.vesselStats.Where(x => x.type.Equals(controller.vesselSelection)).First();
    }

    void Start()
    {
        targetChecker = new TargetDirectionCheck();
        transforms = this.GetComponent<VesselTransforms>();
        //objectID = controller.GetObjectID();
        detectionDist = shipStats.maxProximityDist / 3; 

        SetupWeapons();
    }

    void SetupWeapons() {
        laserCannon = this.gameObject.AddComponent<LaserCannon>();
        laserCannon.Init(objectID, true, transforms.forwardGuns, controller.audioSystem);
    }

    public override void RunSystem() {

        if(canShootPrimary) {
            laserCannon.Shoot(controller.statHandler.DamageBuff, controller.statHandler.CriticalDamage, SoundType.LaserCannon_2);
        }
    }

    public override bool CheckAimDirection() {
        if(target == null) return false;
        Vector3 targetPos = target.transform.position;

        if(!targetChecker.CheckInDirection(targetPos ,transform.position, detectionDist)) {
            return false;
        }

        if(!targetChecker.CheckInView(targetPos, transform.position, transform.forward, 0.7f)){
            canShootPrimary = false;
            return false;
        } else {
            canShootPrimary = true;
            return true;
        }
    }
}

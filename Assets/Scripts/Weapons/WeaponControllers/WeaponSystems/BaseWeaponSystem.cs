using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeaponSystem {
    void Init(string objectID, BaseEntityController controller, bool weaponsActive, VesselShipStats shipStats);
    void RunSystem();
    bool CheckAimDirection();
    void SetAimPosition(float speed);
    void AssignTarget(GameObject target);
}

public class BaseWeaponSystem : MonoBehaviour, IWeaponSystem
{
    public string objectID;
    public BaseEntityController controller;
    public GameObject target;
    public GameManager gameManager;
    public VesselShipStats shipStats;
    public TargetDirectionCheck targetChecker;

    public VesselTransforms transforms;

    public bool canShootPrimary = false;
    public bool canShootSecondary = false;

    public float detectionDist;

    public virtual void Init(string objectID, BaseEntityController controller, bool weaponsActive, VesselShipStats shipStats) {
        this.objectID = objectID;
        this.controller = controller;
        this.shipStats = shipStats;
        gameManager = GameManager.Instance;
        canShootPrimary = weaponsActive;
        canShootSecondary = weaponsActive;
        transforms = this.GetComponent<VesselTransforms>();
    }

    public virtual void RunSystem() {}
    
    public virtual void SetAimPosition(float speed) {}

    public void AssignTarget(GameObject target) {
        this.target = target;
    }

    public bool CheckAimDirection() {
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

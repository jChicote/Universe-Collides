using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeaponSystem {
    void SetupSystem(string objectID, EntityController controller, bool weaponsActive);
    void RunSystem();
    bool CheckAimDirection();
    void SetAimPosition(float speed);
    void AssignTarget(GameObject target);
}

public class BaseWeaponSystem : MonoBehaviour, IWeaponSystem
{
    public string objectID;
    public EntityController controller;
    public GameObject target;
    public GameManager gameManager;
    public VesselType vesselType;
    public VesselShipStats shipStats;

    public bool canShootPrimary = false;
    public bool canShootSecondary = false;

    public virtual void Init() {}

    public virtual void SetupSystem(string objectID, EntityController controller, bool weaponsActive) {
        this.objectID = objectID;
        this.controller = controller;
        canShootPrimary = weaponsActive;
        canShootSecondary = weaponsActive;
    }

    public virtual void RunSystem() {}
    
    public virtual void SetAimPosition(float speed) {}
    public void AssignTarget(GameObject target) {
        this.target = target;
    }

    public virtual bool CheckAimDirection() { return false; }
}

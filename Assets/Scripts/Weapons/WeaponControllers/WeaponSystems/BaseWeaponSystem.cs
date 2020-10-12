﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public interface IWeaponSystem {
    void Init(string objectID, BaseEntityController controller, bool weaponsActive, VesselShipStats shipStats);
    void RunSystem();
    bool CheckAimDirection();
    void SetAimPosition(float speed);
    void SetNewTarget(GameObject target);
    void SetFireRate(float fireRate, bool isParallel);

    void SetPrimaryFire(InputValue value);
    void SetSecondaryFire(InputValue value);
}

public class BaseWeaponSystem : MonoBehaviour, IWeaponSystem
{
    public string objectID;
    public BaseEntityController controller;
    public GameObject target;
    public GameManager gameManager;
    public VesselShipStats shipStats;
    public TargetDirectionCheck targetChecker;
    public AimAssist aimAssist;

    public VesselTransforms shipTransforms;

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
        shipTransforms = this.GetComponent<VesselTransforms>();
    }

    public virtual void RunSystem() {}
    
    public virtual void SetAimPosition(float speed) {}
    public virtual void SetFireRate(float fireRate, bool isParallel) {}

    public void SetNewTarget(GameObject target) {
        this.target = target;
    }

    public bool CheckAimDirection() {
        if(target == null || targetChecker == null) return false;
        Vector3 targetPos = target.transform.position;

        if(!targetChecker.TargetInDirection(targetPos ,transform.position, detectionDist)) {
            return false;
        }

        if(!targetChecker.TargetInView(targetPos, transform.position, transform.forward, 0.7f)){
            canShootPrimary = false;
            return false;
        } else {
            canShootPrimary = true;
            return true;
        }
    }

    public virtual void SetPrimaryFire(InputValue value) { }

    public virtual void SetSecondaryFire(InputValue value) { }
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public interface IWeaponSystem 
{
    void Init(string objectID, BaseEntityController controller, bool weaponsActive, VesselShipStats shipStats, SceneController sceneController);
    void RunSystem();
}

public interface IWeaponFire
{
    void SetPrimaryFire(InputValue value);
    void SetSecondaryFire(InputValue value);
}

public interface IWeaponAim
{
    bool CheckAimDirection();
    void SetAimPosition(float speed);
}

public abstract class BaseWeaponSystem : MonoBehaviour, IWeaponSystem, IWeaponAim, IWeaponFire
{
    //Constants
    [HideInInspector] public string objectID;

    [HideInInspector] public BaseEntityController controller;
    [HideInInspector] public GameManager gameManager;
    [HideInInspector] public VesselShipStats shipStats;
    [HideInInspector] public TargetDirectionCheck targetChecker;
    [HideInInspector] public AimAssist aimAssist;

    [HideInInspector] public GameObject target;

    public float detectionDist; //TODO: Move this to the AI weapon system

    public virtual void Init(string objectID, BaseEntityController controller, bool weaponsActive, VesselShipStats shipStats, SceneController sceneController) {
        this.objectID = objectID;
        this.controller = controller;
        this.shipStats = shipStats;
        gameManager = GameManager.Instance;
    }

    public abstract void RunSystem();

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
            return false;
        } else {
            return true;
        }
    }

    public virtual void SetPrimaryFire(InputValue value) { }

    public virtual void SetSecondaryFire(InputValue value) { }
}

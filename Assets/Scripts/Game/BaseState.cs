using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//MOVEMENT NEEDS TO BE REFACTORED
public interface IMovementControl
{
    Vector3 GetVelocity();
}

public abstract class BaseState : MonoBehaviour, IPausable, IMovementControl
{
    public string objectID;

    protected IWeaponSystem weaponSystem;
    protected IWeaponFire weaponFire;
    protected IWeaponAim weaponAim;

    [HideInInspector] public Vector3 currentRotation = Vector3.zero;
    [HideInInspector] public Vector3 currentVelocity = Vector3.zero;
    [HideInInspector] public VesselShipStats shipStats;

    public float yaw = 0;
    public float pitch = 0;
    public float roll = 0;

    public float speed = 0;

    public bool isPaused = false;

    public abstract void BeginState();

    public virtual void RunState(){}

    public virtual void EndState() { }

    public virtual void ActivateDeathState() { }

    public virtual void Pause()
    {
        isPaused = true;
    }

    public virtual void UnPause()
    {
        isPaused = false;
    }

    public Vector3 GetVelocity()
    {
        return currentVelocity;
    }
}

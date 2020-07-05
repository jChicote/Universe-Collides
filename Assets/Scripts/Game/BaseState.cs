using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState : MonoBehaviour, IPausable
{
    [HideInInspector] public BaseWeaponSystem weaponSystem;
    [HideInInspector] public DamageManager damageSystem;
    [HideInInspector] public Vector3 currentRotation = Vector3.zero;
    [HideInInspector] public Vector3 currentVelocity = Vector3.zero;
    [HideInInspector] public VesselShipStats shipStats;

    public float yaw = 0;
    public float pitch = 0;
    public float roll = 0;

    public float speed = 0;
    public float thrust = 0;

    public bool isPaused = false;
    public bool isEngineDamaged = false;

    public abstract void BeginState();

    public virtual void RunState(){}

    public virtual void EndState() { }

    public void Pause()
    {
        isPaused = true;
    }

    public void UnPause()
    {
        isPaused = false;
    }
}

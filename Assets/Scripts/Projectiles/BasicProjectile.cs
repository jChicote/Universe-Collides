using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasicProjectile : MonoBehaviour, IPausable
{
    public string shooterID; 
    public Rigidbody projectileRB;
    public WeaponInfo info;
    public float speed;
    public DamageInfo damageInfo;
    public float lifeTime;
    public float tickToDestroy = 0;
    public bool isPaused = false;

    public virtual void OnLaunch() {}

    public virtual void OnTravel() { }
    
    public virtual void DestroyObject(float time) { }

    public void Pause()
    {
        isPaused = true;
    }

    public void UnPause()
    {
        isPaused = false;
    }
}

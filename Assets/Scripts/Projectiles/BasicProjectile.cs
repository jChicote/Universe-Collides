using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageReciever { 
    void OnRecievedDamage();
}

public abstract class BasicProjectile : MonoBehaviour, IPausable
{
    public string ownerID; 
    public Rigidbody projectileRB;
    public float speed;
    public float damage;
    public float lifeTime;
    public bool isPaused = false;

    public abstract void OnLaunch();

    public virtual void OnTravel() { }
    
    public virtual void OnDestroy() { }

    public void Pause()
    {
        isPaused = true;
    }

    public void UnPause()
    {
        isPaused = false;
    }
}

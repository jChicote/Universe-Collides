using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasicProjectile : MonoBehaviour
{
    public string ownerID; 
    public Rigidbody projectileRB;
    public float speed;
    public float damage;
    public float lifeTime;

    public abstract void OnLaunch();

    public virtual void OnTravel() { }
    
    public virtual void OnDestroy() { }
}

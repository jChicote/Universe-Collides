using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseProjectile : MonoBehaviour
{
    public Rigidbody projectileRB;
    public string ownerID;
    public float speed;
    public float damage;

    void Awake() {
        projectileRB = this.GetComponent<Rigidbody>();
    }

    public void Init(string ownerID, float damage) {
        this.ownerID = ownerID;
        this.damage = damage;
    }

    public virtual void TravelToTarget() {}

    public virtual void OnDestroy() {}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Photon : BasicProjectile
{
    public Transform target;
    
    Vector3 trackingDir;
    Quaternion trackingRot;
    bool isTracking = false;

    void Awake() {
        speed = info.speed;
        damageInfo = info.damageInfo;
        lifeTime = info.lifeTime;
        projectileRB = this.GetComponent<Rigidbody>();
    }

    void Start() {
        if(target != null) {
            Invoke("BeginTracking", 2f);
        }
    }

    void FixedUpdate()
    {
        if(isPaused) {
            projectileRB.velocity = Vector3.zero;
            return;
        }

        if(tickToDestroy > lifeTime) DestroyObject(0);
        if(isTracking) TrackTarget();

        projectileRB.velocity += transform.forward * speed;
        tickToDestroy += Time.fixedDeltaTime;
    }

    void BeginTracking() {
        isTracking = true;
    }

    void TrackTarget() {
        trackingDir = target.transform.position - transform.position;
        trackingRot = Quaternion.LookRotation(trackingDir);
        transform.rotation = Quaternion.Slerp(transform.rotation, trackingRot, Time.fixedDeltaTime);
    }

    public override void DestroyObject(float time)
    {
        Destroy(gameObject, time);
    }

    private void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.gameObject.GetComponent<IDamageReciever>() == null) {
            DestroyObject(0);
            return;
        }

        IDamageReciever victim = collisionInfo.gameObject.GetComponent<IDamageReciever>();
        victim.OnRecievedDamage(damageInfo, collisionInfo.collider.name);
    }
}

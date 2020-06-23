using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBolt : BasicProjectile
{
    void Awake() {
        OnLaunch();
    }

    void Start() {
        GameObject.Destroy(gameObject, lifeTime);
    }
    public override void OnLaunch()
    {
        projectileRB = this.GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        projectileRB.velocity += transform.forward * speed;
    }

    public override void OnDestroy()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.gameObject.GetComponent<IDamageReciever>() == null) {
            OnDestroy();
            return;
        }

        IDamageReciever victim = collisionInfo.gameObject.GetComponent<IDamageReciever>();
        victim.OnRecievedDamage();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBolt : BasicProjectile
{
    float tickToDestroy = 0;

    void Awake() {
        OnLaunch();
    }

    public override void OnLaunch()
    {
        projectileRB = this.GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if(isPaused) {
            projectileRB.velocity = Vector3.zero;
            return;
        }

        if(tickToDestroy > lifeTime) OnDestroy();

        projectileRB.velocity += transform.forward * speed;
        tickToDestroy += Time.fixedDeltaTime;
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

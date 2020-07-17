using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LaserBolt : BasicProjectile
{
    void Awake() {
        projectileRB = this.GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if(isPaused) {
            projectileRB.velocity = Vector3.zero;
            return;
        }

        if(tickToDestroy > lifeTime) DestroyObject(0);

        projectileRB.velocity += transform.forward * speed;
        tickToDestroy += Time.fixedDeltaTime;
    }

    void SpawnImpact(ContactPoint contactData) {
        Vector3 impactAngle = contactData.normal;
        Quaternion impactRot = Quaternion.FromToRotation(Vector3.up, impactAngle);
        WeaponSettings weaponSettings = GameManager.Instance.gameSettings.weaponSettings;
        GameObject effect = weaponSettings.impactEffects.Where(x => x.type == ImpactType.SparkImpact).First().effect;
        Destroy(Instantiate(effect, contactData.point, impactRot), 5);
    }

    public override void DestroyObject(float time)
    {
        Destroy(gameObject, time);
    }

    private void OnCollisionEnter(Collision collisionInfo)
    {
        string objectID = collisionInfo.gameObject.GetComponent<IIdentity>().GetObjectID();
        if(objectID == shooterID) return;

        if (collisionInfo.gameObject.GetComponent<IDamageReciever>() == null) {
            SpawnImpact(collisionInfo.GetContact(0));
            DestroyObject(0);
            return;
        }

        SpawnImpact(collisionInfo.GetContact(0));

        IDamageReciever victim = collisionInfo.gameObject.GetComponent<IDamageReciever>();
        victim.OnRecievedDamage(damage, shooterID);

        string playerID = GameManager.Instance.playerController.GetObjectID();
        if(shooterID == playerID) GameManager.Instance.scoreManager.OnPlayerHitChain.Invoke();

        DestroyObject(0);
    }
}

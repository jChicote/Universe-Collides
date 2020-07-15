using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathState : BaseState
{
    IEntity entity;
    Rigidbody entityRB;

    public override void BeginState()
    {
        entity = this.GetComponent<IEntity>();
        entity.OnEntityDeath();

        entityRB = this.GetComponent<Rigidbody>();
        RigidbodyConstraints deathConstraints = RigidbodyConstraints.None;
        entityRB.constraints = deathConstraints;
        Invoke("RemoveFromScene", 3f);

        //Add death force
        entityRB.AddForce((transform.forward * 10) + transform.up * 4, ForceMode.Force);
    }

    void FixedUpdate()
    {
        //Add death force
        entityRB.AddForce((transform.forward * 10) + transform.up * 4, ForceMode.Force);
    }

    public void RemoveFromScene() {
        Destroy(gameObject);
    }

    public override void OnRecievedDamage(float damage, string id) {
        //Debug.Log("Damage on Death");
    }

}

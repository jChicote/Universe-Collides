using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DeathState : BaseState
{
    BaseEntityController controller;
    Rigidbody entityRB;

    float shipSpeed = 0;

    public override void BeginState()
    {

        controller = this.GetComponent<BaseEntityController>();
        controller.OnEntityDeath();

        GameSettings gameSettings = GameManager.Instance.gameSettings;
        VesselShipStats vesselStats = gameSettings.vesselStats.Where(x => x.type == controller.vesselSelection).First();
        shipSpeed = vesselStats.speed;

        entityRB = this.GetComponent<Rigidbody>();
        //entityRB.isKinematic = false;
        RigidbodyConstraints deathConstraints = RigidbodyConstraints.None;
        entityRB.constraints = deathConstraints;
        Invoke("RemoveFromScene", 3f);

        //Add death force
        //entityRB.AddExplosionForce(2, (transform.forward * -2) + transform.localPosition, 3f, 0);
        //entityRB.add
    }

    void FixedUpdate()
    {
        //Add death force
        //entityRB.AddForce((transform.forward * 10) + transform.up * 4, ForceMode.Force);
        transform.Rotate(0.5f, 2f, 2f);
        transform.position += transform.forward * (shipSpeed * 2f) * Time.fixedDeltaTime;
    }

    public void RemoveFromScene() {
        Destroy(gameObject);
    }

    public override void OnRecievedDamage(float damage, string id, SoundType soundType) {
        controller.audioSystem.PlaySoundEffect(soundType);
    }

}

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
        entityRB.isKinematic = true;
        RigidbodyConstraints deathConstraints = RigidbodyConstraints.None;
        entityRB.constraints = deathConstraints;
        Invoke("RemoveFromScene", 3f);
    }

    void FixedUpdate()
    {
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

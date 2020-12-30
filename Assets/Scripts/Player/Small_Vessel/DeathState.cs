using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DeathState : BaseState
{
    public enum DeathTypes
    {
        SpinDeath,
        InstantDeath,
        VesselSegregation,
        RollDeath
    }

    private DeathTypes deathSelection;
    private BaseEntityController controller;
    private Rigidbody entityRB;

    private float shipSpeed = 0;

    public override void BeginState()
    {

        controller = this.GetComponent<BaseEntityController>();
        controller.OnEntityDeath();

        GameSettings gameSettings = GameManager.Instance.gameSettings;
        VesselShipStats vesselStats = gameSettings.vesselStats.Where(x => x.type == controller.vesselSelection).First();
        shipSpeed = vesselStats.speed;

        entityRB = this.GetComponent<Rigidbody>();
        //entityRB.isKinematic = true;
        RigidbodyConstraints deathConstraints = RigidbodyConstraints.None;
        entityRB.constraints = deathConstraints;
        Invoke("Explode", 2f);

        deathSelection = (DeathTypes)Random.Range(0, 3);
    }

    void FixedUpdate()
    {
        transform.position += transform.forward * (shipSpeed * 2f) * Time.fixedDeltaTime;
        
        switch (deathSelection)
        {
            case DeathTypes.SpinDeath:
                SpinDeath();
                break;
            case DeathTypes.InstantDeath:
                InstantDeath();
                break;
            case DeathTypes.VesselSegregation:
                VesselSegregationDeath();
                break;
            case DeathTypes.RollDeath:
                RollDeath();
                break;
        }
    }

    private void SpinDeath()
    {
        transform.Rotate(0.5f, 2f, 2f);
    }

    private void InstantDeath()
    {
        Explode();
    }

    private void VesselSegregationDeath()
    {
        transform.Rotate(0f, 0f, 5f);
        //Run segregation systems
    }

    private void RollDeath()
    {
        transform.Rotate(0f, 0f, 10f);
    }

    public void Explode() 
    {
        //Run animation explosion
        GameObject effectsPrefab = GameManager.Instance.gameSettings.effectsSettings.shipExplosions[0]; // Getting first for now
        Instantiate(effectsPrefab, transform.position, transform.rotation);

        IExplosionShake explosionShaker;

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 1000f);
        foreach (Collider collision in hitColliders)
        {
            //Debug.Log(collision.gameObject.transform.parent.name);
            if (collision.gameObject.transform.root.GetComponent<IExplosionShake>() != null)
            {
                Debug.Log("Has encountered camera");
                explosionShaker = collision.gameObject.transform.root.GetComponent<IExplosionShake>();
                explosionShaker.TriggerExplosionShake(transform.position);
            }
        }

        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
       if (collision.gameObject.CompareTag("Player"))
       {
            Explode();
       }
    }

    /*blic override void OnRecievedDamage(float damage, string id, SoundType soundType) {
        controller.audioSystem.PlaySoundEffect(soundType);
    }*/

}

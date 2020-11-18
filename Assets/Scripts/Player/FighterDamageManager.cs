using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FighterDamageManager : DamageManager, IDamageReciever
{
    private HealthComponent healthComponent;
    private VesselAudioSystem audioSystem;

    public void Init(StatHandler statHandler, VesselAudioSystem audioSystem)
    {
        this.statHandler = statHandler;
        this.audioSystem = audioSystem;

        healthComponent = this.GetComponent<HealthComponent>();
    }

    //SET Death state through here
    public override void OnDeath(Vector3 position)
    {
        //Debug.Log("is Dead");
    }

    public override void CalculateHealth(float damage, string id)
    {
        float health = statHandler.CurrentHealth - damage;
        healthComponent.SetHealthUpdate(health);
        Debug.Log("Health: " + health);
    }

    public void OnRecievedDamage(float damage, string id, SoundType soundType)
    {
        float health = statHandler.CurrentHealth - damage;
        healthComponent.SetHealthUpdate(health);
        audioSystem.PlaySoundEffect(soundType);
        Debug.Log("Health: " + health);
    }

    public string GetObjectID()
    {
        throw new System.NotImplementedException();
    }
}

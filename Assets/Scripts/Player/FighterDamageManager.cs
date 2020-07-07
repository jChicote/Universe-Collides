using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FighterDamageManager : DamageManager
{
    public override void Init(BaseState state, IWeaponSystem weaponSystem, StatHandler statHandler) {
        this.shipState = state;
        this.weaponSystem = weaponSystem;
        this.statHandler = statHandler;
    }

    public override void OnDeath(Vector3 position)
    {
        //Debug.Log("is Dead");
    }

    public override void CalculateHealth(float damage, string id)
    {
        float health = statHandler.CurrentHealth - damage;
        statHandler.CurrentHealth = health;
        Debug.Log("Damage: " + damage);
        Debug.Log("Health: " + health);
    }
}

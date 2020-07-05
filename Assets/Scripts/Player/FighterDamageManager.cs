using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FighterDamageManager : DamageManager
{
    public override void Init(BaseState state, BaseWeaponSystem weaponSystem) {
        this.shipState = state;
        this.weaponSystem = weaponSystem;
    }

    public override void OnDeath()
    {
        Debug.Log("is Dead");
    }

    public override void OnRecievedDamage(float damage, string id)
    {
        Debug.Log(damage);
        Debug.Log(id);
    }
}

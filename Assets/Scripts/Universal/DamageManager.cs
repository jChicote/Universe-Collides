using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageReciever { 
    void OnRecievedDamage(float damage, string id);
}

public abstract class DamageManager : MonoBehaviour, IDamageReciever
{
    public BaseState shipState;
    public BaseWeaponSystem weaponSystem;
    public ShipComponent[] components;

    public abstract void Init(BaseState state, BaseWeaponSystem weaponSystem);

    public abstract void OnRecievedDamage(float damage, string id);
    
    public abstract void OnDeath();
}

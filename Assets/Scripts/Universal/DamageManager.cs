using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageReciever { 
    void OnRecievedDamage(float damage, string id);
    string GetObjectID();
}

public abstract class DamageManager
{
    public BaseState shipState;
    public IWeaponSystem weaponSystem;
    public StatHandler statHandler;

    public abstract void Init(BaseState state, IWeaponSystem weaponSystem, StatHandler statHandler);

    public abstract void CalculateHealth(float damage, string id);
    
    public abstract void OnDeath(Vector3 position);
}

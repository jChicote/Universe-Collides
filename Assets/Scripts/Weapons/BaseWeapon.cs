using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeaponItem
{
    void Init(string id, float fireRate, VesselAudioSystem audioSystem, AimAssist aimAssist, ProjectileType projectileType);
    void ModifyStats(float fireRate);
    void Shoot(float damageBuff, float criticalDamage);
    WeaponType GetWeaponType();
}

public abstract class BaseWeapon : MonoBehaviour, IWeaponItem
{
    public WeaponType weaponType;
    [HideInInspector] public ProjectileData projectileData;
    [HideInInspector] public VesselAudioSystem audioSystem;
    
    public float fireRate;
    public bool isShooting = true;
    public int transformIndex = 0;

    public string shooterID;

    public virtual void Init(string id, float fireRate, VesselAudioSystem audioSystem, AimAssist aimAssist, ProjectileType projectileType) { }

    public virtual void ModifyStats(float fireRate) { }

    public virtual WeaponType GetWeaponType()
    {
        return weaponType;
    }

    public virtual void Shoot(float damageBuff, float criticalDamage) {}
}

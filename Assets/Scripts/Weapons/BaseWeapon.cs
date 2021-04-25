using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeaponItem
{
    void Init(string id, WeaponInfo weaponInfo, VesselAudioSystem audioSystem, AimAssist aimAssist, ProjectileType projectileType);
    void ModifyStats(float fireRate);
    void Shoot(float damageBuff, float criticalDamage);
    WeaponType GetWeaponType();
}

public abstract class BaseWeapon : MonoBehaviour, IWeaponItem
{
    public WeaponType weaponType;
    [HideInInspector] public ProjectileData projectileData;
    [HideInInspector] public VesselAudioSystem audioSystem;

    public SoundType weaponSound;

    public float fireRate;
    public bool canRecoilSway = false;
    public bool isShooting = true;
    public int transformIndex = 0;

    public string shooterID;

    private float swayOffset = 0;

    public virtual void Init(string id, WeaponInfo weaponInfo, VesselAudioSystem audioSystem, AimAssist aimAssist, ProjectileType projectileType) { }

    public virtual void ModifyStats(float fireRate) { }

    public virtual WeaponType GetWeaponType()
    {
        return weaponType;
    }

    public virtual void Shoot(float damageBuff, float criticalDamage) 
    {
        if (canRecoilSway)
        {
            CancelInvoke();
            swayOffset += 0.5f;
            swayOffset = Mathf.Clamp(swayOffset, 0, 3f);
            Invoke("CoolRecoil", 2f);
        }
    }

    public virtual Quaternion ApplyRocoilSway(Quaternion direction)
    {
        float xOffset = Random.Range(0f, swayOffset);
        float yOffset = Random.Range(0f, swayOffset);
        float zOffset = Random.Range(0f, swayOffset);

        Quaternion offsetRotation = Quaternion.Euler(xOffset, yOffset, zOffset);
        return direction * offsetRotation;
    }

    public void CoolRecoil()
    {
        swayOffset = 0;
    }
}

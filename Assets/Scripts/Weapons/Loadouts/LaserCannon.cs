using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class LaserCannon : BaseWeapon
{
    public Transform point;

    private AimAssist aimAssist;

    public override void Init(string id, WeaponInfo weaponInfo, VesselAudioSystem audioSystem, AimAssist aimAssist, ProjectileType projectileType) {
        WeaponData weaponData = weaponInfo.weaponData;
        
        this.shooterID = id;
        this.audioSystem = audioSystem;
        this.fireRate = weaponData.fireRate;
        this.aimAssist = aimAssist;

        weaponSound = weaponData.fireSound;
        WeaponSettings settings = GameManager.Instance.gameSettings.weaponSettings;
        this.projectileData = settings.projectiles.Where(x => x.type == projectileType).First();
    }

    public override void Shoot(float damageBuff, float critDamage)
    {
        base.Shoot(damageBuff, critDamage);
        if(!isShooting) return;

        Quaternion rotation = aimAssist.CalculateAimDirection(point);
        if (canRecoilSway) rotation = ApplyRocoilSway(rotation);
        LaserBolt bolt = Instantiate(projectileData.prefab, point.position, rotation).GetComponent<LaserBolt>();
        SetProjectileInfo(bolt, damageBuff, critDamage);
        audioSystem.PlaySoundEffect(weaponSound);
    }

    private void SetProjectileInfo(LaserBolt bolt, float damageBuff, float critDamage) {
        bolt.speed = projectileData.speed;
        bolt.damage = (projectileData.damage + damageBuff) + critDamage;
        bolt.lifeTime = projectileData.lifeTime;
        bolt.shooterID = shooterID;
    }
}

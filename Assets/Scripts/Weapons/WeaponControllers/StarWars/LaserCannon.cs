using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LaserCannon : BaseWeapon
{
    private bool isParallelFire = false;
    AimAssist aimAssist;

    void Start()
    {
        WeaponSettings settings = GameManager.Instance.gameSettings.weaponSettings;
        info = settings.weaponStats.Where(x => x.type == WeaponType.LaserBolt).First();
    }

    public void Init(string id, bool enableParrallel, float fireRate, Transform[] cannons, VesselAudioSystem audioSystem, AimAssist aimAssist) {
        this.shooterID = id;
        this.isParallelFire = enableParrallel;
        this.firePoint = cannons;
        this.audioSystem = audioSystem;
        this.fireRate = fireRate;
        this.aimAssist = aimAssist;
    }

    public override void Shoot(float damageBuff, float critDamage, SoundType type)
    {
        if(!isShooting) return;

        if(isParallelFire) {
            ParallelFire(damageBuff, critDamage);
            audioSystem.PlaySoundEffect(type);
            return;
        }

        SequentialFire(damageBuff, critDamage);
        audioSystem.PlaySoundEffect(type);
    }

    private void ParallelFire(float damageBuff, float critDamage) {
        foreach(Transform point in firePoint) {
            Quaternion rotation = aimAssist.CalculateAimDirection(point);
            LaserBolt bolt = Instantiate(info.prefab, point.position, rotation).GetComponent<LaserBolt>();
            SetProjectileInfo(bolt, damageBuff, critDamage);
        }

        StartCoroutine(Reload());
    }

    private void SequentialFire(float damageBuff, float critDamage) {
        Quaternion rotation = aimAssist.CalculateAimDirection(firePoint[transformIndex]);
        LaserBolt bolt = Instantiate(info.prefab, firePoint[transformIndex].position, rotation).GetComponent<LaserBolt>();
        SetProjectileInfo(bolt, damageBuff, critDamage);
        StartCoroutine(Reload());
        
        transformIndex++;
        if(transformIndex == firePoint.Length) transformIndex = 0;
    }

    private void SetProjectileInfo(LaserBolt bolt, float damageBuff, float critDamage) {
        bolt.speed = info.speed;
        bolt.damage = (info.damage + damageBuff) + critDamage;
        bolt.lifeTime = info.lifeTime;
        bolt.shooterID = shooterID;
    }
}

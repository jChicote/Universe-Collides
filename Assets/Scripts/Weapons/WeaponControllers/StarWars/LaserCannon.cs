using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LaserCannon : BaseWeapon
{
    public bool isParallelFire = false;
    void Start()
    {
        WeaponSettings settings = GameManager.Instance.gameSettings.weaponSettings;
        info = settings.weaponStats.Where(x => x.type == WeaponType.LaserBolt).First();
    }

    public void Init(string id, bool enableParrallel, Transform[] cannons, VesselAudioSystem audioSystem) {
        this.shooterID = id;
        this.isParallelFire = enableParrallel;
        this.firePoint = cannons;
        this.audioSystem = audioSystem;
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
            Quaternion rotation = Quaternion.Euler(point.eulerAngles);
            LaserBolt bolt = Instantiate(info.prefab, point.position, rotation).GetComponent<LaserBolt>();
            bolt.speed = info.speed;
            bolt.damage = (info.damage + damageBuff) + critDamage;
            bolt.lifeTime = info.lifeTime;
            bolt.shooterID = shooterID;
        }

        StartCoroutine(Reload());
    }

    private void SequentialFire(float damageBuff, float critDamage) {
        Quaternion rotation = Quaternion.Euler(firePoint[transformIndex].eulerAngles);
        LaserBolt bolt = Instantiate(info.prefab, firePoint[transformIndex].position, rotation).GetComponent<LaserBolt>();
        bolt.speed = info.speed;
        bolt.damage = (info.damage + damageBuff) * critDamage;
        bolt.lifeTime = info.lifeTime;
        bolt.shooterID = shooterID;
        StartCoroutine(Reload());
        
        transformIndex++;
        if(transformIndex == firePoint.Length) transformIndex = 0;
    }
}

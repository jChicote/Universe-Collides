using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LaserCannon : BaseWeapon
{
    void Start()
    {
        WeaponSettings settings = GameManager.Instance.gameSettings.weaponSettings;
        info = settings.weaponStats.Where(x => x.type == WeaponType.LaserBolt).First();
    }

    public override void Init(StatHandler statHandler) {
        this.statHandler = statHandler;
    }

    public override void Shoot()
    {
        if(!isShooting) return;

        Quaternion rotation = Quaternion.Euler(firePoint[transformIndex].eulerAngles);
        LaserBolt bolt = Instantiate(info.prefab, firePoint[transformIndex].position, rotation).GetComponent<LaserBolt>();
        //bolt.info = info;
        bolt.speed = info.speed;
        bolt.damage = (info.damage + statHandler.DamageBuff) * statHandler.CriticalDamage;
        bolt.lifeTime = info.lifeTime;
        bolt.shooterID = shooterID;
        audioSystem.PlaySoundEffect(SoundType.LaserCannon);
        StartCoroutine(Reload());
        
        transformIndex++;
        if(transformIndex == firePoint.Length) transformIndex = 0;
    }
}

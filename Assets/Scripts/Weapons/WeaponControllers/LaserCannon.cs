using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LaserCannon : BaseWeapon
{
    int index = 0;

    // Start is called before the first frame update
    void Start()
    {
        WeaponSettings settings = GameManager.Instance.gameSettings.weaponSettings;
        stats = settings.weaponStats.Where(x => x.type == WeaponType.LaserCannon).First();
    }

    public override void Shoot()
    {
        if(!isShooting) return;

        Quaternion rotation = Quaternion.Euler(firePoint[index].eulerAngles);
        LaserBolt bolt = Instantiate(stats.prefab, firePoint[index].position, rotation).GetComponent<LaserBolt>();
        bolt.speed = stats.speed;
        bolt.damage = stats.damage;
        bolt.lifeTime = stats.lifeTime;
        audioSystem.PlaySoundEffect(SoundType.LaserCannon);
        StartCoroutine(Reload());
        
        index++;
        if(index == firePoint.Length) index = 0;
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PhotonLauncher : BaseWeapon
{
    void Start()
    {
        //WeaponSettings settings = GameManager.Instance.gameSettings.weaponSettings;
        //info = settings.weaponStats.Where(x => x.type == ProjectileType.Photon).First();
    }

    public override void Shoot(float damageBuff, float critDamage)
    {
        /*if(!isShooting) return;

        Quaternion rotation = Quaternion.Euler(firePoint[transformIndex].eulerAngles);
        Photon photon = Instantiate(info.prefab, firePoint[transformIndex].position, rotation).GetComponent<Photon>();
        
        StartCoroutine(Reload());

        transformIndex++;
        if(transformIndex == firePoint.Count) transformIndex = 0;*/
    }
}

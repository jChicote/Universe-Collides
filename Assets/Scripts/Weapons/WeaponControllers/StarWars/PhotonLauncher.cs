using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PhotonLauncher : BaseWeapon
{
    void Start()
    {
        WeaponSettings settings = GameManager.Instance.gameSettings.weaponSettings;
        info = settings.weaponStats.Where(x => x.type == WeaponType.Photon).First();
    }

    public override void Shoot()
    {
        if(!isShooting) return;

        Quaternion rotation = Quaternion.Euler(firePoint[transformIndex].eulerAngles);
        Photon photon = Instantiate(info.prefab, firePoint[transformIndex].position, rotation).GetComponent<Photon>();
        
        StartCoroutine(Reload());

        transformIndex++;
        if(transformIndex == firePoint.Length) transformIndex = 0;
    }
}

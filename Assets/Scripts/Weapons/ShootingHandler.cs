﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// This class handles firing patterns for each weapon system on a vessel
/// </summary>
public class ShootingHandler : MonoBehaviour
{
    private WeaponInfo info;

    private VesselShipStats shipStats;
    private List<IWeaponItem> weaponItems;
    private WeaponInfo weaponInfo;
    private StatHandler statHandler;

    private float fireRate;
    private int currentIndex = 0;
    private bool canShoot = true;

    public void Init(StatHandler statHandler, List<IWeaponItem> weaponItems, VesselShipStats stats, WeaponInfo weaponInfo)
    {
        this.statHandler = statHandler;
        this.weaponItems = weaponItems;
        this.shipStats = stats;
        this.weaponInfo = weaponInfo;
        this.fireRate = weaponInfo.weaponData.fireRate;
    }

    public void RunWeaponsFire()
    {
        if (!weaponInfo.isEnabled) return;

        if (weaponInfo.isParallel) ParallelFire();
        if (weaponInfo.isSequential) SequentialFire();
    }

    /// <summary>
    /// All weapon collections will fire in a parallel sequence
    /// </summary>
    private void ParallelFire()
    {
        if (!canShoot) return;

        float randomVal = 0;
        foreach (IWeaponItem weapon in weaponItems)
        {
            if (weaponInfo.isRandomised)
            {
                randomVal = Random.Range(0.0f, 1.0f);
                if (randomVal < 0.4f) return;
                weapon.Shoot(statHandler.DamageBuff, statHandler.CriticalDamage);
            }
            else
            {
                weapon.Shoot(statHandler.DamageBuff, statHandler.CriticalDamage);
            }
        }

        StartCoroutine(Reload());
    }

    /// <summary>
    /// All weapon collections will fire in a sequencial sequence
    /// </summary>
    private void SequentialFire()
    {
        if (!canShoot) return;

        float randomVal = Random.Range(0.0f, 1.0f);
        if (randomVal < 0.4f) return;

        weaponItems[currentIndex].Shoot(statHandler.DamageBuff, statHandler.CriticalDamage);
        StartCoroutine(Reload());

        currentIndex++;
        if (currentIndex == weaponItems.Count) currentIndex = 0;
    }

    public virtual IEnumerator Reload()
    {
        canShoot = false;
        float timeSinceFired = 0;

        while (timeSinceFired < fireRate)
        {
            timeSinceFired += Time.deltaTime;
            yield return null;
        }

        canShoot = true;
    }
}

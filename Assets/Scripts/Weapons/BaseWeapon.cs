using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseWeapon : MonoBehaviour
{
    public Transform[] firePoint;
    public WeaponStats stats;
    public bool isShooting = true;

    public abstract void Shoot();
    public virtual IEnumerator Reload() {
        isShooting = false;
        float timeSinceFired = 0;

        while (timeSinceFired < stats.fireRate){
            timeSinceFired += Time.deltaTime;
            yield return null;
        }

        isShooting = true;
    }
}

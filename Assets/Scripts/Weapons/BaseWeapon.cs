using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseWeapon : MonoBehaviour
{
    [HideInInspector] public Transform[] firePoint;
    [HideInInspector] public WeaponStats stats;
    [HideInInspector] public VesselAudioSystem audioSystem;
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

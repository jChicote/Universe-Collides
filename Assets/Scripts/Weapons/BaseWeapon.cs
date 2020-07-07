using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseWeapon : MonoBehaviour
{
    [HideInInspector] public Transform[] firePoint;
    [HideInInspector] public WeaponInfo info;
    [HideInInspector] public VesselAudioSystem audioSystem;
    
    public bool isShooting = true;
    public int transformIndex = 0;

    public string shooterID;


    public virtual void Shoot(float damageBuff, float criticalDamage, SoundType type) {}

    public virtual IEnumerator Reload() {
        isShooting = false;
        float timeSinceFired = 0;

        while (timeSinceFired < info.fireRate){
            timeSinceFired += Time.deltaTime;
            yield return null;
        }

        isShooting = true;
    }
}

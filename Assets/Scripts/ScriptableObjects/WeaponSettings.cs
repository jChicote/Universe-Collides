using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = ("Settings/Weapon Settings"))]
public class WeaponSettings : ScriptableObject
{
    [Header("Weapon Stats")]
    public WeaponInfo[] weaponStats; 

    [Header("Particle Effects")]
    public List<ImpactEffect> impactEffects;
}

[System.Serializable]
public class WeaponInfo {
    public WeaponType type;
    public GameObject prefab;
    public float lifeTime;
    public float fireRate;
    public float speed;
    public float damage;
}

[System.Serializable]
public class ImpactEffect {
    public ImpactType type;
    public GameObject[] effectsArray;

    [HideInInspector]
    public GameObject effect {
        get {
            int size = effectsArray.Length;
            if (size == 1) return effectsArray[0];

            int randomIndex = Random.Range(0, size);
            return effectsArray[randomIndex];
        }
    }
}

public enum ImpactType {
    SparkImpact
}

public enum WeaponType {
    LaserBolt,
    Photon,
    Blaster,
    Phaser,
    Torpedo,
    Missiles,
    Mines
}
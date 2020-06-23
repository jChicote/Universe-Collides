using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = ("Settings/Weapon Settings"))]
public class WeaponSettings : ScriptableObject
{
    [Header("Weapon Stats")]
    public WeaponStats[] weaponStats; 
}

[System.Serializable]
public class WeaponStats {
    public WeaponType type;
    public GameObject prefab;
    public float lifeTime;
    public float fireRate;
    public float speed;
    public float damage;
}

public enum WeaponType {
    LaserCannon,
    Blaster,
    Phaser,
    Torpedo,
    Missiles,
    Mines
}
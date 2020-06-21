using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = ("Settings/Weapon Settings"))]
public class WeaponSettings : ScriptableObject
{
    public WeaponStats[] weaponStats; 
}

[SerializeField]
public class WeaponStats {
    public WeaponType type;
    public float lifeTime;
    public float fireRate;
    public float velocity;
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
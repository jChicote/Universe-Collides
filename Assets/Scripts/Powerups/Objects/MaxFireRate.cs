using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxFireRate : PowerUp
{
    IWeaponSystem weaponSystem;

    public override void Init(StatHandler statHandler, PowerupSystem system, float lifeTime)
    {
        this.statHandler = statHandler;
        this.lifeTime = lifeTime;
        weaponSystem = system.GetComponent<IWeaponSystem>();
    }

    public override void BeginPowerUp() {
        Debug.Log("Powerup Beginning");
    }

    public override void EndPowerUp() {
        Debug.Log("Powerup Ending");
    }
}

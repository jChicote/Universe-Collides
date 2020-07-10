using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxFireDamage : PowerUp
{
    float defaultValue = 0;

    IWeaponSystem weaponSystem;

    public override void Init(StatHandler statHandler, PowerupSystem system, float lifeTime)
    {
        this.statHandler = statHandler;
        this.lifeTime = lifeTime;
        weaponSystem = system.GetComponent<IWeaponSystem>();
    }

    public override void BeginPowerUp() {
        Debug.Log("Powerup Beginning");
        defaultValue = statHandler.DamageBuff;
        statHandler.DamageBuff = defaultValue * 4;
    }

    public override void EndPowerUp() {
        Debug.Log("Powerup Ending");
        statHandler.FireRate = defaultValue;
    }
}

using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PowerUp
{
    public PowerUpType type;
    public float lifeTime;

    public StatHandler statHandler;

    public virtual void Init(StatHandler statHandler, PowerupSystem system, float lifeTime) {}
    
    public virtual void BeginPowerUp(){}

    public virtual void RunPowerUp() {}

    public virtual void EndPowerUp() {}
}

[System.Serializable]
public class PowerUpInfo {
    public PowerUpType type;
    public Type classType {
        get { return Type.GetType(type.ToString()); }
    }

    public float lifeTime;
    public Sprite icon;

}

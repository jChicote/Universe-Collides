using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RienforceHull : PowerUp
{
    float defaultValue = 0;

    public override void Init(StatHandler statHandler, PowerupSystem system, float lifeTime)
    {
        this.statHandler = statHandler;
        this.lifeTime = lifeTime;
    }

    public override void BeginPowerUp()
    {
        defaultValue = statHandler.HullResistence;
        statHandler.HullResistence = defaultValue * 4;
    }

    public override void EndPowerUp()
    {
        statHandler.FireRate = defaultValue;
    }
}

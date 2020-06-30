using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyMoveState : BaseState
{
    EnemyController enemyController;
    DamageManager damageSystem;

    public override void BeginState()
    {
        enemyController = this.GetComponent<EnemyController>();
        shipStats = GameManager.Instance.gameSettings.vesselStats.Where(x => x.type.Equals(enemyController.vesselSelection)).First();

        damageSystem = this.gameObject.AddComponent<FighterDamageManager>();
        damageSystem.Init(this, null);
        damageSystem.components = shipStats.components;
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyPursuit : EnemyState
{
    Vector3 pursuitDir;
    Quaternion targetRot;
    bool isAvoiding = false;

    public override void BeginState()
    {
        controller = this.GetComponent<EnemyController>();
        shipStats = GameManager.Instance.gameSettings.vesselStats.Where(x => x.type.Equals(controller.vesselSelection)).First();

        damageSystem = this.gameObject.GetComponent<FighterDamageManager>();
        damageSystem.Init(this, null);
        damageSystem.components = shipStats.components;
    }

    public override void RunState() {
        if(isPaused) return;
        if(GameManager.Instance.playerController == null) return;
        
        targetDistance = Vector3.Distance(transform.position, GameManager.Instance.playerController.transform.position);
        if(targetDistance > shipStats.maxProximityDist) controller.SetState<EnemyWander>();
        if(targetDistance < 10) controller.SetState<EnemyEvasion>();

        Movement();
        if(controller.avoidanceSystem.DetectCollision()) return;

        LookPursuit();
        ApplyRoll();
    }

    private void Movement() {
        currentVelocity = shipStats.speed * transform.forward * Time.fixedDeltaTime;
        transform.position += currentVelocity;
    }

    private void LookPursuit() {
        target = GameManager.Instance.playerController.gameObject;
        pursuitDir = target.transform.position - transform.position;
        targetRot = Quaternion.LookRotation(pursuitDir);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime);

        roll = shipStats.rollRate * Time.deltaTime * pursuitDir.normalized.x;
    }
}
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyEvasion : EnemyState
{
    Vector3 evasionDir;
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
        targetDistance = Vector3.Distance(transform.position, GameManager.Instance.playerController.transform.position);
        if(targetDistance > shipStats.pursuitDistance) controller.SetState<EnemyPursuit>();

        Movement();
        if(controller.avoidanceSystem.DetectCollision()) return;
    
        Evasion();
        ApplyRoll();
    }

    private void Movement() {
        currentVelocity = (shipStats.speed * 2) * transform.forward * Time.fixedDeltaTime;
        transform.position += currentVelocity;
    }

    private void Evasion() {
        target = GameManager.Instance.playerController.gameObject;
        evasionDir= target.transform.position - transform.position;
        targetRot = Quaternion.LookRotation(-evasionDir);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime);

        roll = shipStats.rollRate * Time.deltaTime * evasionDir.normalized.x;
    }
}

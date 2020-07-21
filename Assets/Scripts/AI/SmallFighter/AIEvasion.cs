using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AIEvasion : AIState
{
    Vector3 evasionDir;
    Quaternion targetRot;

    public override void BeginState()
    {
        controller = this.GetComponent<AIController>();
        objectID = controller.objectID;
        shipStats = GameManager.Instance.gameSettings.vesselStats.Where(x => x.type.Equals(controller.vesselSelection)).First();

        damageSystem = controller.damageSystem;
    }

    void FixedUpdate()
    {
        RunState();
    }

    public override void RunState() {
        if(isPaused) return;
        if(controller.statHandler.CurrentHealth <= 0) AIDeath(); //Check if player is dead

        ChangeState();
        DoMovement();
        if(controller.avoidanceSystem.DetectCollision()) return;
        
        DoMovement();
        Evasion();
        SetShipRoll();
    }

    private void ChangeState() {
        targetDistance = Vector3.Distance(transform.position, GameManager.Instance.playerController.transform.position);
        if(targetDistance > shipStats.maxProximityDist) controller.SetState<AIWander>();
        if(targetDistance > shipStats.pursuitDistance) controller.SetState<AIPursuit>();
    }

    private void DoMovement() {
        currentVelocity = (shipStats.speed * 2) * transform.forward * Time.fixedDeltaTime;
        transform.position += currentVelocity;
    }

    private void Evasion() {
        target = GameManager.Instance.playerController.gameObject; //CHANGE TO DYNAMIC TARGETING
        evasionDir= target.transform.position - transform.position;
        targetRot = Quaternion.LookRotation(-evasionDir);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime);

        roll = shipStats.rollRate * Time.deltaTime * evasionDir.normalized.x;
    }
}

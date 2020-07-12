using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AIPursuit : AIState
{
    TargetDirectionCheck dirChecker;
    Vector3 pursuitDir;
    Quaternion targetRot;

    public override void BeginState()
    {
        controller = this.GetComponent<AIController>();
        objectID = controller.objectID;
        shipStats = GameManager.Instance.gameSettings.vesselStats.Where(x => x.type.Equals(controller.vesselSelection)).First();
        dirChecker = new TargetDirectionCheck();

        weaponSystem = this.GetComponent<IWeaponSystem>();

        damageSystem = controller.damageSystem;

        //damageSystem = new FighterDamageManager();
        //damageSystem.Init(weaponSystem, controller.statHandler);
    }

    void FixedUpdate()
    {
        RunState();
    }

    public override void RunState() {
        if(isPaused) return;
        if(GameManager.Instance.playerController == null) return;

        //Check if player is dead
        if(controller.statHandler.CurrentHealth <= 0) AIDeath();

        /// Testing purposes only ///

        if(GameManager.Instance.playerController.gameObject != null) {
            weaponSystem.AssignTarget(GameManager.Instance.playerController.gameObject);//MUST REMOVE
        }

        /// --------------------- ///
        
        if(weaponSystem.CheckAimDirection()) weaponSystem.RunSystem();

        ChangeState();
        Movement();
        if(controller.avoidanceSystem.DetectCollision()) return;

        LookPursuit();
        ApplyRoll();
    }

    private void ChangeState() {
        targetDistance = Vector3.Distance(transform.position, GameManager.Instance.playerController.transform.position);
        if(targetDistance > shipStats.maxProximityDist) controller.SetState<AIWander>();
        if(targetDistance < 10) controller.SetState<AIEvasion>();
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
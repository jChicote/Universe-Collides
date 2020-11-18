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

        //damageSystem = controller.damageSystem;
    }

    void FixedUpdate()
    {
        RunState();
    }

    public override void RunState() {
        if(isPaused) return;

        //Change to be dynamic AND TO THE TARGET VARIABLE
        if(GameManager.Instance.sceneController.playerController == null) {
            controller.SetState<AIWander>();
            return;
        }

        //Check if ai is dead
        if(controller.statHandler.CurrentHealth <= 0) AIDeath();

        /// Testing purposes only ///

        if(GameManager.Instance.sceneController.playerController.gameObject != null) {
            //weaponSystem.SetNewTarget(GameManager.Instance.playerController.gameObject);//MUST REMOVE
        }

        /// --------------------- ///
        
        //if(weaponSystem.CheckAimDirection()) weaponSystem.RunSystem();

        ChangeState();
        DoMovement();
        if(controller.avoidanceSystem.DetectCollision()) return;

        Pursuit();
        SetShipRoll();
    }

    private void ChangeState() {
        targetDistance = Vector3.Distance(transform.position, GameManager.Instance.sceneController.playerController.transform.position);
        if(targetDistance > shipStats.maxProximityDist) controller.SetState<AIWander>();
        if(targetDistance < 10) controller.SetState<AIEvasion>();
    }

    private void DoMovement() {
        currentVelocity = shipStats.speed * transform.forward * Time.fixedDeltaTime;
        transform.position += currentVelocity;
    }

    private void Pursuit() {
        target = GameManager.Instance.sceneController.playerController.gameObject; //CHANGE TO DYNAMIC TARGETING
        pursuitDir = target.transform.position - transform.position;
        targetRot = Quaternion.LookRotation(pursuitDir);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime);

        roll = shipStats.rollRate * Time.deltaTime * pursuitDir.normalized.x;
    }
}
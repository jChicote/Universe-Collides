using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AIEvasion : AIState
{
    private AIMovementController movementController;

    private Vector3 evasionDir;
    private Quaternion targetRot;

    public override void BeginState()
    {
        controller = this.GetComponent<AIController>();
        objectID = controller.objectID;
        shipStats = GameManager.Instance.gameSettings.vesselStats.Where(x => x.type.Equals(controller.vesselSelection)).First();

        movementController = this.GetComponent<AIMovementController>();
    }

    void FixedUpdate()
    {
        RunState();
    }

    public override void RunState() {
        if(isPaused) return;
        if(controller.statHandler.CurrentHealth <= 0) AIDeath(); //Check if player is dead

        ChangeState();
        if(controller.avoidanceSystem.DetectCollision()) return;

        movementController.PerformMovement();
        EvasionRotation();
        SetShipRoll();
    }

    /// <summary>
    /// Changes state when conditions are met
    /// </summary>
    private void ChangeState() {
        targetDistance = Vector3.Distance(transform.position, GameManager.Instance.sceneController.playerController.transform.position);
        if(targetDistance > shipStats.maxProximityDist) controller.SetState<AIWander>();
        if(targetDistance > shipStats.pursuitDistance) controller.SetState<AIPursuit>();
    }

    /// <summary>
    /// Apples evasion rotation during flight.
    /// </summary>
    private void EvasionRotation() {
        target = GameManager.Instance.sceneController.playerController.gameObject; //CHANGE TO DYNAMIC TARGETING
        evasionDir= target.transform.position - transform.position;
        targetRot = Quaternion.LookRotation(-evasionDir);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime);

        roll = shipStats.rollRate * Time.deltaTime * evasionDir.normalized.x;
    }
}

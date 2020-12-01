using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AIPursuit : AIState
{
    private TargetDirectionCheck dirChecker;
    private AIMovementController movementController;

    private IWeaponAim weaponAim;
    private IAssignTarget targetAssigner;

    private Vector3 pursuitDir;
    private Quaternion targetRot;

    public override void BeginState()
    {
        controller = this.GetComponent<AIController>();
        objectID = controller.objectID;
        shipStats = GameManager.Instance.gameSettings.vesselStats.Where(x => x.type.Equals(controller.vesselSelection)).First();
        dirChecker = new TargetDirectionCheck();

        weaponAim = this.GetComponent<IWeaponAim>();
        weaponSystem = this.GetComponent<IWeaponSystem>();
        targetAssigner = this.GetComponent<IAssignTarget>();
        movementController = this.GetComponent<AIMovementController>();
    }

    private void FixedUpdate()
    {
        RunState();
    }

    public override void RunState() {
        if(isPaused) return;

        if (CheckTargetExistene()) return;

        //Check if ai is dead
        if (controller.statHandler.CurrentHealth <= 0) AIDeath();

        /// Testing purposes only ///

        if(GameManager.Instance.sceneController.playerController.gameObject != null) {
            //weaponSystem.SetNewTarget(GameManager.Instance.playerController.gameObject);//MUST REMOVE
        }

        if (weaponAim.CheckAimDirection())
        {
            weaponSystem.RunSystem();
        }

        ChangeState();
        
        if (controller.avoidanceSystem.DetectCollision()) return;

        //Perform Ship actions
        movementController.PerformMovement();
        PursuitRotation();
        SetShipRoll();
    }

    /// <summary>
    /// Checks whether the target exists in the scene or the initial state will set to wander.
    /// </summary>
    private bool CheckTargetExistene()
    {
        targetAssigner.AssignTarget(GameManager.Instance.sceneController.playerController.gameObject); //TODO: convert this to a more dynamic target assigner
        if (GameManager.Instance.sceneController.playerController == null)
        {
            controller.SetState<AIWander>();
            return true;
        }

        return false;
    }

    /// <summary>
    /// Respoonsible for switching state depending on the circumstances encountered.
    /// </summary>
    private void ChangeState() {
        targetDistance = Vector3.Distance(transform.position, GameManager.Instance.sceneController.playerController.transform.position);
        //if(targetDistance > shipStats.maxProximityDist) controller.SetState<AIWander>();
        if(targetDistance < 10) controller.SetState<AIEvasion>();
    }

    /// <summary>
    /// 
    /// </summary>
    private void PursuitRotation() {
        target = GameManager.Instance.sceneController.playerController.gameObject; //CHANGE TO DYNAMIC TARGETING
        pursuitDir = target.transform.position - transform.position;
        targetRot = Quaternion.LookRotation(pursuitDir);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime);

        roll = shipStats.rollRate * Time.deltaTime * pursuitDir.normalized.x;
    }
}
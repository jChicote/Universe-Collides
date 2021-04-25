using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AIPursuit : AIState
{
    private TargetDirectionCheck dirChecker;
    private AIMovementController movementController;

    // pursuit interfaces for weapons and targeting
    private IWeaponAim weaponAim;
    private ITargetFinder targetFinder;
    private ISetWeaponTarget weaponTargetSetter;

    private Vector3 pursuitDir;
    private Quaternion targetRot;

    public override void BeginState()
    {
        Debug.Log("Begun Pursuit State");
        controller = this.GetComponent<AIController>();
        objectID = controller.objectID;
        shipStats = GameManager.Instance.gameSettings.vesselStats.Where(x => x.type.Equals(controller.vesselSelection)).First();
        dirChecker = new TargetDirectionCheck();

        //  Collect components for class
        weaponAim = this.GetComponent<IWeaponAim>();
        weaponSystem = this.GetComponent<IWeaponSystem>();
        targetFinder = this.GetComponent<ITargetFinder>();
        weaponTargetSetter = this.GetComponent<ISetWeaponTarget>();
        movementController = this.GetComponent<AIMovementController>();

        targetFinder.FindTarget();
        weaponTargetSetter.SetTarget();
    }

    private void FixedUpdate()
    {
        RunState();
    }

    public override void RunState() {
        if(isPaused) return;
        //  Check if ai is dead
        if (controller.statHandler.CurrentHealth <= 0) AIDeath();

        //  Perform ship movement
        movementController.PerformMovement();

        //  Check for forward obstacles
        if (controller.avoidanceSystem.DetectCollision()) return;

        //  Check whether target still exists
        if (!CheckTargetExistene()) return;

        //  Change ai state
        ChangeState();

        //  Pursue target when valid
        PursuitTarget();
    }

    /// <summary>
    /// Pursues valid identified target
    /// </summary>
    private void PursuitTarget()
    {
        //  Run weapon system
        if (weaponAim.CheckAimDirection()) weaponSystem.RunSystem();

        //  Perform Ship Rotation
        PursuitRotation();
        SetShipRoll();
    }

    /// <summary>
    /// Checks whether the target exists in the scene or the initial state will set to wander.
    /// </summary>
    private bool CheckTargetExistene()
    {
       // targetAssigner.AssignTarget(GameManager.Instance.sceneController.playerController.gameObject); //TODO: convert this to a more dynamic target assigner
        if (targetFinder.GetTarget() == null)
        {
            controller.SetState<AIWander>();
            return false;
        }

        return true;
    }

    /// <summary>
    /// Respoonsible for switching state depending on the circumstances encountered.
    /// </summary>
    private void ChangeState() {
        targetDistance = Vector3.Distance(transform.position, targetFinder.GetTarget().transform.position);
        if(targetDistance > shipStats.maxProximityDist) controller.SetState<AIWander>();
        if(targetDistance < 10) controller.SetState<AIEvasion>();
    }

    /// <summary>
    /// Calculates the rotation during pursuit
    /// </summary>
    private void PursuitRotation() {
        target = GameManager.Instance.sceneController.playerController.gameObject; //CHANGE TO DYNAMIC TARGETING
        pursuitDir = target.transform.position - transform.position;
        targetRot = Quaternion.LookRotation(pursuitDir);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime);

        roll = shipStats.rollRate * Time.deltaTime * pursuitDir.normalized.x;
    }
}
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AIWander : AIState
{
    private AIMovementController movementController;

    private float wanderAngle;
    private Vector3 forwardVel;
    private Vector3 displacement;
    private Quaternion targetRot;

    bool isReturning = false;

    public override void BeginState()
    {
        controller = this.GetComponent<AIController>();
        objectID = controller.objectID;
        shipStats = GameManager.Instance.gameSettings.vesselStats.Where(x => x.type.Equals(controller.vesselSelection)).First();

        movementController = this.GetComponent<AIMovementController>();
        InvokeRepeating("SetNewAngle", 5f, 5f);
    }

    void FixedUpdate()
    {
        RunState();
    }

    public override void RunState() {
        if(isPaused) return;
        if(controller.statHandler.CurrentHealth <= 0) AIDeath(); //Check if player is dead

        movementController.PerformMovement();
        if(controller.avoidanceSystem.DetectCollision()) return;
        
        CheckOutOfBounds();

        if(isReturning) {
            Wander();
        }
    }

    /// <summary>
    /// Applies wandering behaviur during travel.
    /// </summary>
    private void Wander() {
        forwardVel = transform.forward * shipStats.speed * Time.deltaTime;
        displacement = new Vector3(0, -1);
        displacement.Scale(forwardVel);

        targetRot = Quaternion.Euler(displacement.y + wanderAngle, displacement.x + wanderAngle, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime);
        roll = shipStats.rollRate * Time.deltaTime * (displacement.y + wanderAngle);
    }

    private void SetNewAngle() {
        wanderAngle += Random.Range(0, 360) - 360 * .5f;
    }

    /// <summary>
    /// Checks whether the vessel is heading out of bounds.
    /// </summary>
    private void CheckOutOfBounds()
    {
        float targetDistance = Vector3.Distance(transform.position, GameManager.Instance.transform.position);
        if(targetDistance < shipStats.maxProximityDist) return;
        if(!isReturning) StartCoroutine(RotateToCenter());

        return;
    }

    /// <summary>
    /// Rotates the vessel to the center of the game scene.
    /// </summary>
    private IEnumerator RotateToCenter() {
        isReturning = true;
        Vector3 dir;
        Quaternion currentRot;
        float timer = 4f;

        while(timer > 0) {
            dir = GameManager.Instance.transform.position - transform.position;
            currentRot = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Slerp(transform.rotation, currentRot, Time.deltaTime);

            roll = shipStats.rollRate * Time.deltaTime * dir.normalized.x;
            yield return null;
        }

        isReturning = false;
    }
}

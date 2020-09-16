using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimAssist
{
    private BaseWeaponSystem weaponSystem;
    private Transform entityTransform;
    private AimUI aimUI;
    private Camera cam = GameManager.Instance.sceneCamera;

    private Transform intendedTarget;

    public void Init(BaseWeaponSystem weaponSystem, AimUI aimUI){
        this.weaponSystem = weaponSystem;
        this.entityTransform = weaponSystem.gameObject.transform;
        this.aimUI = aimUI;
        this.cam = GameManager.Instance.sceneCamera;
    }

    /// <summary>
    /// Gets position of raycast target in its forward direction
    /// </summary>
    public bool GetRaycastPosition() {
        Vector3 futurePosition = entityTransform.position + entityTransform.forward * weaponSystem.shipStats.speed * 3f;
        Vector3 screenPosition = cam.WorldToScreenPoint(futurePosition);
        Ray camRay = cam.ScreenPointToRay(screenPosition);

        RaycastHit hit;

        if(Physics.Raycast(camRay, out hit, 100f)) {
            if(hit.collider.gameObject.GetComponent<IEntity>() == null) return false;
            intendedTarget = hit.collider.transform;
            return true;
        }

        return false;;
    }

    /// <summary>
    /// Checks that the intended target is within the aim range and sets target object
    /// </summary>
    public void SetTargetInAimRange() {
        if(intendedTarget == null) {
            GetRaycastPosition();
            return;
        }

        Vector3 targetDir = (intendedTarget.position - cam.transform.position).normalized;

        if(Vector3.Dot(entityTransform.forward.normalized, targetDir) < 0.98f) {
            intendedTarget = null;
        }
    }

    /// <summary>
    /// Calculates the aim direction of the target and returns its relative rotation.
    /// </summary>
    public Quaternion CalculateAimDirection(Transform firePoint) {
        if(intendedTarget == null) return Quaternion.Euler(firePoint.eulerAngles);

        //Vector3 targetVelocity = intendedTarget.GetComponent<IMovementControl>().GetVelocity();
        //Debug.Log(targetVelocity);

        float distance = Vector3.Distance(intendedTarget.position, firePoint.position);
        Vector3 relativePosition = (firePoint.position + entityTransform.forward * distance);
        //Vector3 relativeTargetPoint = intendedTarget.position + targetVelocity * 3;

        Vector3 targetDir = relativePosition - firePoint.position;
        //Vector3 targetDir = relativeTargetPoint - firePoint.position;
        Quaternion rotation = Quaternion.LookRotation(targetDir, Vector3.up);
        return rotation;
    }
}

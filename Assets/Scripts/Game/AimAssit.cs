using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimAssist
{
    BaseWeaponSystem weaponSystem;
    Transform entityTransform;
    AimUI aimUI;
    Camera cam = GameManager.Instance.sceneCamera;

    Transform intendedTarget;
    Quaternion rotation;
    Vector3 targetDir;

    public void Init(BaseWeaponSystem weaponSystem, AimUI aimUI){
        this.weaponSystem = weaponSystem;
        this.entityTransform = weaponSystem.gameObject.transform;
        this.aimUI = aimUI;
        this.cam = GameManager.Instance.sceneCamera;
    }

    public bool GetRaycastPosition() {
        Vector3 futurePosition = entityTransform.position + entityTransform.forward * weaponSystem.shipStats.speed * 3f;
        Vector3 screenPosition = cam.WorldToScreenPoint(futurePosition);
        Ray camRay = cam.ScreenPointToRay(screenPosition);

        RaycastHit hit;

        if(Physics.Raycast(camRay, out hit, 100f)) {
            if(hit.collider.gameObject.GetComponent<IEntity>() == null) return false;
            //Debug.Log("is raycasting to target");
            intendedTarget = hit.collider.transform;
            return true;
        }

        return false;;
    }

    public void CheckWithinAimRange() {
        if(intendedTarget == null) {
            GetRaycastPosition();
            return;
        }

        targetDir = (intendedTarget.position - cam.transform.position).normalized;

        if(Vector3.Dot(entityTransform.forward.normalized, targetDir) < 0.98f) {
            intendedTarget = null;
        }
    }

    public Quaternion CalculateAimDirection(Transform firePoint) {
        if(intendedTarget == null) return Quaternion.Euler(firePoint.eulerAngles);
        targetDir = intendedTarget.position - firePoint.position;
        rotation = Quaternion.LookRotation(targetDir, Vector3.up);
        return rotation;
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : BaseState
{
    public EnemyController controller;
    public GameObject target;

    public float targetDistance;

    float modelAngle;
    float angleResult;
    float targetAngle;

    Vector3 modelLocalAngles;

    public override void BeginState() {}

    public void ApplyRoll() {
        if(CheckForward()) {
            controller.modelTransform.rotation = Quaternion.Slerp(controller.modelTransform.rotation, transform.rotation, 0.1f);
            return;
        }

        //Finds roll angle of local model
        modelLocalAngles =  controller.modelTransform.localEulerAngles;
        modelAngle = (modelLocalAngles.z > 180) ? modelLocalAngles.z - 360 : modelLocalAngles.z;

        //Applied roll rotation of model
        angleResult = modelAngle + (-1 * roll);
        if (angleResult > -30 && angleResult < 30 )
            controller.modelTransform.Rotate(0, 0, -1 * roll);
        else {
            controller.modelTransform.localEulerAngles = new Vector3(0, 0, modelAngle);
        }
    }

    private bool CheckForward() {
        target = GameManager.Instance.playerController.gameObject;
        targetAngle = Vector3.Angle(target.transform.forward, transform.position - target.transform.position);

        if(targetAngle < shipStats.stableAngleLimit) return true;
        return false;
    }
}

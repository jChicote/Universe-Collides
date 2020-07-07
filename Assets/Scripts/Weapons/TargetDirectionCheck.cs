using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDirectionCheck {
    private float distance = 0;

    private Vector3 forward;
    private Vector3 targetDir;

    public bool CheckInDirection(Vector3 targetPosition, Vector3 currentPosition, float maxRange) {
        float distance = Vector3.Distance(targetPosition, currentPosition);
        if(distance < maxRange) {
            return true;
        }
        return false;
    }

    public bool CheckInView(Vector3 targetPosition, Vector3 currentPosition, Vector3 currentForward, float dotLimit) {
        forward = currentForward.normalized;
        targetDir = (targetPosition - currentPosition).normalized;

        if(Vector3.Dot(forward, targetDir) > dotLimit) {
            return true;
        } else {
            return false;
        }
    }
}

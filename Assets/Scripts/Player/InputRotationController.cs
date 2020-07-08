using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputRotationController {

    public float CalculateInputToCenter(float inputVal, float screenAxis, float sensitivity) {
        return (inputVal - (screenAxis / 2)) / (screenAxis / 2) * sensitivity;
    }

    public float CalculateAxis(float axisStrength, float inputVal) {
        return axisStrength * inputVal * Time.fixedDeltaTime;
    }

    public float CalculateObjectRoll(float eulerAngle) {
        return (eulerAngle > 180) ? eulerAngle - 360 : eulerAngle;
    }
}

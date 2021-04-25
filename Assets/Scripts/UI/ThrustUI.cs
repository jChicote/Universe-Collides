using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThrustUI : MonoBehaviour
{
    public Image thrustImage;

    void Start() {
        thrustImage.fillAmount = 0.5f;
    }

    public void SetThrustLevel(float inputThrust) {
        if(inputThrust == 0) {
            thrustImage.fillAmount = Mathf.Lerp(thrustImage.fillAmount, 0.5f, 0.05f);
        } else if(inputThrust > 0) {
            thrustImage.fillAmount = Mathf.Lerp(thrustImage.fillAmount, 1, 0.05f);
        } else {
            thrustImage.fillAmount = Mathf.Lerp(thrustImage.fillAmount, 0, 0.05f);
        }
    }
}

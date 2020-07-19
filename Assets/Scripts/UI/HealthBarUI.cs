using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    public Slider healthSlider;

    public void Init(float maxHealth) {
        healthSlider.maxValue = maxHealth;
        healthSlider.minValue = 0;
        healthSlider.value = maxHealth;
    }

    public void SetHealth(float amount) {
        healthSlider.value = amount;
    }
}

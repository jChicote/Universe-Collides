using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsOptions : MonoBehaviour
{
    [Header("Sound Options")]
    [Range(0, 1)]
    public float musicVolume;
    [Range(0, 1)]
    public float effectsVolume;

    [Header("UI Options")]
    public bool bShowIndicators;
    public bool bOnlyMinimalGUIp;
    [Range(0.5f, 2f)]
    public float uiDisplaySize;

    [Header("Control Options")]
    public bool bAimAssist;
    [Range(0.1f, 3f)]
    public float controlSensitivity;

}


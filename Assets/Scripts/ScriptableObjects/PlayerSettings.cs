using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName =("Settings/Player Settings"))]
public class PlayerSettings : ScriptableObject
{
    public float sensitivity = 2;
    public List<CameraAttributes> cameraAttributes;
}

[System.Serializable]
public class CameraAttributes {
    public VesselType type;

    [Header("Camera Dampening")]
    public float defaultXDamp = 0;
    public float defaultYDamp = 0;
    public float defaultZDamp = 0;
    public float focusedXDamp = 0;
    public float focusedYDamp = 0;
    public float focusedZDamp = 0;

    [Space]
    public float defaultPitchDamp = 0;
    public float defaultYawDamp = 0;
    public float defaultRollDamp = 0;
    public float focusedPitchDamp = 0;
    public float focusedYawDamp = 0;
    public float focusedRollDamp = 0;

    [Header("Composer Offsets")]
    public float composerDefaultY = 0;
    public float composerModifiedY = 0;

    [Header("Transposer Offsets")]
    public float defaultZOffset = 0;
    public float defaultYOffset = 0;
    public float modifiedZOffset = 0;
    public float modifiedYOffset = 0;

    [Header("Camera FOV")]
    public float minFOV = 0;
    public float maxFOV = 0;
    public float defaultFOV = 0;
}

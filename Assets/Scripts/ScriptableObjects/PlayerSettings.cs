using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName =("Settings/Player Settings"))]
public class PlayerSettings : ScriptableObject
{
    public float mouseSensitivity = 2;
}

[System.Serializable]
public class CameraAttributes {
    public float composerYOffset = 0;
    public float xDamp = 0;
    public float yDamp = 0;
    public float zDamp = 0;
    public float pitchDamp = 0;
    public float yawDamp = 0;
    public float rollDamp = 0;
    public float transposerZOffset = 0;
    public float transposerYOffset = 0;
}

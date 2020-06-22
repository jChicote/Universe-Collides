using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName =("Settings/Game Settings"))]
public class GameSettings : ScriptableObject
{
    [Header ("Settings")]
    public PlayerSettings playerSettings;

    [Header ("Prefabs")]
    public GameObject UIHudPrefab;
    public GameObject playerPrefab; //later change to loaded model types that add the controller;

    [Header ("Collections")]
    public VesselShipStats[] vesselStats;
}

public enum VesselType {
    xWing
}

[System.Serializable]
public class VesselShipStats {
    public VesselType type;
    public float yawRate;
    public float pitchRate;
    public float rollRate;
    public float speed;
}

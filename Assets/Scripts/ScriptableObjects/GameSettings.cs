using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName =("Settings/Game Settings"))]
public class GameSettings : ScriptableObject
{
    [Header ("Settings")]
    public PlayerSettings playerSettings;
    public WeaponSettings weaponSettings;
    public AudioSettings audioSettings;
    public PowerupSettings powerupSettings;

    [Header ("Prefabs")]
    public GameObject sceneCamera;
    public GameObject audioManagerPrefab;
    public GameObject UIHudPrefab;
    public GameObject UIaimSightPrefab;
    public GameObject mainMenuPrefab;
    public GameObject playerPrefab; //later change to loaded model types that add the controller;

    [Header ("Collections")]
    public VesselShipStats[] vesselStats;
}

public enum VesselType {
    xWing,
    TieFighter,
    Test
}

[System.Serializable]
public class VesselShipStats {
    public VesselType type;

    [Header("Sensor Stats")]
    public float maxProximityDist;
    public float evasionDist;
    public float pursuitDistance {
        get {
            float dist = maxProximityDist * 0.8f;
            return dist;
        }
    }
    public float stableAngleLimit;

    [Header("Vessel Movement Stats")]
    public float yawRate;
    public float extraYaw;
    public float pitchRate;
    public float extraPitch;
    public float rollRate;
    public float extraRoll;
    public float speed;
    public float throttleSpeed;

    [Header("Vessel Handler Stats")]
    public BaseStats baseShipStats;

}

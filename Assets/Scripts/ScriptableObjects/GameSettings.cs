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

    [Header ("Base Canvas")]
    public GameObject animatingCanvas;

    [Header ("Prefabs")]
    public GameObject sceneCamera;
    public GameObject audioManagerPrefab;
    public GameObject scoreManagerPrefab;
    public GameObject postProcessingPrefab;
    public GameObject UIHudPrefab;

    //UI
    public GameObject UIPointerManagerPrefab;
    public GameObject UiPointerControllerPrefab;
    public GameObject arrowUIPrefab;
    public GameObject pointerUIPrefab;
    public GameObject predictiveUIPrefab;
    public GameObject UIaimSightPrefab;
    public GameObject mainMenuPrefab;
    public GameObject scoreUIPrefab;
    public GameObject healthBarUIPrefab;
    public GameObject thrustUIPrefab;
    public GameObject playerPrefab; //later change to loaded model types that add the controller;

    [Header("Colours")]
    public Color targetedColor = new Color();
    public Color idleColor = new Color();

    [Header ("Vessel Ship Stats")]
    public VesselShipStats[] vesselStats;

    [Header ("All Vessel Objects")]
    public VesselObjects[] vesselObjects;

    [Header ("Battle Spawner Modes")]
    public List<SpawnerModes> battleSpawnModes;
}

[System.Serializable]
public struct VesselObjects {
    public EntityType entityType;
    public VesselType vesselType;
    public GameObject objectPrefab;
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

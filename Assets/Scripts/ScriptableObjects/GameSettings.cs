using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName =("Settings/Game Settings"))]
public class GameSettings : ScriptableObject
{
    [Header ("Prefabs")]
    public GameObject UIHudPrefab;
    public GameObject playerPrefab; //later change to loaded model types that add the controller;
}

public enum VesselType {
    xWing
}

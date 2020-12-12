using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This contains all general effects only
/// </summary>
[CreateAssetMenu(menuName =("Settings/Effects Settings"))]
public class EffectsSettings : ScriptableObject
{
    [Header("Explosion Effects")]
    public GameObject[] shipExplosions;
}

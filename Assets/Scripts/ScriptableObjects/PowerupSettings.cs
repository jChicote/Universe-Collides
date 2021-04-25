using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Settings/PowerUp Setting")]
public class PowerupSettings : ScriptableObject
{
    public List<GameObject> powerUps;
    public List<PowerUpInfo> powerUpInfo;
}

public enum PowerUpType {
    MaxFireDamage,
    OverHealth
}
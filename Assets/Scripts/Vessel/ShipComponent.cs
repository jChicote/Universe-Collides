using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ShipComponent
{
    public ShipComponentType type;
    public int hits;
    public int maxHits;
    public bool isDamaged = false;

    public ShipComponent(ShipComponentType type, int maxHits) {
        this.type = type;
        this.maxHits = maxHits;
    }
}

public enum ShipComponentType {
    Engine,
    PrimaryWeapon,
    SecondaryWeapon
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentIdentifier : MonoBehaviour
{
    public ShipComponentType type;

    public ShipComponentType GetComponentType() {
        return type;
    }
}

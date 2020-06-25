using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseWeaponSystem : MonoBehaviour
{
    public GameManager gameManager;
    public VesselType vesselType;

    public virtual void Init() {
        gameManager = GameManager.Instance;
    }

    public virtual void RunSystem() {}

    public virtual void Shoot() {}


}

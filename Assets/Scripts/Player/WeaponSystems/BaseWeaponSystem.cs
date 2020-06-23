using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseWeaponSystem : MonoBehaviour
{
    public GameManager gameManager;
    public VesselType vesselType;

    public virtual void Init() {
        gameManager = GameManager.instance;
    }

    public virtual void RunSystem() {}

    public virtual void Shoot() {}


}

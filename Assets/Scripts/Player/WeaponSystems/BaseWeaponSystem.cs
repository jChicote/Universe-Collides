using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseWeaponSystem : MonoBehaviour
{
    public string objectID;
    public GameManager gameManager;
    public VesselType vesselType;

    public bool canShootPrimary = false;
    public bool canShootSecondary = false;

    public virtual void Init() {}

    public virtual void RunSystem() {}
    
    public virtual void SetAimPosition(float speed) {}

    public virtual void Shoot() {}


}

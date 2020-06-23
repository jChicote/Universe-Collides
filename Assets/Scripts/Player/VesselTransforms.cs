using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VesselTransforms : MonoBehaviour
{
    [Header("Weapon Transforms")]
    public Transform[] forwardGuns;
    public Transform[] forwardLaunchers;
    public Transform[] sternLaunchers;

    [Space]
    public Transform[] phaserBanks;
    public Transform[] turrentGuns;
    
    [Header("Exhaust")]
    public Transform[] engineExhausts;

}

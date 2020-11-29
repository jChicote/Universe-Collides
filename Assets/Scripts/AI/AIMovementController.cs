using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMovementController : MonoBehaviour
{
    [HideInInspector] public Vector3 currentVelocity = Vector3.zero;
    
    private VesselShipStats shipStats;

    public void Init(VesselShipStats shipStats)
    {
        this.shipStats = shipStats;
    }

    /// <summary>
    /// 
    /// </summary>
    public void PerformMovement()
    {
        currentVelocity = shipStats.speed * transform.forward * Time.fixedDeltaTime;
        transform.position += currentVelocity;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IIdentity {
    string GetObjectID();
}

public abstract class BaseEntityController : MonoBehaviour, IIdentity
{
    public string objectID = "testXWing";
    public VesselType vesselSelection;
    public Transform modelTransform;
    public StatHandler statHandler;
    public VesselShipStats shipStats;

    [HideInInspector] public VesselAudioSystem audioSystem;
    [HideInInspector] public CameraController cameraController;

    public string GetObjectID()
    {
        return objectID;
    }
}

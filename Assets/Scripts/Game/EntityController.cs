using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityController : MonoBehaviour, IEntity
{
    public string objectID = "testXWing";
    public VesselType vesselSelection;
    public Transform modelTransform;
    public StatHandler statHandler;

    [HideInInspector] public VesselAudioSystem audioSystem;
    [HideInInspector] public CameraController cameraController;

    public string GetObjectID()
    {
        return objectID;
    }
}

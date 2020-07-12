using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IIdentity {
    string GetObjectID();
}

public interface IEntity {
    StatHandler GetStatHandler();
    void OnEntityDeath();
}

public abstract class BaseEntityController : MonoBehaviour, IIdentity, IEntity
{
    public string objectID = "testXWing";
    public VesselType vesselSelection;
    public Transform modelTransform;
    public StatHandler statHandler;
    //public VesselShipStats shipStats;

    [HideInInspector] public DamageManager damageSystem;
    [HideInInspector] public VesselAudioSystem audioSystem;
    [HideInInspector] public CameraController cameraController;

    public string GetObjectID() {
        return objectID;
    }

    public StatHandler GetStatHandler() {
        return statHandler;
    }

    public virtual void OnEntityDeath() {}
}

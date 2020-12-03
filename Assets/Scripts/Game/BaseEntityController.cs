using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerSystems;

public interface IIdentity {
    string GetObjectID();
    void SetObjectID(string objectID);
}

public interface ITeam
{
    TeamColor GetTeamColor();
}

public interface IEntity
{
    void Init(SpawnManager spawner, TeamColor team);
    StatHandler GetStatHandler();
    void OnEntityDeath();
    string GetObjectID();
    void SetObjectID(string objectID);
}

public abstract class BaseEntityController : MonoBehaviour, IIdentity, IEntity, ITeam
{
    [Header("Entity Description")]
    public string objectID = "defaultID";
    public TeamColor teamColor;
    public VesselType vesselSelection;
    public Transform modelTransform;
    public StatHandler statHandler;
    [HideInInspector] public SpawnManager spawner;
    [HideInInspector] public CameraController cameraController; //TODO: Move camera controller in player scope
    [HideInInspector] public VesselAudioSystem audioSystem;

    public virtual void Init(SpawnManager spawner, TeamColor team) {
        this.spawner = spawner;
        this.teamColor = team;
    }

    public string GetObjectID() {
        return objectID;
    }

    public void SetObjectID(string objectID) {
        this.objectID = objectID;
    }

    public StatHandler GetStatHandler() {
        return statHandler;
    }

    public TeamColor GetTeamColor() {
        return teamColor;
    }

    public virtual void OnEntityDeath() 
    {
        SceneController sceneController = GameManager.Instance.sceneController;
        sceneController.CleanSpheres();
    }
}

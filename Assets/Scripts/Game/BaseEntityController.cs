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

        AutoRegisterToDetector();
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

    /// <summary>
    /// This autoregisters the entity on spaqwn to its nearest detector.
    /// </summary>
    private void AutoRegisterToDetector()
    {
        SceneController sceneController = GameManager.Instance.sceneController;

        float calculatedDistance = 0;
        float shortestDistance = 10000000f;
        IDetectorSphere closestDetector = null;

        foreach (IDetectorSphere detector in sceneController.sphereDetectors)
        {
            calculatedDistance = Vector3.Magnitude(detector.GetSpherePosition());

            if (calculatedDistance < shortestDistance)
            {
                closestDetector = detector;
                shortestDistance = calculatedDistance;
            }
        }

        closestDetector.RegisterEntity(this.gameObject);
    }

    public virtual void OnEntityDeath() 
    {
        SceneController sceneController = GameManager.Instance.sceneController;
        sceneController.CleanSpheres();
    }
}

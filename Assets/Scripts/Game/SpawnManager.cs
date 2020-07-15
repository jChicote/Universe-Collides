using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public interface ISpawner {}

public class SpawnManager : MonoBehaviour, ISpawner
{
    public SpawnArea[] spawnAreas;
    [HideInInspector] public SpawnEvent OnEntityRespawn;

    public void Awake() {
        OnEntityRespawn.AddListener(SetSpawn);

        for(int i = 0; i < spawnAreas.Length; i++) {
            spawnAreas[i].isOccupied = false;
        }
    }

    private void SetSpawn(string id, VesselType vesselType, EntityType entityType, TeamColor teamColor) {
        StartCoroutine(RespawnEntity(id, vesselType, entityType, teamColor, 0));
    }

    public void InitialSpawn(VesselType vesselType, EntityType entityType, int index, TeamColor teamColor){
        string id = IDGenerator(vesselType, entityType, index);
        StartCoroutine(RespawnEntity(id, vesselType, entityType, teamColor, 0)); 
    }

    private string IDGenerator(VesselType vesselType, EntityType entityType, int index) {
        return entityType.ToString() + vesselType.ToString() + "_" + index;
    }

    IEnumerator RespawnEntity(string id, VesselType vesselType, EntityType entityType, TeamColor teamColor, float time) {
        float timer = time;

        while(timer > 0){
            timer -= Time.deltaTime;
            yield return null;
        }

        //TODO : Refactor the statements below
        //Spawn entity
        //Replace with while that iterates until it finds a position after 10sec stop enumerator
        SpawnArea spawnArea = spawnAreas.Where(x => x.isOccupied == false).First();
        Transform spawnLocation = spawnArea.location;

        if(spawnArea.isOccupied) yield return null;

        GameSettings gameSettings = GameManager.Instance.gameSettings;
        GameObject prefab = gameSettings.vesselObjects.Where(x => x.entityType == entityType && x.vesselType == vesselType).First().objectPrefab;

        SpawnEntity(id, prefab, spawnLocation, teamColor);

        spawnArea.isOccupied = true;
        StartCoroutine(WaitUntilClear(spawnArea));
    }

    private void SpawnEntity(string id, GameObject prefab, Transform location, TeamColor teamColor) {
        GameObject entityObject = GameObject.Instantiate(prefab, location.position, location.rotation);
        IEntity entity = entityObject.GetComponent<IEntity>();
        entity.SetObjectID(id);
        entity.Init(this, teamColor);
    }

    IEnumerator WaitUntilClear(SpawnArea area) {
        float timer = 1f;
        while(timer > 0) {
            timer -= Time.deltaTime;
            yield return null;
        }

        area.isOccupied = false;
    }
}

[System.Serializable]
public class SpawnEvent : UnityEvent<string, VesselType, EntityType, TeamColor> {}

[System.Serializable]
public class SpawnArea {
    [HideInInspector] public bool isOccupied;
    public Transform location;
}

public enum EntityType {
    Player,
    AiComputer
}

public enum TeamColor {
    Red,
    Blue
}

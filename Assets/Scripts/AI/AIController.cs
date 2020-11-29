using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public interface ISetBehaviour
{
    void SetState<T>() where T : BaseState;
}

public class AIController : BaseEntityController, ISetBehaviour
{
    [HideInInspector] public AvoidanceSystem avoidanceSystem;
    [HideInInspector] public AIStateManager enemyState = null;
    
    private UIEntityPointer attachedPointer;

    public override void Init(SpawnManager spawner, TeamColor team)
    {
        base.Init(spawner, team);
        this.spawner = spawner;
        this.teamColor = team;

        //Get singleton controllers
        GameManager gameaManager = GameManager.Instance;
        GameSettings gameSettings = GameManager.Instance.gameSettings;
        SceneController sceneController = gameaManager.sceneController;

        if (enemyState == null)
            enemyState = this.gameObject.AddComponent<AIStateManager>();
        avoidanceSystem = this.GetComponent<AvoidanceSystem>();

        //Initialise audio system
        audioSystem = this.GetComponent<VesselAudioSystem>();
        audioSystem.Init(EntityType.AiComputer);

        //Initialise Stat Handler
        VesselShipStats vesselStats = gameSettings.vesselStats.Where(x => x.type == vesselSelection).First();
        BaseStats enemyStats = vesselStats.baseShipStats;
        statHandler = new StatHandler(enemyStats, EntityType.AiComputer, this);

        //Initialises the movement systems
        AIMovementController movementController = this.GetComponent<AIMovementController>();
        movementController.Init(vesselStats);

        // Initialise health system
        AIHealthSystem healthSystem = this.GetComponent<AIHealthSystem>();
        healthSystem.Init(statHandler);

        //Set weapon systems and state
        SetWeaponSystems(vesselStats, sceneController);
        SetState<AIPursuit>();

        //Returns pointer UI
        attachedPointer = GameManager.Instance.sceneController.dynamicHud.pointerManagerUI.CreateNewPointer(gameObject.transform, vesselStats.speed);
    }

    /// <summary>
    /// Retrieves and intiialises the weapon system attached on the ship.
    /// </summary>
    void SetWeaponSystems(VesselShipStats vesselStats, SceneController sceneController) {
       
        IWeaponSystem weaponSystem;
        weaponSystem = this.GetComponent<IWeaponSystem>();
        weaponSystem.Init(GetObjectID(), this, true, vesselStats, sceneController);

        //Load weapon/damage components.
        FighterDamageManager damageSystem = this.GetComponent<FighterDamageManager>();
        damageSystem.Init(statHandler, audioSystem);
    }

    /// <summary>
    /// Sets the behaviour state of the AI with generic type of basestate.
    /// </summary>
    public void SetState<T>() where T : BaseState {
        enemyState.AddState<T>();
    }

    /// <summary>
    /// Method invoked if the player were to die and are final actions performed before death.
    /// </summary>
    public override void OnEntityDeath() {
        spawner.OnEntityRespawn.Invoke(objectID, vesselSelection, EntityType.AiComputer, teamColor);
        attachedPointer.RemovePointer();
        //GameManager.Instance.scoreManager.OnScoreEvent.Invoke(teamColor, 100);
    }
}

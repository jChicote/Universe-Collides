using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AIController : BaseEntityController 
{
    public AvoidanceSystem avoidanceSystem;

    [HideInInspector] public AIStateManager enemyState = null;

    void Awake() {
        if (enemyState == null) 
            enemyState = this.gameObject.AddComponent<AIStateManager>();
        avoidanceSystem = this.gameObject.AddComponent<AvoidanceSystem>();
        audioSystem = this.gameObject.AddComponent<VesselAudioSystem>();

        //Initialise Stat Handler
        GameSettings gameSettings = GameManager.Instance.gameSettings;
        VesselShipStats vesselStats = gameSettings.vesselStats.Where(x => x.type == vesselSelection).First();
        BaseStats enemyStats = vesselStats.baseShipStats;
        statHandler = new StatHandler(enemyStats);
    }

    void Start() {
        SetWeaponSystems();
        SetState<AIPursuit>();

        Debug.Log("Enemy Health: " + statHandler.CurrentHealth);
        Debug.Log("Enemy Damage: " + statHandler.CriticalDamage);
    }

    void SetWeaponSystems() {
        BaseWeaponSystem weaponSystem;
        VesselShipStats vesselStats = GameManager.Instance.gameSettings.vesselStats.Where(x => x.type == vesselSelection).First();
       
        switch(vesselSelection){
            case VesselType.xWing:
                weaponSystem = this.gameObject.AddComponent<XWingWeaponSystem>();
                weaponSystem.Init(GetObjectID(), this, true, vesselStats);
                break;
            case VesselType.TieFighter:
                weaponSystem = this.gameObject.AddComponent<TieWeaponSystem>();
                weaponSystem.Init(GetObjectID(), this, true, vesselStats);
                break;
        }
    }

    public void SetState<T>() where T : BaseState {
        enemyState.AddState<T>();
    }
}

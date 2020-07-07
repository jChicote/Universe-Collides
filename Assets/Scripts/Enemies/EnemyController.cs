using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyController : EntityController 
{
    public AvoidanceSystem avoidanceSystem;
    //public BaseWeaponSystem weaponSystem;

    [HideInInspector] public EnemyStateManager enemyState = null;

    void Awake() {
        if (enemyState == null) 
            enemyState = this.gameObject.AddComponent<EnemyStateManager>();
        avoidanceSystem = this.gameObject.AddComponent<AvoidanceSystem>();
        audioSystem = this.gameObject.AddComponent<VesselAudioSystem>();

        GameSettings gameSettings = GameManager.Instance.gameSettings;
        VesselShipStats vesselStats = gameSettings.vesselStats.Where(x => x.type == vesselSelection).First();
        BaseStats enemyStats = vesselStats.baseShipStats;
        statHandler = new StatHandler(enemyStats);
    }

    void Start() {
        SetWeaponSystems();
        SetState<EnemyPursuit>();

        Debug.Log("Enemy Health: " + statHandler.CurrentHealth);
        Debug.Log("Enemy Damage: " + statHandler.CriticalDamage);
    }

    void SetWeaponSystems() {
        BaseWeaponSystem weaponSystem;
        switch(vesselSelection){
            case VesselType.xWing:
                weaponSystem = this.gameObject.AddComponent<XWingWeaponSystem>();
                weaponSystem.SetupSystem(GetObjectID(), this, true);
                break;
            case VesselType.TieFighter:
                weaponSystem = this.gameObject.AddComponent<TieWeaponSystem>();
                weaponSystem.SetupSystem(GetObjectID(), this, true);
                break;
        }
    }

    public void SetState<T>() where T : BaseState {
        enemyState.AddState<T>();
    }

    void FixedUpdate()
    {
        if(enemyState.currentState == null) return;
        enemyState.currentState.RunState();
    }
}

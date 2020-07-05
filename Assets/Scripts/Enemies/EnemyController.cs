using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : EntityController 
{
    public VesselType vesselSelection;
    public Transform modelTransform;
    public AvoidanceSystem avoidanceSystem;

    [HideInInspector] public EnemyStateManager enemyState = null;
    [HideInInspector] public VesselAudioSystem audioSystem;
    [HideInInspector] public DamageManager damageSystem;

    void Awake() {
        if (enemyState == null) enemyState = this.gameObject.AddComponent<EnemyStateManager>();
        damageSystem = this.gameObject.AddComponent<FighterDamageManager>();
        avoidanceSystem = this.gameObject.AddComponent<AvoidanceSystem>();
        //audioSystem = this.gameObject.AddComponent<VesselAudioSystem>();
        //damageManager = this.gameObject.AddComponent<DamageManager>();
    }

    void Start() {
        SetState<EnemyWander>();
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

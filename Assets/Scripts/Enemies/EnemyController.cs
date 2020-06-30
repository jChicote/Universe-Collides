using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : EntityController 
{
    public VesselType vesselSelection;
    public Transform modelTransform;

    [HideInInspector] public EnemyStateManager enemyState = null;
    //[HideInInspector] public DamageManager damageManager;
    [HideInInspector] public VesselAudioSystem audioSystem;

    void Awake() {
        if (enemyState == null) enemyState = this.gameObject.AddComponent<EnemyStateManager>();
        audioSystem = this.gameObject.AddComponent<VesselAudioSystem>();
        //damageManager = this.gameObject.AddComponent<DamageManager>();
    }

    void Start() {
        SetState<EnemyMoveState>();
    }

    public void SetState<T>() where T : BaseState {
        enemyState.AddState<T>();
    }
}

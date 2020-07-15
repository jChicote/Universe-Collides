using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;

public abstract class BattleModeSpawner
{
    public GameManager gameManager;
    public SpawnManager spawnerA;
    public SpawnManager spawnerB;

    public void Init(GameManager gameManager, SpawnManager spawnerA, SpawnManager spawnerB) {
        this.gameManager = gameManager;
        this.spawnerA = spawnerA;
        this.spawnerB = spawnerB;
    }

    public virtual void RunInstance() {}
}

[System.Serializable]
public class SpawnerModes {
    public BattleMode mode;
    public string className;
    public Type modeClassType {
        get { return Type.GetType(className.ToString()); }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeForAllSpawner : BattleModeSpawner
{
    public override void RunInstance() {
        SetEnemyTeam();
        SetPlayerTeam();
    }

    void SetEnemyTeam() {
        for(int i = 0; i < spawnerA.GetSpawnAreaLength(); i++) {
            spawnerA.InitialSpawn(VesselType.TieFighter, EntityType.AiComputer, 0, TeamColor.Red);
        }
    }

    void SetPlayerTeam() {
        spawnerB.InitialSpawn(VesselType.xWing, EntityType.Player, 1, TeamColor.Blue);
    }
}

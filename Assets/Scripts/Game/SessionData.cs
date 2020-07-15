using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SessionData : MonoBehaviour
{
    public int currentSceneIndex = 0;
    bool isPaused = false;

    //Gameplay Selection
    VesselType vesselType;
    BattleMode battleMode;

    public void SetGamePlay(VesselType vesselType, BattleMode battleMode) {
        this.vesselType = vesselType;
        this.battleMode = battleMode;
    }

    public void TogglePause() {
        isPaused = !isPaused;
        GameManager.Instance.gameplayHUD.RevealPauseScreen(isPaused);

        List<GameObject> pausables = FindObjectsOfType<GameObject>().Where(x => x.GetComponent<IPausable>() != null).ToList();
        foreach (GameObject p in pausables) {
            if (isPaused)
                p.GetComponent<IPausable>().Pause();
            else
                p.GetComponent<IPausable>().UnPause();
        }
    }
}

public enum BattleMode {
    FreeForAll,
    TeamBattle
}

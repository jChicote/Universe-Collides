using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SessionData : MonoBehaviour
{
    public int currentSceneIndex = 0;
    public bool isPaused = false;

    //Gameplay Selection
    public VesselType vesselType;
    public BattleMode battleMode;

    public void SetGamePlay(VesselType vesselType, BattleMode battleMode) {
        this.vesselType = vesselType;
        this.battleMode = battleMode;
    }

    public void TogglePause() {
        isPaused = !isPaused;
        GameManager.Instance.sceneController.gameplayHUD.RevealPauseScreen(isPaused);

        List<GameObject> pausables = FindObjectsOfType<GameObject>().Where(x => x.GetComponent<IPausable>() != null).ToList();
        foreach (GameObject p in pausables)
        {
            if (isPaused)
                p.GetComponent<IPausable>().Pause();
            else
                p.GetComponent<IPausable>().UnPause();
        }

        List<GameObject> particleSystems = FindObjectsOfType<GameObject>().Where(x => x.GetComponent<ParticleSystem>() != null).ToList();
        foreach (GameObject particles in particleSystems)
        {
            if (isPaused)
                particles.GetComponent<ParticleSystem>().Pause();
            else
                particles.GetComponent<ParticleSystem>().Play();
        }
    }
}

public enum BattleMode {
    FreeForAll,
    TeamBattle
}

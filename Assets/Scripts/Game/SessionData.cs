using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SessionData : MonoBehaviour
{
    public int currentSceneIndex = 0;
    bool isPaused = false;

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

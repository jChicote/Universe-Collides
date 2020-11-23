using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseUI : MonoBehaviour
{
    public GameObject settingsUI;

    // Start is called before the first frame update
    void Start()
    {
        settingsUI.SetActive(false);
    }

    public void OnResume() {
        settingsUI.SetActive(false);
        GameManager.Instance.sessionData.TogglePause();
    }

    public void Respawn() {
        
    }

    public void Settings() {
        settingsUI.SetActive(!settingsUI.activeInHierarchy);
    }

    public void Restart() {
        settingsUI.SetActive(false);
        GameManager.Instance.sceneLoader.RestartLevel(1); //Must store current index within sessiondata
    }

    public void Quit() {
        GameManager.Instance.sceneLoader.LoadMainMenu();
    }
}

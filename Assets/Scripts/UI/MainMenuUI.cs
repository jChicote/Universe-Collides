using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    public GameObject playScreen;
    public GameObject fighterModeScreen;
    public GameObject starshipModeScreen;

    void Start() {
        GameManager.Instance.OnGameplayStart.AddListener(OnClose);
        DisplayHome();
    }

    public void DisplayHome() {
        playScreen.SetActive(false);
        fighterModeScreen.SetActive(false);
        starshipModeScreen.SetActive(false);
    }

    public void DisplayPlaySelection() {
        playScreen.SetActive(true);
        fighterModeScreen.SetActive(false);
        starshipModeScreen.SetActive(false);
    }

    public void DisplayFighterSelection() {
        GameManager.Instance.LoadLevel(1);
    }

    public void OnClose() {
        GameObject.Destroy(this.gameObject); 
    }
}

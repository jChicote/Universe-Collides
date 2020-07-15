using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    public GameObject playScreen;
    public GameObject navigationMenu;
    public GameObject fighterModeScreen;
    public GameObject starshipModeScreen;
    public GameObject battleModeScreen;

    VesselType vesselSelection;
    BattleMode battleModeSelection;

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
        playScreen.SetActive(false);
        navigationMenu.SetActive(false);
        battleModeScreen.SetActive(true);
    }

    //Fighter Selection
    public void SelectFreeForAll() {
        vesselSelection = VesselType.xWing; //Need to chage for vessel selections
        battleModeSelection = BattleMode.FreeForAll;
    }

    public void SelectTeamBattle() {
        vesselSelection = VesselType.xWing; //Need to chage for vessel selections
        battleModeSelection = BattleMode.TeamBattle;
    }

    //public void VesselSelection() {
        //Left EMPTY UNTIL PROPER UI SOLUTION IS LEARNED
    //}

    public void ReturnToMenu() {
        battleModeScreen.SetActive(false);
        navigationMenu.SetActive(true);
    }

    //Fighter Mode Selection

    public void ContinueToBattle() {
        GameManager.Instance.sessionData.SetGamePlay(vesselSelection , battleModeSelection);
        GameManager.Instance.LoadLevel(1);
    }

    public void OnClose() {
        GameObject.Destroy(this.gameObject); 
    }
}

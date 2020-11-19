using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHudManager : MonoBehaviour
{
    GameManager gameManager;
    
    public AimUI aimSightUI;
    public PauseUI pauseUI;
    public PowerUpUI powerUpUI;
    public UIPointerManager pointerManagerUI;
    public ScoreUI scoreUI;
    public HealthBarUI healthBar;
    public ThrustUI thrustUI;

    public void Init(AimUI aimUI, UIPointerManager pointerUI, ScoreUI scoreUI, HealthBarUI healthBar, ThrustUI thrustUI) {
        this.gameManager = GameManager.Instance;
        this.aimSightUI = aimUI;
        this.pointerManagerUI = pointerUI;
        this.scoreUI = scoreUI;
        this.healthBar = healthBar;
        this.thrustUI = thrustUI;
    }

    public void RevealPauseScreen(bool isRevealed) {
        pauseUI.gameObject.SetActive(isRevealed);

        HideInGameHUD(!isRevealed);
    }

    public void HideInGameHUD(bool isActive) //TODO: Must refactor this into a more modular form
    {
        powerUpUI.gameObject.SetActive(isActive);
        aimSightUI.gameObject.SetActive(isActive);
        healthBar.gameObject.SetActive(isActive);
        thrustUI.gameObject.SetActive(isActive);
        scoreUI.gameObject.SetActive(isActive);
    }
}

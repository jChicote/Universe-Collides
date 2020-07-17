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

    public void Init(AimUI aimUI, UIPointerManager pointerUI, ScoreUI scoreUI) {
        this.gameManager = GameManager.Instance;
        this.aimSightUI = aimUI;
        this.pointerManagerUI = pointerUI;
        this.scoreUI = scoreUI;
    }

    public void RevealPauseScreen(bool isRevealed) {
        pauseUI.gameObject.SetActive(isRevealed);
    }
}

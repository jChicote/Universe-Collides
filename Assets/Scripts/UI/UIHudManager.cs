using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHudManager : MonoBehaviour
{
    GameManager gameManager;
    
    public AimUI aimSightUI;
    public PauseUI pauseUI;
    public PowerUpUI powerUpUI;

    void Start()
    {
        gameManager = GameManager.Instance;
    }

    public void Init(AimUI aimUI) {
        this.aimSightUI = aimUI;
    }

    public void RevealPauseScreen(bool isRevealed) {
        pauseUI.gameObject.SetActive(isRevealed);
    }
}

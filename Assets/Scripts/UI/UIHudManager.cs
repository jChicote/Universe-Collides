using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHudManager : MonoBehaviour
{
    GameManager gameManager;
    
    public AimUI aimSightUI;
    public PauseUI pauseUI;

    void Start()
    {
        gameManager = GameManager.Instance;
        aimSightUI.parentTransform = this.GetComponent<RectTransform>();
    }

    public void RevealPauseScreen(bool isRevealed) {
        pauseUI.gameObject.SetActive(isRevealed);
    }
}

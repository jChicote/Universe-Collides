using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UIStaticHudManager : MonoBehaviour
{
    public UDynamicHUDManager dynamicHudManager;

    [Header("Static UI Compoenents")]
    public PauseUI pauseUI;
    public PowerUpUI powerUpUI;

    public void Init(UDynamicHUDManager dynamicHud)
    {
        this.dynamicHudManager = dynamicHud;
    }

    public void RevealPauseScreen(bool isRevealed)
    {
        pauseUI.gameObject.SetActive(isRevealed);

        dynamicHudManager.HideInGameHUD(!isRevealed);
        powerUpUI.gameObject.SetActive(!isRevealed);
    }
}

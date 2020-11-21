using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UDynamicHUDManager : MonoBehaviour
{
    public GameSettings gameSettings;

    [Header("Dynamic UI Components")]
    public AimUI aimSightUI;
    public UIPointerManager pointerManagerUI;
    public ScoreUI scoreUI;
    public HealthBarUI healthBar;
    public ThrustUI thrustUI;

    public void Init(Camera sceneCamera)
    {
        gameSettings = GameManager.Instance.gameSettings;

        aimSightUI.Init();
        pointerManagerUI.Init(sceneCamera);
        scoreUI.Init();
    }

    public void HideInGameHUD(bool isActive)
    {
        aimSightUI.gameObject.SetActive(isActive);
        healthBar.gameObject.SetActive(isActive);
        thrustUI.gameObject.SetActive(isActive);
        scoreUI.gameObject.SetActive(isActive);
    }
}

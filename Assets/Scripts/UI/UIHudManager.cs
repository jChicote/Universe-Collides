using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHudManager : MonoBehaviour
{
    GameManager gameManager;
    
    public AimUI aimSightUI;

    void Start()
    {
        gameManager = GameManager.Instance;
        aimSightUI.parentTransform = this.GetComponent<RectTransform>();
    }
}

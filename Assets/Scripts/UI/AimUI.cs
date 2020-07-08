using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AimUI : MonoBehaviour
{
    public RectTransform aimTransform;

    GameManager gameManager;
    RectTransform parentTransform;
    Canvas parentCanvas;

    Vector2 screenPosition;
    Vector2 scaledScreenPosition;

    float scaledWidth;
    float scaledHeight;

    void Awake()
    {
        parentCanvas = this.GetComponent<Canvas>();
        parentTransform = this.GetComponent<RectTransform>();
        gameManager = GameManager.Instance;
    }

    public void SetAimPosition(Vector3 futurePosition) {
        if(gameManager.sceneCamera == null) return;

        screenPosition = RectTransformUtility.WorldToScreenPoint(gameManager.sceneCamera, futurePosition);

        scaledWidth = parentTransform.rect.width * (screenPosition.x / Screen.width) * 1;
        scaledHeight = parentTransform.rect.height * (screenPosition.y / Screen.height) * 1;
        
        scaledScreenPosition = new Vector2(scaledWidth,scaledHeight);
        aimTransform.anchoredPosition = scaledScreenPosition;
    }
}

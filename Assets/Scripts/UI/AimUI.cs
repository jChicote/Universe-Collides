using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AimUI : MonoBehaviour
{
    public RectTransform firstAimsight;
    public RectTransform secondAimsight;
    public RectTransform thirdAimsight;

    GameManager gameManager;
    RectTransform parentTransform;
    Canvas parentCanvas;

    Vector2 screenPosition;
    Vector2 scaledScreenPosition;

    float scaledWidth;
    float scaledHeight;

    Vector3 defaultThirdScale;
    Vector3 newScale;

    public void Init()
    {
        parentCanvas = this.GetComponent<Canvas>();
        parentTransform = this.GetComponent<RectTransform>();
        gameManager = GameManager.Instance;
        defaultThirdScale = thirdAimsight.localScale;
    }

    public void SetAimPosition(Vector3 playerPosition, Vector3 forward, float maxTimeAhead, bool isFocused) {
        if(gameManager.sceneCamera == null) return;

        Vector3 futurePosition = playerPosition + forward * maxTimeAhead * 3f;
        PositionAimUI(futurePosition, firstAimsight);
        
        futurePosition = playerPosition + forward * (maxTimeAhead * 0.6f) * 3f;
        PositionAimUI(futurePosition, secondAimsight);

        futurePosition = playerPosition + forward * (maxTimeAhead * 0.3f) * 3f;
        PositionAimUI(futurePosition, thirdAimsight);

        if(isFocused) ScaleAimUI(2);
        ScaleAimUI(maxTimeAhead);
    }

    void PositionAimUI(Vector3 futurePosition, RectTransform aimTransform) {
        screenPosition = RectTransformUtility.WorldToScreenPoint(gameManager.sceneCamera, futurePosition);

        scaledWidth = parentTransform.rect.width * (screenPosition.x / Screen.width) * 1;
        scaledHeight = parentTransform.rect.height * (screenPosition.y / Screen.height) * 1;
        
        scaledScreenPosition = new Vector2(scaledWidth,scaledHeight);
        aimTransform.anchoredPosition = scaledScreenPosition;
    }

    void ScaleAimUI(float maxTimeAhead){

        //Debug.Log(maxTimeAhead);
        if(maxTimeAhead > 15){
            CalculateAimUIScale(1.3f, secondAimsight);
            CalculateAimUIScale(1.3f, thirdAimsight);
        } else if(maxTimeAhead < 7f) {
            CalculateAimUIScale(0.5f, secondAimsight);
            CalculateAimUIScale(0.5f, thirdAimsight);
        } else {
            CalculateAimUIScale(1, secondAimsight);
            CalculateAimUIScale(1, thirdAimsight);
        }
    }

    void CalculateAimUIScale(float scaleMultiplier, RectTransform aimTransform) {
        newScale = Vector3.Lerp(thirdAimsight.localScale, defaultThirdScale * scaleMultiplier, 0.05f);
        aimTransform.localScale = newScale;
    }
}
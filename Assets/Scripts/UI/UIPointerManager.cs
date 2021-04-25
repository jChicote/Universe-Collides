using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPointerManager : MonoBehaviour
{
    public RectTransform canvasTransform;
    public Camera cam;
    public List<UIEntityPointer> entityPointers;
    

    void FixedUpdate()
    {
        if(cam == null) {
            cam = GameManager.Instance.sceneController.sceneCamera;
            return;
        }

        foreach(UIEntityPointer pointer in entityPointers) {
            pointer.RunPointer();
        }
    }

    public void Init(Camera camera, RectTransform parentRectTransform) {
        this.cam = GameManager.Instance.sceneController.sceneCamera;
        canvasTransform = parentRectTransform;
    }

    public UIEntityPointer CreateNewPointer(Transform target, float shipSpeed) {
        GameObject prefab = GameManager.Instance.gameSettings.UiPointerControllerPrefab;
        UIEntityPointer pointer = Instantiate(prefab, this.transform).GetComponent<UIEntityPointer>();

        //Debug.Log(cam + " + " + canvasTransform);
        pointer.Init(shipSpeed, GameManager.Instance.sceneController.sceneCamera, canvasTransform, target, this);
        entityPointers.Add(pointer);
        return pointer;
    }
    
    public void RemoveFromList(UIEntityPointer pointer) {
        entityPointers.Remove(pointer);
    }
}

public class PointedItem {
    public bool isTargeted = false;
    public GameObject entity; 
}

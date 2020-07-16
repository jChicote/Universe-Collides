using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPointerManager : MonoBehaviour
{
    public RectTransform canvasTransform;

    public Camera cam;
    public List<UIEntityPointer> entityPointers;


    void Start()
    {
        canvasTransform = this.GetComponent<RectTransform>();
    }

    void FixedUpdate()
    {
        if(cam == null) {
            cam = GameManager.Instance.sceneCamera;
            return;
        }

        foreach(UIEntityPointer pointer in entityPointers) {
            pointer.RunPointer();
        }
    }

    public void Init(Camera camera) {
        this.cam = camera;
    }

    public UIEntityPointer CreateEntity(Transform target, float shipSpeed) {
        GameObject prefab = GameManager.Instance.gameSettings.UiPointerControllerPrefab;
        UIEntityPointer pointer = Instantiate(prefab, transform.position, Quaternion.identity).GetComponent<UIEntityPointer>();
        pointer.Init(shipSpeed, cam, canvasTransform, target, this);
        pointer.transform.SetParent(this.transform);
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

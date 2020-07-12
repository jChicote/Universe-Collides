using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPointerManager : MonoBehaviour
{
    public GameObject targetedEntitiy; //TEST

    public RectTransform canvasTransform;

    //Test Pointers
    public RectTransform targetInsidePointer;
    public RectTransform targetOutsideArrow;

    public Camera cam;

    public UIEntityPointer primaryTarget;
    public List<UIEntityPointer> entityPointers;


    void Start()
    {
        canvasTransform = this.GetComponent<RectTransform>();
        //targetedEntitiy = GameObject.Find("Tie_Fighter_Enemy");
    }

    void FixedUpdate()
    {
        if(cam == null) {
            cam = GameManager.Instance.sceneCamera;
            //CreateEntity(targetedEntitiy.transform);
            return;
        }

        foreach(UIEntityPointer pointer in entityPointers) {
            pointer.RunPointer();
        }
    }

    public void Init(Camera camera) {
        this.cam = camera;
    }

    public UIEntityPointer CreateEntity(Transform target) {
        GameObject prefab = GameManager.Instance.gameSettings.uiPointerPrefab;
        UIEntityPointer pointer = Instantiate(prefab, transform.position, Quaternion.identity).GetComponent<UIEntityPointer>();
        pointer.Init(cam, canvasTransform, target, this);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIEntityPointer : MonoBehaviour, IPausable
{
    public Transform target;
    public RectTransform canvasTransform;

    UIPointerManager manager;
    RectTransform pointerTransform;
    RectTransform arrowTransform;
    public Image arrowImage;

    Camera cam;
    Vector3 forward;
    Vector3 targetDir;

    Vector3 screenPosition;
    Vector2 scaledScreenPosition;

    Vector3 arrowPoint = Vector3.zero;
    Vector3 center;
    Vector3 objScreenPos;
    Vector3 cross;
    Vector3 dir;

    float scaledWidth;
    float scaledHeight;
    float angle;

    bool isPaused = false;

    void Start()
    {
        arrowTransform = this.GetComponent<RectTransform>();
        pointerTransform = Instantiate(GameManager.Instance.gameSettings.uiPointingPrefab, transform.position, Quaternion.identity).GetComponent<RectTransform>();
        pointerTransform.transform.SetParent(transform.root);
    }

    public void Init(Camera camera, RectTransform canvasTransform, Transform target, UIPointerManager manager) {
        this.manager = manager;
        this.cam = camera;
        this.canvasTransform = canvasTransform;
        this.target = target;
    }

    public void RunPointer()
    {
        if(isPaused) return;

        if(CheckIfTargetInSight()) {
            arrowImage.gameObject.SetActive(false);
            pointerTransform.gameObject.SetActive(true);
            PointerInFrame();
        }
        else {
            pointerTransform.gameObject.SetActive(false);
            arrowImage.gameObject.SetActive(true);
            PointingOutFrame();
        }

    }

    bool CheckIfTargetInSight() {
        forward = cam.transform.forward.normalized;
        targetDir = (target.position - cam.transform.position).normalized;

        if(Vector3.Dot(forward, targetDir) > 0.8f) {
            return true;
        } else {
            return false;
        }
    }

    public void PointerInFrame() {
        screenPosition = cam.WorldToScreenPoint(target.position);
        scaledWidth = canvasTransform.rect.width * (screenPosition.x / Screen.width) * 1;
        scaledHeight = canvasTransform.rect.height * (screenPosition.y / Screen.height) * 1;

        scaledScreenPosition = new Vector2(scaledWidth,scaledHeight);
        pointerTransform.anchoredPosition = scaledScreenPosition;
    }

    public void PointingOutFrame() {
        center = new Vector3(Screen.width / 2, Screen.height / 2, 0);

        objScreenPos = cam.WorldToScreenPoint(target.position);
        dir = (objScreenPos - center).normalized;
        angle = Mathf.Rad2Deg * Mathf.Acos(Vector3.Dot(dir, Vector3.up));

        cross = Vector3.Cross(dir, Vector3.up);
        angle = -Mathf.Sign(cross.z) * angle;
        arrowTransform.localEulerAngles = new Vector3(arrowTransform.localEulerAngles.x, arrowTransform.localEulerAngles.y, angle);
    }

    public void RemovePointer() {
        GameObject.Destroy(pointerTransform.gameObject);
        manager.RemoveFromList(this);
        Destroy(this.gameObject);
    }

    public void Pause()
    {
        isPaused = true;
    }

    public void UnPause()
    {
        isPaused = false;
    }
}

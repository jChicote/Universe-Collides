using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIEntityPointer : MonoBehaviour, IPausable
{
    public Transform target;
    public RectTransform canvasTransform;

    UIPointerManager manager;
    RectTransform controllerRect;

    //Arrow Pointer
    RectTransform arrowTransform;

    //Inview Pointer
    RectTransform pointerTransform;
    RectTransform predictiveAimPoint;
    Image pointerImage;
    public Sprite inactivePointer;
    public Sprite activePointer;

    Camera cam;
    Vector3 forward;
    Vector3 targetDir;

    Vector3 screenPosition;
    Vector2 scaledScreenPosition;

    //Vector3 arrowPoint = Vector3.zero;
    Vector3 center;
    Vector3 objScreenPos;
    Vector3 cross;
    Vector3 dir;

    float scaledWidth;
    float scaledHeight;
    float angle;
    float shipSpeed;

    bool isPaused = false;

    void Start()
    {
        GameSettings gameSettings = GameManager.Instance.gameSettings;
        controllerRect = this.GetComponent<RectTransform>();
        arrowTransform = Instantiate(gameSettings.arrowUIPrefab, canvasTransform.transform.position, Quaternion.identity).GetComponent<RectTransform>();
        pointerTransform = Instantiate(gameSettings.pointerUIPrefab, canvasTransform.transform.position, Quaternion.identity).GetComponent<RectTransform>();
        predictiveAimPoint = Instantiate(gameSettings.predictiveUIPrefab, canvasTransform.transform.position, Quaternion.identity).GetComponent<RectTransform>();
        pointerImage = pointerTransform.GetComponent<Image>();

        arrowTransform.transform.SetParent(this.transform.root);
        pointerTransform.transform.SetParent(this.transform.root);
        predictiveAimPoint.transform.SetParent(this.transform.root);
    }

    public void Init(float speed, Camera camera, RectTransform canvasTransform, Transform target, UIPointerManager manager) {
        this.manager = manager;
        this.cam = camera;
        this.canvasTransform = canvasTransform;
        this.target = target;
        this.shipSpeed = speed;
    }

    public void RunPointer()
    {
        if(isPaused) return;

        if(CheckIfTargetInSight()) {
            arrowTransform.gameObject.SetActive(false);
            pointerTransform.gameObject.SetActive(true);
            PointerInFrame();
        }
        else {
            pointerTransform.gameObject.SetActive(false);
            arrowTransform.gameObject.SetActive(true);
            PointingOutFrame();
        }
    }

    bool CheckIfTargetInSight() {
        forward = cam.transform.forward.normalized;
        targetDir = (target.position - cam.transform.position).normalized;

        ChangePointerImageInView(forward, targetDir);

        if(Vector3.Dot(forward, targetDir) > 0.5f) {
            return true;
        } else {
            return false;
        }
    }

    //Summary: 
    //      Changes image once is within the inner range of firing.
    void ChangePointerImageInView(Vector3 forward, Vector3 targetDir) {
        if(Vector3.Dot(forward, targetDir) > 0.9f) {
            pointerImage.sprite = activePointer;
            pointerTransform.localScale = new Vector3(1f, 1f, 1f);
            predictiveAimPoint.gameObject.SetActive(true);
            RenderPredictiveAim();
        } else {
            pointerImage.sprite = inactivePointer;
            pointerTransform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
            predictiveAimPoint.gameObject.SetActive(false);
        }
    }

    void RenderPredictiveAim() {
        Vector3 futurePosition = target.position + target.forward * (shipSpeed*0.3f);
        screenPosition = RectTransformUtility.WorldToScreenPoint(cam, futurePosition);
        scaledWidth = canvasTransform.rect.width * (screenPosition.x / Screen.width) * 1;
        scaledHeight = canvasTransform.rect.height * (screenPosition.y / Screen.height) * 1;
        
        scaledScreenPosition = new Vector2(scaledWidth,scaledHeight);
        predictiveAimPoint.anchoredPosition = scaledScreenPosition;
    }

    public void PointerInFrame() {
        screenPosition = RectTransformUtility.WorldToScreenPoint(cam, target.position);
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
        GameObject.Destroy(arrowTransform.gameObject);
        GameObject.Destroy(predictiveAimPoint.gameObject);
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

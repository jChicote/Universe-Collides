using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableItem : MonoBehaviour, IDamageReciever, IIdentity
{
    public string objectID = "DestructID_1";
    public bool mustReveal = false;

    UIEntityPointer attachedPointer;
    public string GetObjectID()
    {
        return objectID;
    }

    public void Update()
    {
        if (mustReveal && attachedPointer == null)
        {
            attachedPointer = GameManager.Instance.sceneController.dynamicHud.pointerManagerUI.CreateNewPointer(gameObject.transform, 5);
        }
    }

    public void SetObjectID(string objectID) {
        this.objectID = objectID;
    }

    public void OnRecievedDamage(float damage, string id, SoundType soundType)
    {
        //Debug.Log(damage);
        //Debug.Log(id);
    }
}

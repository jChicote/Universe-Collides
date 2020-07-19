using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableItem : MonoBehaviour, IDamageReciever, IIdentity
{
    public string objectID = "DestructID_1";
    public string GetObjectID()
    {
        return objectID;
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableItem : MonoBehaviour, IDamageReciever, IEntity
{

    public string objectID = "DestructID_1";
    public string GetObjectID()
    {
        return objectID;
    }

    public void OnRecievedDamage(float damage, string id)
    {
        Debug.Log(damage);
        Debug.Log(id);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityController : MonoBehaviour, IEntity
{
    public string objectID = "testXWing";

    public string GetObjectID()
    {
        return objectID;
    }
}

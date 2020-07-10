using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEntityPointer : MonoBehaviour
{
    public GameObject targetedEntitiy;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    
}

public class PointedItem {
    public bool isTargeted = false;
    public GameObject entity; 
}

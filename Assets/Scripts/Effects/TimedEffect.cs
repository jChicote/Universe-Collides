using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedEffect : MonoBehaviour
{
    public float timeLimit = 1;

    private void Start()
    {
        Invoke("RemoveFromScene", timeLimit);
    }

    private void RemoveFromScene()
    {
        Destroy(gameObject);
    }
}

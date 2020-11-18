using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTest : MonoBehaviour
{
    public GameObject targetDamageTest;
    public IDamageReciever targetDamage;

    public bool testDamage = false;

    // Update is called once per frame
    void Update()
    {
        if (targetDamage == null && targetDamageTest != null)
        { 
            targetDamage = targetDamageTest.GetComponent<IDamageReciever>();
            return;
        }

        if (testDamage && targetDamage != null)
        {
            targetDamage.OnRecievedDamage(20, "testID", SoundType.BoltImpact);
            testDamage = false;
        }
    }
}

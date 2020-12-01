using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDecideThreat
{
    void DecideNewTarget();
}


/// <summary>
/// This is for decision making for the AI and analysis of its perception of environemnt.
/// </summary>
public class AIBrain : MonoBehaviour, IDecideThreat
{
    private GameObject likelyThreat;
    private float threatValue;

    private GameObject currentTarget;
    private IAssignTarget targetAssigner;
    private float targetInterest;

    private float fearValue;

    public void Init()
    {

    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        DetermineFear();
        DetermineThreat();
        
    }

    private void DetermineFear()
    {

    }

    private void DetermineThreat()
    {
        //if there is a target
    }

    public void DecideNewTarget()
    {

        targetAssigner.AssignTarget(null);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIStateManager : MonoBehaviour
{
    BaseState currentState;

    public void AddState<T>() where T : BaseState
    {
        if(currentState != null)
        {
            this.RemoveState();
        }

        currentState = this.gameObject.AddComponent<T>();
        currentState.BeginState();
    }

    public void RemoveState()
    {
        if(currentState != null)
        {
            currentState.EndState();
            Destroy(currentState);
        }
    }
}

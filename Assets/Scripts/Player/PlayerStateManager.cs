using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    PlayerState currentState;

    public void AddState<T>() where T : PlayerState
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
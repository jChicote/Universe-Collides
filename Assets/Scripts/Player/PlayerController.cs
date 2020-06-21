using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerStateManager playerState = null;

    void Awake() {
        if (playerState == null) playerState = this.gameObject.AddComponent<PlayerStateManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //SetState<XWingState>(); //TEMPORARY
    }

    public void SetState<T>() where T : PlayerState {
        playerState.AddState<T>();
    }

}

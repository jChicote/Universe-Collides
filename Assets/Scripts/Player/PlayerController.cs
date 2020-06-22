using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class PlayerController : MonoBehaviour
{
    public PlayerStateManager playerState = null;
    public Transform modelTransform;
    public CinemachineVirtualCamera virtualCamera;

    void Awake() {
        if (playerState == null) playerState = this.gameObject.AddComponent<PlayerStateManager>();
    }

    public void SetState<T>() where T : PlayerState {
        playerState.AddState<T>();
    }

}

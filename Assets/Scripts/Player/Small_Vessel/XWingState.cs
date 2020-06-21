using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class XWingState : PlayerState
{
    PlayerController playerController;
    
    bool isPaused = false;

    public override void BeginState()
    {
        playerController = this.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(isPaused) return;
        //Debug.Log("is running");
    }

    private void MouseAim(InputValue value) {

    }

    private void GamePadAim(InputValue value) {

    }

    private void KeyboardMovement(InputValue value) {

    }

    private void GamePadMovement(InputValue value1) {

    }

    public override void EndState() {

    }
}

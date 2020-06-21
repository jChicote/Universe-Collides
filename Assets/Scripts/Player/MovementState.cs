using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/*

THIS IS FOR EXAMPLE

*/

public class MovementState : PlayerState
{
    PlayerController playerController;
    public override void BeginState()
    {
        playerController = GameManager.instance.playerController;
    }

    void FixedUpdate(){
        Debug.Log("is working");
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

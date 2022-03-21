using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "New Input", menuName = "InputMgr")]
public class InputMgr : ScriptableObject
{
    // Start is called before the first frame update
    public Vector2 MoveDir { get; private set; }
    public Vector2 GravityDir { get; private set; }

    bool holding_trigger = false;
    public PlayerInput playerInput;

    public void Move(InputAction.CallbackContext context)
    {
        MoveDir = context.ReadValue<Vector2>();
    }

    public void ChangeGravityDirection(InputAction.CallbackContext context)
    {
        GravityDir = context.ReadValue<Vector2>();

        if (context.performed)
        {
            //tells the MovementHandler to change the direction of gravity
        }
    }

    public void L2Modifier(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            holding_trigger = true;
            AddActionMap();
        }

        else if (context.canceled)
        {
            holding_trigger = false;
            AddActionMap();
        }
    }

    void AddActionMap()
    {
        if (!holding_trigger)
        {
            playerInput.actions.FindActionMap("ChangeGravity").Disable();
            return;
        }

        playerInput.actions.FindActionMap("ChangeGravity").Enable();

    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && !holding_trigger)
        {
            //call another event that tells the animator to send trigger and the MovementHandler to actually jump 
        }
    }

}

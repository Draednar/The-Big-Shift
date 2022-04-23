using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputMgr : MonoBehaviour
{
    // Start is called before the first frame update
    public delegate void JumpDelegate();
    public event JumpDelegate JumpEvent;

    public delegate void GravityDirDelegate(Vector2 dir);
    public event GravityDirDelegate GravityEvent;

    public delegate void OpenMenu(bool value);
    public static event OpenMenu menu;

    public Vector2 MoveDir { get; private set; }
    public Vector2 GravityDir { get; private set; }

    bool holding_trigger = false;

    public PlayerInput playerInput;

    public bool menuOpen = false;

    public bool pressedFirstInput = false;

    bool addedActionMap = false;

   public float playTime { get; private set; }

    public void Move(InputAction.CallbackContext context)
    {
        MoveDir = context.ReadValue<Vector2>();

        if (MoveDir.normalized.magnitude > 0.1)
        {
            pressedFirstInput = true;
        }

    }

    public void ChangeGravityDirection(InputAction.CallbackContext context)
    {
        GravityDir = context.ReadValue<Vector2>();

        if (context.performed)
        {
            //tells the MovementHandler to change the direction of gravity
            GravityEvent.Invoke(GravityDir);
            pressedFirstInput = true;
        }
    }

    public void ChangeGravityUP(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            //tells the MovementHandler to change the direction of gravity
            pressedFirstInput = true;
            GravityEvent.Invoke(Vector2.up);
        }
    }

    public void ChangeGravityDOWN(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            //tells the MovementHandler to change the direction of gravity
            pressedFirstInput = true;
            GravityEvent.Invoke(Vector2.down);
        }
    }

    public void ChangeGravityLEFT(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            //tells the MovementHandler to change the direction of gravity
            pressedFirstInput = true;
            GravityEvent.Invoke(Vector2.left);
        }
    }

    public void ChangeGravityRIGHT(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            //tells the MovementHandler to change the direction of gravity
            pressedFirstInput = true;
            GravityEvent.Invoke(Vector2.right);
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
            addedActionMap = false;
            return;
        }

        if (!addedActionMap)
        {
            playerInput.actions.FindActionMap("ChangeGravity").Enable();
            addedActionMap = true;
        }

    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && !holding_trigger)
        {
            //call another event that tells the animator to send trigger and the MovementHandler to actually jump 
            pressedFirstInput = true;
            JumpEvent.Invoke();
        }
    }

    public void UIMenuOpen()
    {
        menuOpen = !menuOpen;
        menu.Invoke(menuOpen);
    }

    public void ResetBool()
    {
        menuOpen = false;
    }

}

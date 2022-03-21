using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputMgr : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector2 dir { get; private set; }
    bool holding_trigger = false;
    public PlayerInput playerInput;

    private void Update()
    {
        
    }

    public void Move(InputAction.CallbackContext context)
    {
        dir = context.ReadValue<Vector2>();

        Debug.Log($"X direction = {dir.x}");
    }

    public void ChangeGravityUP(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("UP");
        }
    }

    public void ChangeGravityDOWN(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("DOWN");
        }
    }

    public void ChangeGravityLEFT(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("LEFT");
        }
    }

    public void ChangeGravityRIGHT(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("RIGHT");
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
            Debug.Log("Jump");
        }
    }

}

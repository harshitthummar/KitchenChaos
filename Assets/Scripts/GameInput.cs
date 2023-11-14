using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameInput : MonoBehaviour
{
    public event EventHandler OnInteractAction;


    private PlayerInputActions playerInputAction;
    private void Awake()
    {
        playerInputAction = new PlayerInputActions();
        playerInputAction.Player.Enable();

        playerInputAction.Player.Interact.performed += Interact_performed;
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if(OnInteractAction != null)
        {
            //bcz if its null it will raise null reference exception
            OnInteractAction(this, EventArgs.Empty);
        }
        
    }

    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = playerInputAction.Player.Move.ReadValue<Vector2>();


        inputVector = inputVector.normalized;

        return inputVector;
    }
}

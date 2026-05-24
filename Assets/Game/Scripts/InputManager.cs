using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using static GameInputAction;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour, IPlayerActions
{
    private GameInputAction _inputAction;
    public UnityEvent<Vector2> OnMoveInput;

    private void Awake()
    {
        _inputAction = new GameInputAction();
        _inputAction.Enable();
        _inputAction.Player.Enable(); //mengaktifkan action map Player
        _inputAction.Player.SetCallbacks(this);
    }
    void OnMove(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            OnMoveInput?.Invoke(context.ReadValue<Vector2>());
            Debug.Log("Move: " + context.ReadValue<Vector2>());
        }
    }
    void OnInteract(InputAction.CallbackContext context)
    {
        Debug.Log("Interact");
    }

    //membuat Event OnMoveInput



    void IPlayerActions.OnMove(InputAction.CallbackContext context)
    {
        OnMove(context);
    }

    void IPlayerActions.OnInteract(InputAction.CallbackContext context)
    {
        OnInteract(context);
    }
}

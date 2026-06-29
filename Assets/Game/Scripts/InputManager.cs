using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using static GameInputAction;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour, IPlayerActions
{
    private GameInputAction _inputAction;
    public UnityEvent<Vector2> OnMoveInput;
    public UnityEvent<bool> OnSprintInput; //harus bool karena untuk menampung nilai true atau false ketika tombol ditekan atau dilepas
    public UnityEvent OnInteractInput;
    // Membuat event OnFlashlightInput
    public UnityEvent OnFlashlightInput;

    private void Awake()
    {
        _inputAction = new GameInputAction();
        _inputAction.Enable();
        _inputAction.Player.Enable(); //mengaktifkan action map Player
        _inputAction.Player.SetCallbacks(this);
    }
    //membuat Event OnMoveInput
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

    public void OnSprint(InputAction.CallbackContext context)
    {
        if (context.performed)//check apakah tombol shift ditekan
        {
            OnSprintInput?.Invoke(true); //kalau iya maka true
        }
        if (context.canceled)//check apakah tombol shift dilepas
        {
            OnSprintInput?.Invoke(false); //kalau iya dilepas maka false
        }
    }

    void IPlayerActions.OnMove(InputAction.CallbackContext context)
    {
        OnMove(context);
    }

    void IPlayerActions.OnInteract(InputAction.CallbackContext context)
    {
        // contect.performed digunakan untuk mengecek apakah input ditekan
        if (context.performed)
        {
            // Jika input ditekan maka trigger event OnInteractInput
            OnInteractInput?.Invoke();
        }
    }

    public void OnFlashlight(InputAction.CallbackContext context)
    {
        // contect.performed digunakan untuk mengecek apakah input ditekan
        if (context.performed)
        {
            // Jika input ditekan maka trigger event OnFlashlightInput
            OnFlashlightInput?.Invoke();
        }
    }
}

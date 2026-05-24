using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using static GameInputAction;

public class InputManager : MonoBehaviour
{
    private GameInputAction _inputAction;

    private void Awake()
    {
        _inputAction = new GameInputAction();
        _inputAction.Enable();
    }

    
}

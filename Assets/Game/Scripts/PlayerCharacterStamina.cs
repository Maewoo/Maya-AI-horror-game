using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using static GameInputAction;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class PlayerCharacterStamina : MonoBehaviour
{
    [SerializeField] private float _maxStamina = 100f;
    [SerializeField] private float _sprintStaminaCost = 20f;
    [SerializeField] private float _staminaRegenValue = 20f;
    private float _currentStamina;

    [SerializeField] private PlayerMovementCharacter _characterMovement;

    // public bool IsSprint => _isSprint;

    void Awake()
    {
        _currentStamina = _maxStamina;
    }

    void Update()
    {
        CalculateStamina();
    }
    public void CalculateStamina()
    {
        if (_characterMovement.IsSprint)
        {
            if (_currentStamina > 0)
            {
                _currentStamina = _currentStamina - _sprintStaminaCost * Time.deltaTime;
            }
            else
            {
                _characterMovement.SetSprint(false);
            }
        }
        else
        {
            _currentStamina = _currentStamina + _staminaRegenValue * Time.deltaTime;
        }
        _currentStamina = Mathf.Clamp(_currentStamina, 0, _maxStamina);

    }
}

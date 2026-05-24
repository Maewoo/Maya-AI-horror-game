using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using static GameInputAction;
using UnityEngine.Events;
using UnityEngine.InputSystem;
public class PlayerMovementCharacter : MonoBehaviour
{
    private Vector3 _movementDirection;
    private float _moveSpeed = 1f;
    private Vector3 _velocityXZ;
    [SerializeField]
    private CharacterController _characterController;

    void Update()
    {
        Move();
    }
    public void SetMoveDirection(Vector2 inputDirection)
    {
        // Mengisikan arah input sumbu x ke arah gerakan character sumbu x 
        // Mengisikan arah input sumbu y ke arah gerakan character sumbu z, karena pada umumnya sumbu y digunakan untuk gerakan vertikal (melompat) dalam game 3D, sedangkan sumbu z digunakan untuk gerakan maju/mundur.
        _movementDirection = new Vector3(inputDirection.x, 0, inputDirection.y);
    }

    public void Move()
    {
        if (_movementDirection.magnitude >= 0.01)
        {
            _velocityXZ = _movementDirection * _moveSpeed * Time.deltaTime;
        }
        else
        {
            _velocityXZ = Vector3.zero;
        }
    }

}

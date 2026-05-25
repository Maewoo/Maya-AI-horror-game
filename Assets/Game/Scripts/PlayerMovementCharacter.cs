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
    [SerializeField] float _walkSpeed = 1f;
    [SerializeField] float _sprintSpeed = 2f;
    private float _acceleration = 0.5f; //ada percepatan untuk berpindah dari walkspeed ke sprintspeed
    private Vector3 _velocityXZ;
    [SerializeField] private CharacterController _characterController;
    private float _velocityY;
    [SerializeField] float _gravityScale = 1f;
    private bool _isGrounded;
    private bool _isSprint;

    public bool IsSprint => _isSprint; //memberitahu untuk mengakses nilai _isSprint

    void Awake()
    {
        _moveSpeed = _walkSpeed;
    }
    void Update()
    {
        CheckIsGrounded();
        ResetVelocityY();
        CalculateAcceleration();
        Move();
    }
    public void SetMoveDirection(Vector2 inputDirection)
    {
        // Mengisikan arah input sumbu x ke arah gerakan character sumbu x 
        // Mengisikan arah input sumbu y ke arah gerakan character sumbu z, karena pada umumnya sumbu y digunakan untuk gerakan vertikal (melompat) dalam game 3D, sedangkan sumbu z digunakan untuk gerakan maju/mundur.
        _movementDirection = new Vector3(inputDirection.x, 0, inputDirection.y);
    }
    public void SetSprint(bool isSprint)
    {
        _isSprint = isSprint;
    }

    public void Move()
    {
        CalculateVelocityXYZ();
        CalculateVelocityY();
        Vector3 velocity = new Vector3(_velocityXZ.x, _velocityY, _velocityXZ.z);
        _characterController.Move(velocity);
    }
    private void CalculateAcceleration()
    {
        if (_movementDirection.magnitude >= 0.01) //check apakah player sedang bergerak atau tidak
        {
            if (_isSprint)
            {
                _moveSpeed = _moveSpeed + _acceleration * Time.deltaTime; //kalau iya maka moveSpeed akan bertambah dengan nilai percepatan dikalikan dengan waktu yang dibutuhkan untuk mencapai kecepatan sprint
            }
            else
            {
                _moveSpeed = _moveSpeed - _acceleration * Time.deltaTime; //kalau tidak maka moveSpeed akan berkurang dengan nilai percepatan dikalikan dengan waktu yang dibutuhkan untuk kembali ke kecepatan berjalan
            }
            _moveSpeed = Mathf.Clamp(_moveSpeed, _walkSpeed, _sprintSpeed); //membatasi nilai moveSpeed agar tidak melebihi kecepatan sprint dan tidak kurang dari kecepatan berjalan
        }
        else
        {
            _moveSpeed = _walkSpeed;
        }
    }

    void CalculateVelocityXYZ()
    {
        Transform cameraTransform = Camera.main.transform;
        Vector3 xDirection = _movementDirection.x * cameraTransform.right;
        Vector3 zDirection = _movementDirection.z * cameraTransform.forward;
        Vector3 direction = xDirection + zDirection;
        direction.y = 0;

        if (_movementDirection.magnitude >= 0.01)
        {
            _velocityXZ = _moveSpeed * direction.normalized * Time.deltaTime;
        }
        else
        {
            _velocityXZ = Vector3.zero;
        }
    }
    void CheckIsGrounded()
    {
        LayerMask groundLayer = LayerMask.GetMask("Ground");
        _isGrounded = Physics.CheckSphere(transform.position, 0.5f, groundLayer);//melakukan deteksi apakah ada object lain yang menembus lingkaran
    }

    void CalculateVelocityY()
    {
        _velocityY = _velocityY + Physics.gravity.y * _gravityScale * Time.deltaTime;
    }
    void ResetVelocityY()
    {
        if (_isGrounded == true && _velocityY < 0)
        {
            _velocityY = -2f;
        }
    }
}

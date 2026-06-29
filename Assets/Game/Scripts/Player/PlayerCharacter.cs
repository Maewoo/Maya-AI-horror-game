using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    // Variable untuk reference ke module PlayerCharacterMovement
    [SerializeField]
    private PlayerMovementCharacter _movement;
    // Variable untuk reference ke module PlayerCharacterStamina
    [SerializeField]
    private PlayerCharacterStamina _stamina;
    // Variable untuk reference ke module InventoryManager
    [SerializeField]
    private InventoryManager _inventory;
    [SerializeField]
    private InteractDetector _interactDetector;
    // Variable untuk reference ke module CameraManager
    [SerializeField]
    private CameraManager _camera;
    [SerializeField]
    private InputManager _input;
    // Variable untuk reference ke module Flashlight
    [SerializeField]
    private Flashlight _flashlight;

    // Property untuk mengakses variable _movement
    public PlayerMovementCharacter Movement => _movement;
    // Property untuk mengakses variable _stamina
    public PlayerCharacterStamina Stamina => _stamina;
    // Property untuk mengakses variable _inventory
    public InventoryManager Inventory => _inventory;
    // Property untuk mengakses variable _interactDetector
    public InteractDetector InteractDetector => _interactDetector;
    // Property untuk mengakses variable _camera
    public CameraManager Camera => _camera;
    // Property untuk mengakses variable _input

    // Property untuk menentukan apakah player sedang hiding
    public bool IsHiding { get; private set; }

    // Property untuk mengakses variable _flashlight
    public Flashlight Flashlight => _flashlight;
    public InputManager Input => _input;

    private void Awake()
    {
        // Ketika game dijalankan,
        // cursor mouse akan disembunyikan
        Cursor.visible = false;
        // cursor mouse akan dikunci di tengah layar
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void SetIsHiding(bool isHiding)
    {
        IsHiding = isHiding;
    }
}
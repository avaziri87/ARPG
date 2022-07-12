using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    XboxInputs _xboxInputs;

    [SerializeField] Vector2 _movementInput;
    bool _primaryAttackInput;

    public bool PrimaryAttackInput { get => _primaryAttackInput; }
    public static InputManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void OnEnable()
    {
        if(_xboxInputs == null)
        {
            _xboxInputs = new XboxInputs();
            _xboxInputs.PlayerMovement.Movement.performed += MovementInput;
            _xboxInputs.PlayerActions.PrimaryAttack.performed += FirePrimaryATK;
            _xboxInputs.PlayerActions.PrimaryAttack.canceled += CancelPrimaryATK;
        }

        _xboxInputs.Enable();
    }

    private void CancelPrimaryATK(InputAction.CallbackContext obj)
    {
        _primaryAttackInput = false;
    }

    private void FirePrimaryATK(InputAction.CallbackContext obj)
    {
        _primaryAttackInput = true;
    }

    private void MovementInput(InputAction.CallbackContext value)
    {
        _movementInput = value.ReadValue<Vector2>();
    }

    private void OnDisable()
    {
        _xboxInputs.Disable();
    }
    public float HorizontalInput()
    {
        return _movementInput.x;
    }
    public float VerticalInput()
    {
        return _movementInput.y;
    }
}

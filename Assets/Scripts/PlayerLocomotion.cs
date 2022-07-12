using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocomotion : MonoBehaviour
{
    InputManager _inputManager;
    Rigidbody _rigidbody;

    Vector3 _moveDirection;

    [SerializeField] Transform _cameraTransform;
    [SerializeField] float _movementSpeed = 7.0f;
    [SerializeField] float rotationSpeed = 15.0f;
    [SerializeField] float _attackSpeed = 1.0f;
    [SerializeField] Transform shootTransform;
    [SerializeField] GameObject projectile;
    [SerializeField] Transform targetAim;

    float _nextFire;
    EnemyLocomotion _currentTarget;

    public EnemyLocomotion CurrentTarget { get => _currentTarget; set => _currentTarget = value; }

    private void Awake()
    {
        _inputManager = InputManager.Instance;
        _rigidbody = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        HandleMovement();
        HandleRotation();

        if (_inputManager.PrimaryAttackInput) Shoot();
    }
    void HandleMovement()
    {
        _moveDirection = _cameraTransform.forward * _inputManager.VerticalInput();
        _moveDirection = _moveDirection + _cameraTransform.right * _inputManager.HorizontalInput();
        _moveDirection.Normalize();
        _moveDirection.y = 0;
        _moveDirection = _moveDirection * _movementSpeed;

        Vector3 movementVelocity = _moveDirection;
        _rigidbody.velocity = movementVelocity;
    }

    void HandleRotation()
    {
        if(_currentTarget != null)
        {
            transform.LookAt(_currentTarget.transform);
            return;
        }

        Vector3 targetDirection = Vector3.zero;
        targetDirection = _cameraTransform.forward * _inputManager.VerticalInput();
        targetDirection = targetDirection + _cameraTransform.right * _inputManager.HorizontalInput();
        targetDirection.Normalize();
        targetDirection.y = 0;

        if (targetDirection == Vector3.zero) targetDirection = transform.forward;
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        transform.rotation = playerRotation;
    }

    public void Shoot()
    {
        if (Time.time < _nextFire) return;

        _nextFire = Time.time + _attackSpeed;
        GameObject newProjectile = Instantiate(projectile, shootTransform.position, shootTransform.localRotation);
        if(_currentTarget != null)
        {
            newProjectile.transform.LookAt(_currentTarget.transform);
        }
        else
        {
            newProjectile.transform.LookAt(targetAim.transform);
        }
    }
}

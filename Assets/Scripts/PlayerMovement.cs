using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _speedMultiplier;
    [SerializeField] private float _rotateTime;
    [SerializeField] private float _jumpHeight;
    [SerializeField] private float _groundCheckRadius;
    [SerializeField] private LayerMask _whatIsGround;
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private Transform _cameraView;
    [SerializeField] private Transform _groundCheck;

    private float _currentSpeed;
    private float _smoothVelocity;
    private Vector3 _direction;
    private Vector3 _gravity;
    private Vector3 _velocity;
    private bool _isGrounded;

    private void Start()
    {
        _currentSpeed = _speed;
        _gravity = Physics.gravity;
    }

    private void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        _direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            _currentSpeed = _speed * _speedMultiplier;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            _currentSpeed = _speed;
        }

        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            Jump();
        }
    }

    private void FixedUpdate()
    {
        if (_direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(_direction.x, _direction.z) * Mathf.Rad2Deg + _cameraView.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _smoothVelocity, _rotateTime);
            transform.rotation = Quaternion.Euler(0, angle, 0);

            Vector3 moveDirection = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
            _characterController.Move(moveDirection * _currentSpeed * Time.fixedDeltaTime);
        }

        _isGrounded = GroundCheck();
        if (_isGrounded && _velocity.y < 0)
        {
            _velocity.y = 0f;
        }

        _velocity.y += _gravity.y * Time.deltaTime;
        _characterController.Move(_velocity * Time.deltaTime);
    }

    private bool GroundCheck()
    {
        return Physics.CheckSphere(_groundCheck.position, _groundCheckRadius, _whatIsGround,
            QueryTriggerInteraction.Ignore);
    }

    private void Jump()
    {
        _velocity.y += Mathf.Sqrt(_jumpHeight * -2f * _gravity.y);
    }
}
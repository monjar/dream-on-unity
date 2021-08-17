using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private Vector2 _moveVelocity = Vector2.zero;
    private bool _isRunning = false;
    private bool _isJumping = false;
    private bool _isFalling = false;
    private bool _isRunningInput = false;

    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Animator _animator;
    [SerializeField] private float _horizontalMoveSpeed = 10f;
    [SerializeField] private float _jumpPower = 10f;
    [SerializeField] private float _jumpHorizontalSpeedCoef = 0.5f;
    [SerializeField] private float _fallDistanceThreshold = 0.5f;

    [SerializeField] private LayerMask groundLayerMask;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void SetHorizontalVelocity(float velocity)
    {
        _moveVelocity.x = velocity * _horizontalMoveSpeed;
        _isRunningInput = Mathf.Abs(velocity) > 0.3f;
    }

    public void Jump()
    {
        if (IsGrounded())
        {
            _rigidbody.AddForce(new Vector2(0, _jumpPower), ForceMode2D.Impulse);
            _animator.SetBool("IsJumping", true);
            _animator.SetBool("DidLand", false);
            _isJumping = true;
        }
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = new Vector2(_moveVelocity.x, _rigidbody.velocity.y);
    }

    private void Update()
    {
        float horizontalMag = Mathf.Abs(_rigidbody.velocity.x);
        if (_isRunningInput && !_isRunning)
        {
            _animator.SetBool("IsRunning", true);
            _isRunning = true;
        }
        else if (!_isRunningInput && _isRunning)
        {
            _animator.SetBool("IsRunning", false);
            _isRunning = false;
        }

        if (_isRunningInput)
            _spriteRenderer.flipX = _rigidbody.velocity.x < 0;

        float verticalVelocity = _rigidbody.velocity.y;
        if (Mathf.Abs(verticalVelocity) < 0.1f && (_isJumping || _isFalling))
        {
            if (IsGrounded())
            {
                
                _animator.SetBool("IsJumping", false);
                _animator.SetBool("IsFalling", false);
                _animator.SetBool("DidLand", true);
                _isJumping = false;
                _isFalling = false;
            }
        }
        else if (verticalVelocity < -0.1f && !_isFalling)
        {
            _animator.SetBool("IsJumping", false);
            _animator.SetBool("IsFalling", true);
            _animator.SetBool("DidLand", false);
            _isJumping = false;
            _isFalling = true;
        }
    }

    private bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, _fallDistanceThreshold, groundLayerMask);

        if (hit.collider)
        {
            return true;
        }

        return false;
    }
}
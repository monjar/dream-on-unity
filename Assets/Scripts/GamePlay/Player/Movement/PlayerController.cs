using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private Vector2 _moveVelocity = Vector2.zero;
    private Animator _animator;
    private bool _isRunning = false;

    [SerializeField] private float _horizontalMoveSpeed = 10f;
    [SerializeField] private float _jumpPower = 10f;
    [SerializeField] private float _jumpHorizontalSpeedCoef = 0.5f;
    [SerializeField] private float _fallDistanceThreshold = 0.5f;

    [SerializeField] private LayerMask groundLayerMask;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    public void SetHorizontalVelocity(float velocity)
    {
        _moveVelocity.x = velocity * _horizontalMoveSpeed;
    }

    public void Jump()
    {
        if (IsGrounded())
        {
            _rigidbody.AddForce(new Vector2(0, _jumpPower), ForceMode2D.Impulse);
        }
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = new Vector2(_moveVelocity.x, _rigidbody.velocity.y);
        
    }

    private void Update()
    {
        float horizontalMag = Mathf.Abs(_rigidbody.velocity.x);
        if (horizontalMag > 0.01f && !_isRunning)
        {
            _animator.SetBool("IsRunning", true);
            _isRunning = true;
        }
        else if (horizontalMag <= 0.01f && _isRunning)
        {
            _animator.SetBool("IsRunning", false);
            _isRunning = false;
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
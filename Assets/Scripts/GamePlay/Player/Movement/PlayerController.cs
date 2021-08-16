using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private Vector2 _moveVelocity = Vector2.zero;

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
        _rigidbody.velocity = new Vector2(
            _moveVelocity.x * (IsGrounded() ? 1 : _jumpHorizontalSpeedCoef),
            _rigidbody.velocity.y
        );
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
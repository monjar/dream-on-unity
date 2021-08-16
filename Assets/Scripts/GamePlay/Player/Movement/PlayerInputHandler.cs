using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    private PlayerController _playerController;

    private void Start()
    {
        _playerController = GetComponent<PlayerController>();
    }

    void Update()
    {
        var horizontalAxis = Input.GetAxis("Horizontal");
        _playerController.SetHorizontalVelocity(horizontalAxis);

        if (Input.GetButtonDown("Jump"))
        {
            _playerController.Jump();
        }
    }
}
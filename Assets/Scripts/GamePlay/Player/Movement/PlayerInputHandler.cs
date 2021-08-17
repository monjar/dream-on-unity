using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    private PlayerController _playerController;
    private Rewindable _rewindable;

    private void Start()
    {
        _playerController = GetComponent<PlayerController>();
        _rewindable = GetComponent<Rewindable>();
    }

    void Update()
    {
        var horizontalAxis = Input.GetAxis("Horizontal");
        _playerController.SetHorizontalVelocity(horizontalAxis);

        if (Input.GetButtonDown("Jump"))
        {
            _playerController.Jump();
        }
        if(Input.GetButtonDown("Fire") && _rewindable.Status == RewindStatus.NONE)
            _rewindable.StartRecording();
        else if(Input.GetButtonDown("Fire") && _rewindable.Status == RewindStatus.RECORDING)
        {
            _rewindable.StartRewinding();
        }else if(Input.GetButtonDown("Fire") && _rewindable.Status == RewindStatus.REWINDING)
        {
            _rewindable.Resume();
        }
    }
}
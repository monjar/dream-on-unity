using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Rewindable : MonoBehaviour
{
    private RewindStatus _status = RewindStatus.NONE;
    private Stack<Vector2> _positionRecordings = new Stack<Vector2>();


    public RewindStatus Status => _status;

    public void StartRecording()
    {
        _status = RewindStatus.RECORDING;
    }

    public void StartRewinding()
    {
        _status = RewindStatus.REWINDING;
    }

    public void Resume()
    {
        _status = RewindStatus.NONE;
    }

    private void FixedUpdate()
    {
        switch (_status)
        {
            case RewindStatus.RECORDING:
                _positionRecordings.Push(transform.position);
                break;
            case RewindStatus.REWINDING:
                if (_positionRecordings.Count > 0)
                    transform.position = _positionRecordings.Pop();
                else
                    _status = RewindStatus.NONE;
                break;
        }
    }
}

public enum RewindStatus
{
    RECORDING,
    REWINDING,
    NONE
}
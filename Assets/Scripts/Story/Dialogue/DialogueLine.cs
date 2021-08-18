using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DialogueLine
{
    private string from;
    [SerializeField] private string body;
    private float waitSecondsAfterType = 3;


    public float WaitSecondsAfterType => waitSecondsAfterType;
    public DialogueLine(string from, string body)
    {
        this.from = from;
        this.body = body;
    }
    
    public string Body => body;

    public string From => from;
}
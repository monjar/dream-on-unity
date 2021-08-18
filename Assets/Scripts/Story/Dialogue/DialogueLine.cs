using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DialogueLine
{
    [SerializeField] private string from;
    [SerializeField] private string body;
    [SerializeField] private float waitSecondsAfterType = 3;
    [SerializeField] private DialogueLine next = null;


    public float WaitSecondsAfterType => waitSecondsAfterType;
    public DialogueLine(string from, string body)
    {
        this.from = from;
        this.body = body;
    }
    public DialogueLine(string from, string body, DialogueLine next)
    {
        this.from = from;
        this.body = body;
        this.next = next;
    }
    public string Body => body;

    public string From => from;
}
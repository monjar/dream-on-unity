using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DialogueResponse
{
    [SerializeField] private DialogueLine line;
    [SerializeField] private int nextQuestionIndex;


    public int NextQuestionIndex => nextQuestionIndex;
}

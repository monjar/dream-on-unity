using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DialogueNode
{
    [SerializeField] private List<DialogueLine> lines;

    [SerializeField] private List<DialogueResponse> responses;

    [SerializeField] private List<DialogueNode> nexts;


    public List<DialogueLine> Lines => lines;

    public List<DialogueResponse> Responses => responses;
    public DialogueNode AnswerNode(int responseIndex)
    {
        var selectedResponse = responses[responseIndex];
        var nextQuestion = nexts[selectedResponse.NextQuestionIndex];
        return nextQuestion;
    }
    
}
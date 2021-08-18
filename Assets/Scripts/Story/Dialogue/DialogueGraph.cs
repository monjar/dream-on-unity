using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueGraph : MonoBehaviour
{
    [SerializeField]
    private DialogueNode root;
    private DialogueNode currentNode;
    private DialogueBox box;


    private void Start()
    {
        currentNode = root;
        box = GetComponent<DialogueBox>();
    }

   
    public List<DialogueLine> GetCurrentLines()
    {
        return currentNode.Lines;
    }

    public List<DialogueResponse> GetCurrentResponses()
    {
        return currentNode.Responses;
    }

    public void Answer(int answerIndex)
    {
        var dialogueNode = currentNode.AnswerNode(answerIndex);
        this.currentNode = dialogueNode;
        box.ResetIndex();
        box.Show();

    }
    
    
    
}

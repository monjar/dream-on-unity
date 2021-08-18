using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PlayerDialogueBox : MonoBehaviour
{
    [SerializeField] private GameObject dialogueBoxPrefab;
    [SerializeField] private Transform verticalLayoutParent;


    public void ShowAnswers(DialogueGraph graph)
    {
        HideAnswers();
        var responses = graph.GetCurrentResponses();
        for (int i = 0; i < responses.Count; i++)
        {
            var go = Instantiate(dialogueBoxPrefab, verticalLayoutParent);
            var iCopy = i;
            go.GetComponent<DialogueResponseBox>().SetResponse(responses[i].Line.Body, () =>
            {
                graph.Answer(iCopy);
                HideAnswers();
            });
        }
    }

    public void HideAnswers()
    {
        for (int i = 0; i < verticalLayoutParent.childCount; i++)
        {
            var child = verticalLayoutParent.GetChild(i);
            child.gameObject.SetActive(false);
        }
    }
}
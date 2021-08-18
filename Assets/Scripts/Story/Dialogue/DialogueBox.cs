using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueBox : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMesh;
    private List<DialogueLine> _dialogueLines = new List<DialogueLine>();
    private int nextDialogueIndex = 0;

    public void PushLine(DialogueLine line)
    {
        _dialogueLines.Add(line);
    }

    public void ClearLines()
    {
        _dialogueLines.Clear();
        nextDialogueIndex = 0;
    }

    private DialogueLine GetNextLine()
    {
        var nextLine =
            nextDialogueIndex == _dialogueLines.Count ? null : _dialogueLines[nextDialogueIndex];
        nextDialogueIndex++;
        return nextLine;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            var dialogue = new DialogueLine("me", _dialogueLines.Count + " Hello There Fellow Traveler");
            PushLine(dialogue);
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            Show();
        }
    }

    private IEnumerator currentTypeRoutine;

    public void Show()
    {
        if (currentTypeRoutine != null)
            StopCoroutine(currentTypeRoutine);
        var line = GetNextLine();
        if (line == null)
            Close();
        else
        {
            currentTypeRoutine = TypeDialogue(line);
            StartCoroutine(currentTypeRoutine);
        }
    }

    public void Close()
    {
        StopAllCoroutines();
        textMesh.text = "";
        nextDialogueIndex = 0;
    }

    private IEnumerator TypeDialogue(DialogueLine line)
    {
        textMesh.text = "";
        foreach (var character in line.Body.ToCharArray())
        {
            textMesh.text += character;
            yield return new WaitForSeconds(0.05f);
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueBox : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMesh;
    private DialogueGraph graph;

    private int nextDialogueIndex = 0;

    private void Start()
    {
        graph = GetComponent<DialogueGraph>();
    }

    private DialogueLine GetNextLine()
    {
        var nextLine =
            nextDialogueIndex == graph.GetCurrentLines().Count ? null : graph.GetCurrentLines()[nextDialogueIndex];
        nextDialogueIndex++;
        return nextLine;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            graph.Answer(0);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            graph.Answer(1);
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
        {
            ResetIndex();
            GetNextLine();
        }
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
    
    public void ResetIndex()
    {
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


    private void OnMouseDown()
    {
        Show();
    }
}
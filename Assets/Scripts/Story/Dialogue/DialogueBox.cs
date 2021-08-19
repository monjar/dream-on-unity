using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueBox : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMesh;
    private DialogueGraph graph;
    private Animator animator;
    private int nextDialogueIndex = 0;
    private void Start()
    {
        graph = GetComponent<DialogueGraph>();
        animator = GetComponent<Animator>();
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
            OpenDialogue();
        }
    }

    public void OpenDialogue()
    {
        StartCoroutine(OpenDialogueAsync());
    }

    private IEnumerator OpenDialogueAsync()
    {
        animator.SetTrigger("Open");
        yield return new WaitForSeconds(0.5f);
        Show();
    }

    private IEnumerator currentTypeRoutine;

    public void Show(float closeDelay = 0f)
    {
        if (currentTypeRoutine != null)
        {
            StopCoroutine(currentTypeRoutine);
            textMesh.text = currentLine.Body;
        }

        var line = GetNextLine();
        if (line == null)
        {
            if (graph.IsFinalNode())
            {
                StartCoroutine(CloseAfter(closeDelay));
            }
            else
            {
                var playerAnswer = FindObjectOfType<PlayerDialogueBox>();
                playerAnswer.ShowAnswers(graph);
                ResetIndex();
                GetNextLine();
            }
          
           
        }
        else
        {
            currentTypeRoutine = TypeDialogue(line);
            StartCoroutine(currentTypeRoutine);
        }
    }

    public IEnumerator CloseAfter(float delay = 0)
    {
        yield return new WaitForSeconds(delay);
        StopAllCoroutines();
        textMesh.text = "";
        nextDialogueIndex = 0;
        graph.ResetGraph();
        animator.SetTrigger("Close");
    }

    public void ResetIndex()
    {
        nextDialogueIndex = 0;
    }

    private DialogueLine currentLine;

    private IEnumerator TypeDialogue(DialogueLine line)
    {
        currentLine = line;
        textMesh.text = "";
        foreach (var character in line.Body.ToCharArray())
        {
            textMesh.text += character;
            yield return new WaitForSeconds(0.05f);
        }

        yield return new WaitForSeconds(0.2f);
        Show(1f);
    }


    private void OnMouseDown()
    {
        Show();
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueResponseBox : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    private Action onSelect;

    public void SetResponse(string body, Action onSelect)
    {
        text.text = body;
        this.onSelect = onSelect;
    }

    public void Select()
    {
        print("AAAAAAAa");
        onSelect?.Invoke();
    }

}

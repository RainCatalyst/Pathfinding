using System;
using UnityEngine;
using UnityEngine.Events;

public class WordInput : MonoBehaviour
{
    public string Word
    {
        get => _word;
        set
        {
            _word = value;
            _activeGroup.SetWord(_word);
        }
    }

    public LetterGroup ActiveGroup
    {
        get => _activeGroup;
        set
        {
            _activeGroup = value;
            Word = "";
        }
    }

    public bool IsInputAllowed { get; set; }

    public bool CanInput => Word.Length < _activeGroup.Size;
    public bool CanConfirm => Word.Length == _activeGroup.Size;
    
    public UnityAction Confirmed;

    public void InputLetter(char letter)
    {
        if (!IsInputAllowed)
            return;
        Word += Char.ToLower(letter);
    }

    public void RemoveLetter()
    {
        if (!IsInputAllowed)
            return;
        // If word is > 0 symbols
        if (Word.Length != 0)
        {
            Word = Word.Substring(0, Word.Length - 1);
        }
    }

    public void Confirm()
    {
        if (!IsInputAllowed)
            return;
        Confirmed?.Invoke();
    }

    private void Update()
    {
        if (!IsInputAllowed || !ActiveGroup)
            return;
        
        foreach (char c in Input.inputString)
        {
            if (c == '\b') // backspace/delete
            {
                RemoveLetter();
            }
            else if (c == '\n' || c == '\r') // enter/return
            {
                if (CanConfirm)
                    Confirm();
            }
            else if (CanInput)
            {
                if (Char.IsLetter(c))
                    InputLetter(c);
            }
        }
    }

    private string _word = "";
    private LetterGroup _activeGroup;
}

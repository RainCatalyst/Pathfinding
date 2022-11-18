using System;
using System.Collections;
using UnityEngine;

public class LetterGroup : MonoBehaviour
{
    public int Size => _letters.Length;
    
    public void SetLetter(int index, string letter) => _letters[index].Text = letter;

    public void SetWord(string word)
    {
        for (int i = 0; i < Size; i++)
        {
            SetLetter(i, i < word.Length ? word[i].ToString() : String.Empty);
        }
    }

    public void HighlightLetter(int index, LetterState state) => _letters[index].State = state;

    public void Highlight(LetterState[] states)
    {
        for (int i = 0; i < states.Length; i++)
        {
            HighlightLetter(i, states[i]);
        }
    }

    public void Clear()
    {
        foreach (var letter in _letters)
            letter.Clear();
    }

    public void ClearHighlight()
    {
        foreach (var letter in _letters)
            letter.State = LetterState.Empty;
    }

    public void Shake()
    {
        foreach (var letter in _letters)
            letter.Animations.Shake();
    }

    private void Start()
    {
        for (int i = 0; i < _letters.Length; i++)
            _letters[i].GroupIndex = i;
        Clear();
        ClearHighlight();
    }

    [SerializeField] private Letter[] _letters;
    [Header("Animation")]
    [SerializeField] private GameObject _items;

    private Coroutine _coShake;
}
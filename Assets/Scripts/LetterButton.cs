using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LetterButton : MonoBehaviour
{
    public UnityAction Pressed;
    public UnityAction<bool> Hovered;
    public Letter Letter => _letter;

    private void OnMouseDown()
    {
        Pressed?.Invoke();
        _letter.Animations.SetSelected(false);
    }

    private void OnMouseEnter()
    {
        Hovered?.Invoke(true);
        _letter.Animations.SetSelected(true);
    }
    
    private void OnMouseExit()
    {
        Hovered?.Invoke(false);
        _letter.Animations.SetSelected(false);
    }

    private void Awake()
    {
        _letter = GetComponent<Letter>();
    }

    private Letter _letter;
}

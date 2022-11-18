using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Keyboard : MonoBehaviour
{
    public UnityAction<char> KeyPressed;
    public UnityAction Remove;
    public UnityAction Confirm;

    public bool IsInputAllowed
    {
        get => _inputAllowed;
        set
        {
            _inputAllowed = value;
            // foreach (var btn in _buttons)
            // {
            //     btn.GetComponent<Button>().interactable = value;
            // }
        }
    }
    
    public void Clear()
    {
        foreach (var btn in _buttons)
        {
            btn.Letter.State = LetterState.Normal;
        }
    }

    public void Highlight(char c, LetterState state)
    {
        var button = _buttons[_buttonMap[c]];
        if (button.Letter.State == LetterState.Normal)
            button.Letter.State = state;
    }

    private void Start()
    {
        _buttonMap = new();
        _confirmButton.Pressed += () => Confirm?.Invoke();
        _removeButton.Pressed += () => Remove?.Invoke();
        _confirmButton.Letter.Animations.FallInstant();
        _removeButton.Letter.Animations.FallInstant();
        
        for (int i = 0; i < _buttons.Length; i++)
        {
            char c = _layoutString[i];
            _buttons[i].Letter.Text = Char.ToUpper(c).ToString();
            _buttons[i].Letter.Animations.FallInstant();
            _buttons[i].Pressed += () => KeyPressed?.Invoke(c);
            _buttonMap.Add(c, i);
        }
    }

    [SerializeField] private LetterButton[] _buttons;
    [SerializeField] private LetterButton _confirmButton;
    [SerializeField] private LetterButton _removeButton;

    private bool _inputAllowed;
    private Dictionary<char, int> _buttonMap;
    private const string _layoutString = "qwertyuiopasdfghjklzxcvbnm";
}

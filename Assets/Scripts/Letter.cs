using System;
using TMPro;
using UnityEngine;
using UnityEngine.VFX;

[RequireComponent(typeof(LetterAnimations))]
public class Letter : MonoBehaviour
{
    public LetterState State
    {
        get => _state;
        set
        {
            Color currentColor = GameManager.Instance.Colors.GetColor(_state);
            _state = value;
            _animations.BlendColor(currentColor,
                GameManager.Instance.Colors.GetColor(_state));
        }
    }

    public string Text
    {
        get => _text.text;
        set
        {
            if (_state == LetterState.Empty && !String.IsNullOrEmpty(value))
            {
                State = LetterState.Normal;
                _animations.Fall();
                _text.text = value.ToUpper();
            }
            else if (_state == LetterState.Normal && String.IsNullOrEmpty(value))
            {
                State = LetterState.Empty;
                _animations.Rise();
            }
            else
            {
                _text.text = value.ToUpper();
            }
        }
    }

    public int GroupIndex
    {
        get => _groupIndex;
        set
        {
            _groupIndex = value;
            _animations.Delay = value * 0.03f;
        }
    }
    public LetterAnimations Animations => _animations;

    private void Awake()
    {
        _animations = GetComponent<LetterAnimations>();
    }

    public void Clear()
    {
        Text = String.Empty;
        _animations.Hide();
    }
    
    [SerializeField] private TMP_Text _text;

    private LetterState _state;
    private LetterAnimations _animations;
    private int _groupIndex;
}

using System;
using UnityEngine;

[Serializable]
public struct LetterColorData
{
    public Color GetColor(LetterState state) => state switch
    {
        LetterState.Empty => _emptyColor,
        LetterState.Normal => _normalColor,
        LetterState.Solved => _solvedColor,
        LetterState.WrongPlace => _wrongPlaceColor,
        LetterState.Unused => _unusedColor,
        _ => throw new ArgumentOutOfRangeException()
    };

    public Color _emptyColor;
    public Color _normalColor;
    public Color _solvedColor;
    public Color _wrongPlaceColor;
    public Color _unusedColor;
}

public enum LetterState
{
    Empty,
    Normal,
    Solved,
    WrongPlace,
    Unused
}
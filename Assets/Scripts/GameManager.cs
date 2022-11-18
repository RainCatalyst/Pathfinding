using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoSingleton<GameManager>
{
    public LetterColorData Colors => _colors;

    public string GetRandomWord() => _usedWords[Random.Range(0, _usedWords.Length)];
    public bool IsWordValid(string word) => _allWords.Contains(word);

    protected override void Awake()
    {
        base.Awake();
        LoadWords();
    }

    private void LoadWords()
    {
        _usedWords = _usedWordsAsset.text.Split(new [] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
        _allWords = new HashSet<string>(_allWordsAsset.text.Split(new [] { "\r\n", "\r", "\n" }, StringSplitOptions.None)
            .Concat(_usedWords));
        print($"Loaded {_allWords.Count} words.");
    }

    [SerializeField] private LetterColorData _colors;
    [SerializeField] private TextAsset _allWordsAsset;
    [SerializeField] private TextAsset _usedWordsAsset;

    
    private HashSet<string> _allWords;
    private string[] _usedWords;
}

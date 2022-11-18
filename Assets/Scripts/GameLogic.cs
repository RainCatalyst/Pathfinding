using System;
using System.Linq;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    public void Restart() => StartGame();
    
    private void Awake()
    {
        _input.Confirmed += OnConfirmed;
        _keyboard.KeyPressed += c => _input.InputLetter(c);
        _keyboard.Confirm += _input.Confirm;
        _keyboard.Remove += _input.RemoveLetter;
    }

    private void Start()
    {
        StartGame();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            StartGame();
    }

    private void StartGame()
    {
        _gameOver.Hide();
        // Clear all letters
        foreach (var group in _groups)
        {
            group.Clear();
            group.ClearHighlight();
        }
        _keyboard.Clear();
        
        // Get secret word
        _secretWord = GameManager.Instance.GetRandomWord();
        _attempt = 0;
        
        // Initialize input
        _input.IsInputAllowed = true;
        _input.ActiveGroup = _groups[_attempt];

        // Initialize keyboard
        _keyboard.IsInputAllowed = true;
        
        print($"Word: {_secretWord}");
    }

    private void FinishGame(bool win)
    {
        _input.IsInputAllowed = false;
        _keyboard.IsInputAllowed = false;
        
        // _gameOver.Show(win, _attempt, _secretWord);
        print(win ? "You won!" : "You lost!");
    }

    private void OnConfirmed()
    {
        if (GameManager.Instance.IsWordValid(_input.Word))
        {
            // Highlight the word
            var states = Utilities.CompareWords(_input.Word, _secretWord);
            
            // Highlight keyboard
            for (int i = 0; i < states.Length; i++)
            {
                _keyboard.Highlight(_input.Word[i], states[i]);
            }
            
            _groups[_attempt].Highlight(states);
            
            // If we guessed the word correctly
            if (states.All(x => x == LetterState.Solved))
            {
                FinishGame(true);
                return;
            }

            _attempt++;
            // If we ran out of guesses
            if (_attempt >= _groups.Length)
            {
                FinishGame(false);
                return;
            }
            
            // Otherwise, continue the game
            _input.ActiveGroup = _groups[_attempt];
        }
        else
        {
            print("Invalid!");
            // Shake or something
            _input.ActiveGroup.Shake();
        }
    }

    private string _secretWord;
    private int _attempt;
    
    [SerializeField] private WordInput _input;
    [SerializeField] private LetterGroup[] _groups;
    [SerializeField] private Keyboard _keyboard;
    [SerializeField] private GameOver _gameOver;
}

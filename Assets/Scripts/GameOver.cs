using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public void Show(bool win, int attempts, string word)
    {
        gameObject.SetActive(true);
        // Effects.Instance.Fade(_background, _fadeOptions);
        
        _secretText.text = word.ToUpper();
        _attemptsText.text = $"Attempt: {attempts}";
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    // [SerializeField] private Effects.FadeOptions _fadeOptions;
    [SerializeField] private Image _background;
    [SerializeField] private TMP_Text _secretText;
    [SerializeField] private TMP_Text _attemptsText;
}

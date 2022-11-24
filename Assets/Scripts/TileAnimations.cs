using TMPro;
using UnityEngine;
using UnityEngine.VFX;

public class TileAnimations : MonoBehaviour
{
    public void Hide()
    {
        _content.SetActive(false);
    }
    
    public void Shake()
    {
        LeanTween.cancel(_content);
        float strength = UnityEngine.Random.Range(4.5f, 7f);
        LTDescr shakeTween = LeanTween.rotateAroundLocal(_content, Vector3.up, strength, 0.125f)
            .setEase( LeanTweenType.easeShake ) // this is a special ease that is good for shaking
            .setLoopClamp()
            .setRepeat(-1);

        // Slow the camera shake down to zero
        LeanTween.value(gameObject, strength, 0f, 0.25f).setOnUpdate( 
            val => {
                shakeTween.setTo(Vector3.right * val);
            }
        ).setEase(LeanTweenType.easeOutQuad);
    }

    public void SetSelected(bool enabled)
    {
        LeanTween.cancel(_content);
        if (enabled)
        {
            _content.transform.localPosition = new Vector3(0, 0f, 0);
            LeanTween.moveLocalY(_content, 0.07f, 0.065f).setEaseOutCubic();
        }
        else
        {
            _content.transform.localPosition = new Vector3(0, 0.07f, 0);
            LeanTween.moveLocalY(_content, 0f, 0.065f).setEaseOutCubic();
        }
    }

    public void BlendColor(Color start, Color end)
    {
        LeanTween.cancel(gameObject);
        _backgroundImage.material.SetFloat("_Cutoff", -1f);
        _backgroundImage.material.SetColor("_Color1", start);
        _backgroundImage.material.SetColor("_Color2", end);
        LeanTween.value(gameObject, -1f, 1f, 0.35f).setOnUpdate(UpdateColorTransition);
        // LeanTween.color(_backgroundImage, GameManager.Instance.Colors.GetColor(_state), 0.125f).setDelay(GroupIndex * 0.03f);
        // _backgroundImage.material.color = GameManager.Instance.Colors.GetColor(_state);
    }

    public void FadeText(float delay)
    {
        LeanTween.cancel(_text.gameObject);
        // _text.transform.localScale = Vector3.zero;
        // LeanTween.scale(_text, Vector3.one * 0.1f, 0.25f).setEaseInQuad().setDelay(delay);
        _text.color = new Color(0, 0, 0, 0);
        LeanTween.value(_text.gameObject, 0, 0.95f, 0.25f).setOnUpdate( 
            val =>
            {
                _text.color = new Color(0, 0, 0, val);
            }
        ).setEase(LeanTweenType.easeInQuad).setDelay(delay);;
        // _text.gameObject.LeanColor(new Color(0, 0, 0, 0.9f), 0.25f).setEaseInQuad().setDelay(delay);
    }

    private void UpdateColorTransition(float value)
    {
        _backgroundImage.material.SetFloat("_Cutoff", value);
    }
    
    [SerializeField] private GameObject _content;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private MeshRenderer _backgroundImage;
}
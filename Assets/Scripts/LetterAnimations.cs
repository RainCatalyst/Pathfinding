using UnityEngine;
using UnityEngine.VFX;

public class LetterAnimations : MonoBehaviour
{
    public float Delay { get; set; }
    
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

    public void FallInstant()
    {
        LeanTween.cancel(_content);
        _content.SetActive(true);
        _content.transform.localEulerAngles = Vector3.zero;
        _content.transform.localPosition = new Vector3(0, 0f, 0);
    }

    public void Fall()
    {
        LeanTween.cancel(_content);
        _content.SetActive(false);
        _content.transform.localRotation = Quaternion.Euler(UnityEngine.Random.Range(-10f, 10f),
            UnityEngine.Random.Range(-10f, 10f), UnityEngine.Random.Range(-10f, 10f));
        _content.transform.localPosition = new Vector3(0, 3f, 0);
        // LeanTween.delayedCall(_content, 0.075f, () => Effects.Instance.ShakeCamera());
        LeanTween.delayedCall(_content, 0.05f, () => _content.SetActive(true));
        LeanTween.rotateLocal(_content, Vector3.zero, 0.125f).setEaseInCubic();
        LeanTween.moveLocalY(_content, 0f, 0.21f).setEase(_fallCurve)
            .setOnComplete(() =>
            {
                Effects.Instance.ShakeCamera();
                _effect.Play();
            });
    }

    public void Rise()
    {
        LeanTween.cancel(_content);
        _content.SetActive(true);
        _content.transform.localEulerAngles = Vector3.zero;
        _content.transform.localPosition = new Vector3(0, 0f, 0);
        LeanTween.delayedCall(_content, 0.075f, () => _content.SetActive(false));
        LeanTween.rotateLocal(_content, new Vector3(UnityEngine.Random.Range(-10f, 10f),
            UnityEngine.Random.Range(-10f, 10f), UnityEngine.Random.Range(-10f, 10f)), 0.125f).setEaseInCubic();
        LeanTween.moveLocalY(_content, 3f, 0.125f).setEaseOutCubic();
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
        _backgroundImage.material.SetFloat("_Cutoff", -1f);
        _backgroundImage.material.SetColor("_Color1", start);
        _backgroundImage.material.SetColor("_Color2", end);
        LeanTween.value(gameObject, -1f, 1f, 0.35f).setOnUpdate(UpdateColorTransition);
        // LeanTween.color(_backgroundImage, GameManager.Instance.Colors.GetColor(_state), 0.125f).setDelay(GroupIndex * 0.03f);
        // _backgroundImage.material.color = GameManager.Instance.Colors.GetColor(_state);
    }

    private void UpdateColorTransition(float value)
    {
        _backgroundImage.material.SetFloat("_Cutoff", value);
    }
    
    [SerializeField] private GameObject _content;
    [SerializeField] private MeshRenderer _backgroundImage;
    [SerializeField] private VisualEffect _effect;
    [SerializeField] private AnimationCurve _fallCurve;
}
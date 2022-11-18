using System;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public void Shake()
    {
        LeanTween.cancel(gameObject);
        LTDescr shakeTween = LeanTween.rotateAroundLocal(gameObject, Vector3.right, _strength, _speed)
            .setEase( LeanTweenType.easeShake ) // this is a special ease that is good for shaking
            .setLoopClamp()
            .setRepeat(-1);

        // Slow the camera shake down to zero
        LeanTween.value(gameObject, _strength, 0f, _duration).setOnUpdate( 
            val => {
                shakeTween.setTo(Vector3.right * val);
            }
        ).setEase(LeanTweenType.easeOutQuad);
    }

    [SerializeField] private float _strength;
    [SerializeField] private float _speed;
    [SerializeField] private float _duration;
}
using UnityEngine;
using DG.Tweening;
public class DOTweenController : MonoBehaviour
{
    [SerializeField] private float duration, strength, vibrato, randomness;
    [SerializeField] private Ease ease;
    public void GetShakeRotation(Transform shakeObject,System.Action onComplete = null)
    {
        shakeObject.transform.DOShakeRotation(duration, strength, (int)vibrato, randomness)
            .SetEase(ease)
            .OnComplete(() =>
            {
                shakeObject.transform.DOKill();
                onComplete?.Invoke();
            });
    }
}

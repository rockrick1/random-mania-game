using DG.Tweening;
using UnityEngine;

public class ScaleAnimation : MonoBehaviour
{
    [SerializeField] public Vector3 scaleFrom;
    [SerializeField] float duration;
    [SerializeField] float delay;
    [SerializeField] Ease ease;
    [SerializeField] bool playOnAwake;

    Vector3 scaleTo;

    RectTransform Transform => (RectTransform) transform;
    
    void Awake ()
    {
        scaleTo = Transform.localScale;
        if (playOnAwake)
            Play();
    }

    public void Play ()
    {
        transform.DOScale(scaleFrom, 0);
        transform.DOScale(scaleTo, duration).SetDelay(delay).SetEase(ease);
    }
}
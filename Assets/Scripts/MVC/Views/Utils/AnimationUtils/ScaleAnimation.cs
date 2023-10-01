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
        Setup();
        if (playOnAwake)
            Play();
    }

    void Setup ()
    {
        scaleTo = Transform.localScale;
    }

    public void Play ()
    {
        if (scaleTo == default)
            Setup();
        transform.DOScale(scaleFrom, 0);
        transform.DOScale(scaleTo, duration).SetDelay(delay).SetEase(ease);
    }
}
using DG.Tweening;
using UnityEngine;

public class FadeAnimation : BaseCanvasGroupAnimation
{
    [SerializeField] float fadeFrom;
    [SerializeField] float overrideFadeTo;

    float fadeTo;

    public override void Play ()
    {
        base.Play();
        if (fadeTo == default)
            Setup();
        _canvasGroup.alpha = fadeFrom;
        tween = _canvasGroup.DOFade(overrideDestinationValue ? overrideFadeTo : fadeTo, duration).SetDelay(delay).SetEase(ease);
    }

    protected override void Setup ()
    {
        fadeTo = _canvasGroup.alpha;
    }
}
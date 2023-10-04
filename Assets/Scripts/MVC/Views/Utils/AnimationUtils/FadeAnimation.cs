using DG.Tweening;
using UnityEngine;

public class FadeAnimation : BaseCanvasGroupAnimation
{
    [SerializeField] float fadeFrom;
    [SerializeField] float overrideFadeTo;

    float fadeTo;

    public override void Play ()
    {
        if (fadeTo == default)
            Setup();
        _canvasGroup.DOFade(fadeFrom, 0);
        _canvasGroup.DOFade(overrideDestinationValue ? overrideFadeTo : fadeTo, duration).SetDelay(delay).SetEase(ease);
    }

    protected override void Setup ()
    {
        fadeTo = _canvasGroup.alpha;
    }
}
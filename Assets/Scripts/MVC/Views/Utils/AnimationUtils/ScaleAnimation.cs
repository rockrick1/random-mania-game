using DG.Tweening;
using UnityEngine;

public class ScaleAnimation : BaseTransformAnimation
{
    [SerializeField] Vector3 scaleFrom;
    [SerializeField] Vector3 overrideScaleTo;

    Vector3 scaleTo;

    public override void Play ()
    {
        base.Play();
        if (scaleTo == default)
            Setup();
        _transform.localScale = scaleFrom;
        tween = _transform.DOScale(overrideDestinationValue ? overrideScaleTo : scaleTo, duration).SetDelay(delay).SetEase(ease);
    }

    protected override void Setup ()
    {
        scaleTo = _transform.localScale;
    }
}
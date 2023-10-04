using DG.Tweening;
using UnityEngine;

public class MoveAnimation : BaseTransformAnimation
{
    [SerializeField] public Vector3 moveFrom;
    [SerializeField] public Vector3 overrideMoveTo;

    Vector3 moveTo;

    public override void Play ()
    {
        if (moveTo == default)
            Setup();
        _transform.DOLocalMove(moveFrom, 0);
        _transform.DOLocalMove(overrideDestinationValue ? overrideMoveTo : moveTo, duration).SetDelay(delay).SetEase(ease);
    }

    protected override void Setup ()
    {
        moveTo = _transform.localPosition;
    }
}
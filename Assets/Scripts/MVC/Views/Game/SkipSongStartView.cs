using System;
using DG.Tweening;
using UnityEngine;

public class SkipSongStartView : MonoBehaviour
{
    const float ANIMATION_DURATION = .3f;
    
    [SerializeField] CanvasGroup canvasGroup;

    void Start ()
    {
        canvasGroup.alpha = 0;
        transform.DOScaleY(0, 0);
    }

    public void Show ()
    {
        canvasGroup.DOFade(1, ANIMATION_DURATION).SetEase(Ease.OutCubic);
        transform.DOScaleY(1, ANIMATION_DURATION).SetEase(Ease.OutCubic);
    }

    public void Hide ()
    {
        canvasGroup.DOFade(0, ANIMATION_DURATION).SetEase(Ease.OutCubic);
        transform.DOScaleY(0, ANIMATION_DURATION).SetEase(Ease.OutCubic);
    }
}
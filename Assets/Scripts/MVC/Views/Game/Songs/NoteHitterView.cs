﻿using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class NoteHitterView : MonoBehaviour
{
    [Header("Hitter")]
    [SerializeField] Color unselectedColor;
    [SerializeField] Color selectedColor;
    [SerializeField] Image hitterPressedImage;
    [SerializeField] Image hitter;
    [SerializeField] float hitterDuration;
    
    [Header("Hit effect")]
    [SerializeField] Image hitEffect;
    [SerializeField] float effectDuration;
    [SerializeField] float effectEndDuration;
    [SerializeField] float effectMiddleDuration;
    [SerializeField] Vector3 effectMiddleScale;
    
    [SerializeField] float effectFadeDuration;
    [SerializeField] float effectFadeEndDuration;

    Sequence scaleSequence;
    Sequence alphaSequence;
    Tween effectFade;
    
    void Awake()
    {
        hitEffect.transform.DOScale(Vector3.zero, 0);
        hitEffect.DOFade(0, 0);
    }

    public void SetHitterSelectedState (bool selected)
    {
        hitter.color = selected ? selectedColor : unselectedColor;
        SetHitterPressedState(false);
    }

    public void SetHitterPressedState (bool pressed) => hitterPressedImage.DOFade(pressed ? 1 : 0, hitterDuration);

    public void PlayHitterEffect ()
    {
        scaleSequence?.Kill();
        scaleSequence = DOTween.Sequence();
        scaleSequence.Append(hitEffect.transform.DOScale(Vector3.one, effectDuration));
        scaleSequence.Append(hitEffect.transform.DOScale(Vector3.zero, effectEndDuration));
        scaleSequence.Play();
        alphaSequence?.Kill();
        alphaSequence = DOTween.Sequence();
        alphaSequence.Append(hitEffect.DOFade(1, effectFadeDuration));
        alphaSequence.Append(hitEffect.DOFade(0, effectFadeEndDuration));
        alphaSequence.Play();
    }

    public void StartLongNoteEffect ()
    {
        hitEffect.transform.DOScale(Vector3.one, effectDuration).OnComplete(() =>
        {
            scaleSequence?.Kill();
            scaleSequence = DOTween.Sequence();
            scaleSequence.Append(hitEffect.transform.DOScale(effectMiddleScale, effectMiddleDuration).SetEase(Ease.InOutCubic));
            scaleSequence.Append(hitEffect.transform.DOScale(Vector3.one, effectMiddleDuration).SetEase(Ease.InOutCubic));
            scaleSequence.SetLoops(-1);
            scaleSequence.Play();
            hitEffect.DOFade(1, effectFadeDuration);
        });
    }

    public void EndLongNoteEffect ()
    {
        hitEffect.transform.DOScale(Vector3.zero, effectEndDuration);
        hitEffect.DOFade(0, effectFadeEndDuration);
    }
}
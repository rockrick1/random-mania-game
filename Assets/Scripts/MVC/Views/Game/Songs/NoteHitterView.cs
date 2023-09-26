using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class NoteHitterView : MonoBehaviour
{
    [Header("Hitter")]
    [SerializeField] Image hitter;
    [SerializeField] float hitterDuration;
    [SerializeField] Vector3 hitterScale;
    
    [Header("Hit effect")]
    [SerializeField] Image hitEffect;
    [SerializeField] float effectDuration;
    [SerializeField] float effectEndDuration;
    [SerializeField] float effectMiddleDuration;
    [SerializeField] Vector3 effectMiddleScale;
    
    [SerializeField] float effectFadeDuration;
    [SerializeField] float effectFadeEndDuration;
    
    void Awake()
    {
        hitEffect.transform.DOScale(Vector3.zero, 0);
        hitEffect.DOFade(0, 0);
    }

    public void PlayHitterPressed ()
    {
        hitter.transform.DOScale(hitterScale, hitterDuration);
    }

    public void PlayHitterReleased()
    {
        hitter.transform.DOScale(Vector3.one, hitterDuration);
    }

    public void PlayHitterEffect ()
    {
        Sequence scaleSequence = DOTween.Sequence();
        scaleSequence.Append(hitEffect.transform.DOScale(Vector3.one, effectDuration));
        scaleSequence.Append(hitEffect.transform.DOScale(Vector3.zero, effectEndDuration));
        scaleSequence.Play();
        Sequence alphaSequence = DOTween.Sequence();
        alphaSequence.Append(hitEffect.DOFade(1, effectFadeDuration));
        alphaSequence.Append(hitEffect.DOFade(0, effectFadeEndDuration));
        alphaSequence.Play();
    }

    public void StartLongNoteEffect ()
    {
        hitEffect.transform.DOScale(Vector3.one, effectDuration).OnComplete(() =>
        {
            Sequence scaleSequence = DOTween.Sequence();
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
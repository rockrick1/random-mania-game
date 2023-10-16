using System;
using DG.Tweening;
using UnityEngine;

public class PauseView : MonoBehaviour
{
    [SerializeField] UIClickHandler resumeButton;
    [SerializeField] UIClickHandler retryButton;
    [SerializeField] UIClickHandler quitButton;

    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] float fadeDuration;
    
    public UIClickHandler ResumeButton => resumeButton;
    public UIClickHandler RetryButton => retryButton;
    public UIClickHandler QuitButton => quitButton;
    public bool IsPlayingAnimation => fadeTween?.IsPlaying() ?? false;

    Tween fadeTween;

    void Start ()
    {
        canvasGroup.alpha = 0f;
    }

    public void Open ()
    {
        gameObject.SetActive(true);
        canvasGroup.alpha = 0f;
        fadeTween = canvasGroup.DOFade(1, fadeDuration).SetEase(Ease.OutCubic);
    }

    public void Close (Action onFinish = null)
    {
        fadeTween = canvasGroup.DOFade(0, fadeDuration).SetEase(Ease.OutCubic).OnComplete(() =>
        {
            gameObject.SetActive(false);
            onFinish?.Invoke();
        });
    }
}
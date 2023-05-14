using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class ResultsView : MonoBehaviour
{
    [SerializeField] UIClickHandler retryButton;
    [SerializeField] UIClickHandler quitButton;
    
    [Header("Notes")]
    [SerializeField] TextMeshProUGUI perfectCount;
    [SerializeField] TextMeshProUGUI greatCount;
    [SerializeField] TextMeshProUGUI okayCount;
    [SerializeField] TextMeshProUGUI missCount;
    
    [Header("Score")]
    [SerializeField] TextMeshProUGUI score;
    [SerializeField] TextMeshProUGUI combo;
    [SerializeField] TextMeshProUGUI accuracy;
    
    [Header("Canvas")]
    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] float fadeDuration;
    
    public UIClickHandler RetryButton => retryButton;
    public UIClickHandler QuitButton => quitButton;

    void Start ()
    {
        canvasGroup.alpha = 0f;
        gameObject.SetActive(false);
    }

    public void SetCounts (string perfect, string great, string okay, string miss)
    {
        perfectCount.text = perfect;
        greatCount.text = great;
        okayCount.text = okay;
        missCount.text = miss;
    }

    public void SetScore (string score, string combo, string accuracy)
    {
        this.score.text = score;
        this.combo.text = combo;
        this.accuracy.text = accuracy;
    }

    public void Open ()
    {
        gameObject.SetActive(true);
        canvasGroup.alpha = 0f;
        canvasGroup.DOFade(1, fadeDuration).SetEase(Ease.OutCubic);
    }

    public void Close (Action onFinish = null)
    {
        canvasGroup.DOFade(0, fadeDuration).SetEase(Ease.OutCubic).OnComplete(() =>
        {
            gameObject.SetActive(false);
            onFinish?.Invoke();
        });
    }
}
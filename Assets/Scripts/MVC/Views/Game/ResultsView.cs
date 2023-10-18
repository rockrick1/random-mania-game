using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class ResultsView : MonoBehaviour
{
    [SerializeField] UIClickHandler retryButton;
    [SerializeField] UIClickHandler quitButton;
    
    [Header("Title")]
    [SerializeField] TextMeshProUGUI title;

    [Header("Notes")]
    [SerializeField] TextMeshProUGUI perfectCount;
    [SerializeField] TextMeshProUGUI greatCount;
    [SerializeField] TextMeshProUGUI okayCount;
    [SerializeField] TextMeshProUGUI missCount;
    
    [Header("Score")]
    [SerializeField] TextMeshProUGUI score;
    [SerializeField] TextMeshProUGUI combo;
    [SerializeField] TextMeshProUGUI accuracy;
    [SerializeField] TextMeshProUGUI result;
    
    [Header("Offsets")]
    [SerializeField] TextMeshProUGUI earlyOffset;
    [SerializeField] TextMeshProUGUI lateOffset;
    [SerializeField] Transform graphBarsParent;
    
    [Header("Canvas")]
    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] float fadeDuration;
    
    public UIClickHandler RetryButton => retryButton;
    public UIClickHandler QuitButton => quitButton;
    public int BarCount => graphBars.Count;

    List<RectTransform> graphBars;
    float graphBarMaxHeight;

    void Start ()
    {
        canvasGroup.alpha = 0f;
        gameObject.SetActive(false);
        graphBars = graphBarsParent.GetComponentsInChildren<RectTransform>().ToList();
        graphBars.RemoveAt(0);
        graphBarMaxHeight = graphBars[0].rect.height;
    }

    public void SetTitle (string text) => title.text = text;

    public void SetCounts (string perfect, string great, string okay, string miss)
    {
        perfectCount.text = perfect;
        greatCount.text = great;
        okayCount.text = okay;
        missCount.text = miss;
    }

    public void SetScore (string score, string combo, string accuracy, string result)
    {
        this.score.text = score;
        this.combo.text = combo;
        this.accuracy.text = accuracy;
        this.result.text = result;
    }

    public void SetOffsets (string offset)
    {
        earlyOffset.text = offset;
        lateOffset.text = offset;
    }

    public void SetOffsetsBarRatio (int index, float ratio)
    {
        Vector2 sizeDelta = graphBars[index].sizeDelta;
        sizeDelta.y = graphBarMaxHeight * ratio;
        graphBars[index].sizeDelta = sizeDelta;
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
using DG.Tweening;
using UnityEngine;

public class EditorSongSavedView : MonoBehaviour
{
    [SerializeField] CanvasGroup canvasGroup;

    Sequence showSequence;
    
    void Start ()
    {
        gameObject.SetActive(true);
        canvasGroup.alpha = 0;
    }

    public void Show ()
    {
        showSequence?.Kill();
        showSequence = DOTween.Sequence();
        showSequence.Append(canvasGroup.DOFade(1, .1f));
        showSequence.Append(canvasGroup.DOFade(0, 1).SetEase(Ease.InQuad).SetDelay(1.3f));
        showSequence.Play();
    }
}

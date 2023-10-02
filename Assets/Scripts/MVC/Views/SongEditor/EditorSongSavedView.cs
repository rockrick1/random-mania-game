using DG.Tweening;
using UnityEngine;

public class EditorSongSavedView : MonoBehaviour
{
    [SerializeField] CanvasGroup canvasGroup;

    void Start ()
    {
        gameObject.SetActive(true);
        canvasGroup.alpha = 0;
    }

    public void Show ()
    {
        canvasGroup.DOFade(1, .2f);
        canvasGroup.DOFade(0, 1).SetEase(Ease.InQuad).SetDelay(1.3f);
    }
}

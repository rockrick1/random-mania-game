using DG.Tweening;
using TMPro;
using UnityEngine;

public class ComboView : MonoBehaviour
{
    const string COMBO_FORMAT = "{0}x";
    
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] float animDuration;
    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] FadeAnimation fadeIn;
    [SerializeField] FadeAnimation fadeOut;
    
    readonly Vector3 scale = new(1.3f, 1.2f, 1f);
    readonly Vector3 original = new(1f, 1f, 1f);

    Sequence sequence;

    public void SetCombo (int combo)
    {
        sequence = DOTween.Sequence();
        sequence.Append(text.transform.DOScale(scale, animDuration / 2));
        sequence.Append(text.transform.DOScale(original, animDuration / 2));
        sequence.Play();
        text.text = string.Format(COMBO_FORMAT, combo);
    }

    public void PlayFadeInAnimation () => canvasGroup.alpha = 1;

    public void PlayFadeOutAnimation () => fadeOut.Play();
}

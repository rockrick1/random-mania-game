using TMPro;
using UnityEngine;

public class ScoreView : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI accuracyText;
    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] FadeAnimation fadeIn;
    [SerializeField] FadeAnimation fadeOut;

    public void SetScore (string score) => scoreText.text = score;

    public void SetAccuracy (string accuracy) => accuracyText.text = accuracy;

    public void PlayFadeInAnimation () => canvasGroup.alpha = 1;

    public void PlayFadeOutAnimation () => fadeOut.Play();
}

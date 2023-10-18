using UnityEngine;

public class SongView : MonoBehaviour
{
    [SerializeField] UpperSongView upperSongView;
    [SerializeField] LowerSongView lowerSongView;
    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] FadeAnimation fadeIn;
    [SerializeField] FadeAnimation fadeOut;

    public UpperSongView UpperSongView => upperSongView;
    public LowerSongView LowerSongView => lowerSongView;

    public void PlayFadeInAnimation () => canvasGroup.alpha = 1;

    public void PlayFadeOutAnimation () => fadeOut.Play();
}
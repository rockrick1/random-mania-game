using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class SettingsView : MonoBehaviour
{
    public event Action<float> OnMainVolumeChanged;
    public event Action<float> OnMusicVolumeChanged;
    public event Action<float> OnSFXVolumeChanged;
    public event Action OnClose;
    
    [SerializeField] Slider mainVolumeSlider;
    [SerializeField] Slider musicVolumeSlider;
    [SerializeField] Slider sfxVolumeSlider;

    [SerializeField] UIClickHandler closeButton;
    
    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] float fadeDuration;

    public Slider MainVolumeSlider => mainVolumeSlider;
    public Slider MusicVolumeSlider => musicVolumeSlider;
    public Slider SFXVolumeSlider => sfxVolumeSlider;

    void Start ()
    {
        mainVolumeSlider.onValueChanged.AddListener((value) => OnMainVolumeChanged?.Invoke(value));
        musicVolumeSlider.onValueChanged.AddListener((value) => OnMusicVolumeChanged?.Invoke(value));
        sfxVolumeSlider.onValueChanged.AddListener((value) => OnSFXVolumeChanged?.Invoke(value));
        closeButton.OnLeftClick.AddListener(() => OnClose?.Invoke());
    }

    public void Open () => gameObject.SetActive(true);
}
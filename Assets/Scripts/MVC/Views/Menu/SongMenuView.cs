using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SongMenuView : MonoBehaviour
{
    const float LIST_ROTATION = 15f;
    
    public event Action OnBackPressed;
    public event Action OnPlayPressed;
    public event Action<float> OnARSliderValueChanged;

    [Header("Song List")]
    [SerializeField] ScrollRect scrollRect;
    [SerializeField] VerticalLayoutGroup songsList;
    [SerializeField] SongEntryView songEntryPrefab;
    
    [Header("Selected Song Box")]
    [SerializeField] TextMeshProUGUI selectedSongTitle;
    [SerializeField] TextMeshProUGUI selectedSongArtist;
    [SerializeField] TextMeshProUGUI selectedSongDifficultyName;
    [SerializeField] TextMeshProUGUI selectedSongBPM;
    [SerializeField] TextMeshProUGUI selectedSongApproachRate;
    [SerializeField] TextMeshProUGUI selectedSongDifficulty;
    [SerializeField] TextMeshProUGUI selectedSongLength;

    [Header("Approach Rate Slider")]
    [SerializeField] Slider arSlider;
    [SerializeField] SliderValueView arSliderValueView;

    [Header("Misc")]
    [SerializeField] Image backgroundImage;
    [SerializeField] UIClickHandler backButton;
    [SerializeField] UIClickHandler playButton;

    void Start ()
    {
        backButton.OnLeftClick.AddListener(() => OnBackPressed?.Invoke());
        playButton.OnLeftClick.AddListener(() => OnPlayPressed?.Invoke());
        arSlider.onValueChanged.AddListener(x => OnARSliderValueChanged?.Invoke(x));
    }

    public void Setup (IReadOnlyList<ISongSettings> songs)
    {
        SetupSongsListSize(songs.Count);
    }

    public SongEntryView CreateEntryView ()
    {
        SongEntryView view = Instantiate(songEntryPrefab, songsList.transform);
        view.transform.localRotation = Quaternion.Euler(0f, 0f, LIST_ROTATION);
        return view;
    }

    public void Open () => gameObject.SetActive(true);

    public void SetSelectedSongTitle (string text) => selectedSongTitle.text = text;
    
    public void SetSelectedSongArtist (string text) => selectedSongArtist.text = text;
    
    public void SetSelectedSongDifficultyName (string text) => selectedSongDifficultyName.text = text;
    
    public void SetSelectedSongBPM (string text) => selectedSongBPM.text = text;

    public void SetSelectedSongApproachRate (string text) => selectedSongApproachRate.text = text;
    
    public void SetSelectedSongDifficulty (string text) => selectedSongDifficulty.text = text;
    
    public void SetSelectedSongLength (string text) => selectedSongLength.text = text;

    public void SetBackgroundImage (Sprite sprite)
    {
        backgroundImage.gameObject.SetActive(sprite != null);
        backgroundImage.sprite = sprite;
    }

    public void SetARSliderValue (float value)
    {
        arSlider.value = value;
        arSliderValueView.UpdateText(value);
    }

    void SetupSongsListSize (int songCount)
    {
        float height = ((RectTransform) songEntryPrefab.transform).rect.height;
        height *= songCount + 2;
        height += songsList.spacing * (songCount + 2);
        RectTransform rectTransform = (RectTransform)songsList.transform;
        rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, height);
    }
}
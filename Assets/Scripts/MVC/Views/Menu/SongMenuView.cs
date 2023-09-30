using System;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SongMenuView : MonoBehaviour
{
    public event Action<string, string> OnSongClicked;
    public event Action OnBackPressed;
    public event Action OnPlayPressed;

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

    [Header("Misc")]
    [SerializeField] UIClickHandler backButton;
    [SerializeField] UIClickHandler playButton;

    void Start ()
    {
        transform.DOScale(0, 0);
        backButton.OnLeftClick.AddListener(() => OnBackPressed?.Invoke());
        playButton.OnLeftClick.AddListener(() => OnPlayPressed?.Invoke());
        gameObject.SetActive(true);
    }

    public void Setup (IReadOnlyList<ISongSettings> songs)
    {
        SetupSongsListSize(songs.Count);

        foreach (ISongSettings song in songs)
        {
            SongEntryView entry = Instantiate(songEntryPrefab, songsList.transform);
            entry.transform.localRotation = Quaternion.Euler(0f, 0f, 15f);
            entry.Setup(song);
            entry.Button.OnClick.AddListener(() => OnSongClicked?.Invoke(song.Id, song.DifficultyName));
        }
    }
    
    public void Open ()
    {
        gameObject.SetActive(true);
        transform.DOScale(Vector3.one, 1f).SetEase(Ease.InOutCubic);
    }

    public void Close ()
    {
        transform.DOScale(Vector3.zero, 1f).SetEase(Ease.InOutCubic);
    }
    
    public void SetSelectedSongTitle (string text) => selectedSongTitle.text = text;
    
    public void SetSelectedSongArtist (string text) => selectedSongArtist.text = text;
    
    public void SetSelectedSongDifficultyName (string text) => selectedSongDifficultyName.text = text;
    
    public void SetSelectedSongBPM (string text) => selectedSongBPM.text = text;

    public void SetSelectedSongApproachRate (string text) => selectedSongApproachRate.text = text;
    
    public void SetSelectedSongDifficulty (string text) => selectedSongDifficulty.text = text;
    

    void SetupSongsListSize (int songCount)
    {
        float height = ((RectTransform) songEntryPrefab.transform).rect.height;
        height *= songCount + 2;
        height += songsList.spacing * (songCount + 2);
        var transform1 = songsList.transform;
        ((RectTransform) transform1).sizeDelta = new Vector2(((RectTransform) transform1).sizeDelta.x, height);
    }
}
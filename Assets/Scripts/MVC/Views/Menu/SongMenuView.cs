using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class SongMenuView : MonoBehaviour
{
    public event Action<string, string> OnSongClicked;
    public event Action OnBackPressed;

    [SerializeField] ScrollRect scrollRect;
    [SerializeField] VerticalLayoutGroup songsList;
    [SerializeField] SongEntryView songEntryPrefab;

    [SerializeField] UIClickHandler backButton;

    void Start ()
    {
        transform.DOScale(0, 0);
        backButton.OnLeftClick.AddListener(() => OnBackPressed?.Invoke());
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
        transform.DOScale(Vector3.one, 1f).SetEase(Ease.InOutCubic);
    }

    public void Close ()
    {
        transform.DOScale(Vector3.zero, 1f).SetEase(Ease.InOutCubic);
    }

    void SetupSongsListSize (int songCount)
    {
        float height = ((RectTransform) songEntryPrefab.transform).rect.height;
        height *= songCount + 2;
        height += songsList.spacing * (songCount + 2);
        var transform1 = songsList.transform;
        ((RectTransform) transform1).sizeDelta = new Vector2(((RectTransform) transform1).sizeDelta.x, height);
    }
}